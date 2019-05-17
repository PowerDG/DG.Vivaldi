# EventStore文件存储设计

​                                                   2019年05月15日 06:40:00           [dotNET跨平台](https://me.csdn.net/sD7O95O)           阅读数：21                   

​                   

## 背景

ENode是一个CQRS+Event  Sourcing架构的开发框架，Event  Sourcing需要持久化事件，事件可以持久化在DB，但是DB由于面向的是CRUD场景，是针对数据会不断修改或删除的场景，所以内部实现会比较复杂，性能也相对比较低。而Event  Store实际上对数据只有新增和查询的需求，所以我想为Event Sourcing的场景针对性的实现一个Event  Store。看了一下业界的一些实现，感觉都没有达到我的期望，所以想自己动手实现一个。下面是我构思的一个Event  Store的单机版应该要具备的能力以及对应的设计方案，分享出来和大家讨论。

## 一、需求概述

- 存储聚合根的事件数据
- 支持事件的版本并发控制，新事件的版本号必须是当前版本号+1
- 支持命令重复判断，即不可以处理重复命令产生的事件
- 支持按聚合根ID查询该聚合根的所有事件
- 支持按聚合根ID+事件版本号查询指定的事件
- 支持按命令ID查询该命令对应的事件数据
- 高性能，写入要尽量快，查询要尽量快

## 二、事件数据格式

```
{
"aggregateRootId": "",     //聚合根ID
"aggregateRootType": "",   //聚合根类型
"eventVersion": "",        //事件版本号
"eventTime": "",           //事件发生时间
"eventData": "",           //事件数据，JSON格式
"commandId": "",           //产生该事件的命令ID
"commandTime": ""          //产生该事件的命令产生时间
}
```

## 三、存储设计

### 1、核心内存存储设计

- 遵循内存只存储索引数据的原则，尽量充分利用内存；
- aggregateLatestVersionDict，存储每个聚合根的最大事件版本号
  - eventVersion，当前聚合根的最新事件的版本号，也即当前聚合根的版本号
  - eventTime，事件产生时间
  - eventPosition，事件在事件数据文件中的位置
  - key：aggregateRootId，聚合根ID
  - value：
- commandIdDict，存储命令索引
  - commandTime，命令产生时间
  - eventPosition，命令对应的事件在事件数据文件中的位置
  - key：commandId，命令ID
  - value：

### 2、物理存储的数据

- 事件数据：eventData，单条数据的结构：

```
{
"aggregateRootId": "",     //聚合根ID
"aggregateRootType": "",   //聚合根类型
"eventVersion": "",        //事件版本号
"eventTime": "",           //事件发生时间
"eventData": "",           //事件数据，JSON格式
"commandId": "",           //产生该事件的命令ID
"commandTime": "",         //产生该事件的命令产生的事件
"previousEventPosition": ""//前一个事件在事件文件中的位置
}
```

- 事件索引：eventIndex，单条数据的结构：

```
{
"aggregateRootId": "",     //聚合根ID
"eventVersion": "",        //事件版本号
"eventTime": "",           //事件产生时间
"eventPosition": "",       //事件在事件数据文件中的位置
}
```

- 命令索引：commandIndex，存储内容：存储所有命令的ID及其对应的事件所在文件的位置

```
{
"commandId": "",        //聚合根ID
"commandTime": "",      //命令产生时间
"eventPosition": "",    //事件在事件数据文件中的位置
}
```

### 3、事件数据存储 

- 同步顺序写eventDataChunk文件，一个文件大小为1GB，写满一个文件后写入下一个文件；
- 写入每个事件时，同时写入当前事件的前一个事件所在的文件位置，以便将来可以一次性将某个聚合根的所有事件从文件查找出来；

### 4、事件索引存储

- 异步顺序写eventIndexChunk文件，一个文件大小为1GB，写满一个文件后写入下一个文件；
- 对于已经写满的不会再变化的文件的内容，使用后台线程进行B+树索引整理，索引的排序依据是聚合根ID+事件版本号；B+树设计为3层，根节点包含1000个子节点，每个子节点再包含1000个子节点，这样叶子节点共有100W个。每个叶子节点我们保存20个版本索引，则单个文件共可保存最多2000W个版本索引，10个文件为2亿个版本索引；单机存储2亿个事件索引，应该可以满足大部分应用场景了；3层，则查找任意一个节点，只需要3次IO访问；
- 由于是后台线程对已经写完的文件进行B+树索引整理，B+树是在内存建立，建立完成后，将最新的内容写入新文件，原子替换老的eventIndexChunk文件；所以，这块的逻辑处理应该不会对服务的主逻辑产生较大的影响；
- 采用BloomFilter优化查询性能，使用BloomFilter来快速判断某个eventIndexChunk文件中是否包含某个聚合根ID，如果不在，则不用从B+树去检索该聚合根的版本号了；如果在，则取检索；通过这个设计，当我们要获取某个聚合根的最大版本号时，不需要对每个eventIndexChunk文件进行B+树查询，而是先通过BloomFilter快速判断当前的eventIndexChunk文件是否包含该聚合根的信息，大大提升检索效率；BloomFilter的二进制Bit数据占用内存小，可以在每个eventIndexChunk文件被扫描时，和文件头的信息一起加载到内存；

### 5、命令索引存储

- 异步顺序写commandIndexChunk文件，一个文件大小为1GB，写满一个文件后写入下一个文件；
- 同事件索引存储，进行B+树索引建立，索引的排序依据是命令ID；
- 同事件索引存储，采用BloomFilter优化查询性能；

## 四、框架逻辑设计

### 1、查询某个聚合根的最大版本号

- EventStore启动时，会加载所有的eventIndexChunk文件的元数据到内存，比如文件号、文件头、BloomFilter等信息，但不真实加载文件内容，文件数不会太多，最多也就几十个；
- 根据聚合根ID+BloomFilter算法，快速确定应该到哪个eventIndexChunk文件中去查找该聚合根的最新版本号，eventIndexChunk文件从新到旧遍历，因为某个聚合根ID的最大版本号一定是在最新的eventIndexChunk文件中的；
- 在找到的eventIndexChunk中使用B+树查找算法，找到对应的叶子节点；
- 在找到的叶子节点，使用二分查找算法（由于单个节点的聚合根ID不多，顺序查找即可），找到指定聚合根的最新版本号；

### 2、查询某个聚合根的所有事件

- 先通过上面的算法找出该聚合根的最大版本号的事件在事件数据文件中的位置；
- 然后从该位置获取事件完整数据；
- 再根据事件数据中记录的上一个事件在事件数据文件中的位置，查找上一个事件的数据；
- 以此类推，直到找到该聚合根的第一个事件的数据；

### 3、查询某个命令对应的事件数据

- 先尝试从内存查询该命令的索引信息，如果存在，则直接获取该命令对应的事件在事件数据文件中的位置，即eventPosition；如果不存在，则尝试从命令的索引文件中查找，结合BloomFilter和B+树查找算法进行查找；
- 如果找到了eventPosition，则根据eventPosition到事件数据文件中查找对应的事件数据即可；如果未找到，则返回空；

### 4、追加一个新事件的处理逻辑

- 根据aggregateLatestVersionDict判断事件版本号是否合法，必须是聚合根的当前版本号+1，如果当前版本号不存在，则首先尝试从eventIndexChunk文件查找当前聚合根的最大版本号，如果还是查找不到，说明当前聚合根确实不存在任何事件，则当前事件版本号必须为1；
- 根据commandIdDict判断命令ID是否重复，如果commandIdDict中不存在该命令，尝试从commandIndexChunk文件中查找，也是B+树的方式；这里需要设计一个配置项，让开发者配置是否需要继续从commandIndexChunk文件查找命令ID。有时我们只希望从内存查找即可，不希望再从磁盘查找了，因为判断命令是否重复我们很多时候只希望检查最近一段时间内的命令，检查全部命令代价过大，意义也不是很大；
- 如果事件的版本号合法、命令ID不重复，则Append的方式写入事件数据到eventDataChunk；
- 写入完成后，更新aggregateLatestVersionDict、commandIdDict，、BloomFilter的Bit数组，以及将当前的事件放入内存的一个双缓冲队列；队列消费者异步批量将事件索引和命令索引写入对应的索引文件；
- 返回事件写入结果；

### 5、其他逻辑

- 异步线程定时批量持久化事件索引；
- 异步线程定时批量持久化命令索引；
- 异步线程定时清理不需要放在内存的聚合根最新版本号信息（aggregateLatestVersionDict中的key），根据eventTime判断，只保留最近1周有过变化（产生过事件）的聚合根；
- 异步线程定时清理不需要放在内存的命令索引（commandIdDict中的key），根据commandTime判断，只保留最近1周的命令ID；
- 异步线程定时进行事件索引和命令索引的B+树索引的建立，即对已经写入完成的eventIndexChunk和commandIndexChunk文件的内部重构；
- eventIndexChunk和commandIndexChunk文件标记为写入完成前，要把BloomFilter的Bit数组内容写入文件中；
- 其他EventStore的启动逻辑，比如启动时加载一定数量的索引数据到内存，以及索引数据相比事件数据是否有漏掉或无效的检查；
- 其他逻辑支持，如支持聚合根的快照存储，从文件查找数据时，如果文件的B+树索引信息还未建立，则需要进行全文扫码；

*原文地址：https://www.cnblogs.com/netfocus/p/10861152.html*

```

```

.NET社区新闻，深度好文，欢迎访问公众号文章汇总 http://www.csharpkit.com 
![640?wx_fmt=jpeg](assets/p)