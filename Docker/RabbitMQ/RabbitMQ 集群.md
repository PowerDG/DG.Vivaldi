# RabbitMQ 集群

RabbitMQ 最优秀的功能之一就是内建集群，这个功能设计的目的是允许消费者和生产者在节点崩溃的情况下继续运行，以及通过添加更多的节点来线性扩展消息通信吞吐量。RabbitMQ 内部利用 Erlang 提供的分布式通信框架 OTP 来满足上述需求，使客户端在失去一个 RabbitMQ 节点连接的情况下，还是能够重新连接到集群中的任何其他节点继续生产、消费消息。

##### RabbitMQ 集群中的一些概念

RabbitMQ 会始终记录以下四种类型的内部元数据：

1. 队列元数据
    包括队列名称和它们的属性，比如是否可持久化，是否自动删除
2. 交换器元数据
    交换器名称、类型、属性
3. 绑定元数据
    内部是一张表格记录如何将消息路由到队列
4. vhost 元数据
    为 vhost 内部的队列、交换器、绑定提供命名空间和安全属性

在单一节点中，RabbitMQ 会将所有这些信息存储在内存中，同时将标记为可持久化的队列、交换器、绑定存储到硬盘上。存到硬盘上可以确保队列和交换器在节点重启后能够重建。而在集群模式下同样也提供两种选择：存到硬盘上（独立节点的默认设置），存在内存中。

如果在集群中创建队列，集群只会在单个节点而不是所有节点上创建完整的队列信息（元数据、状态、内容）。结果是只有队列的所有者节点知道有关队列的所有信息，因此当集群节点崩溃时，该节点的队列和绑定就消失了，并且任何匹配该队列的绑定的新消息也丢失了。还好RabbitMQ 2.6.0之后提供了镜像队列以避免集群节点故障导致的队列内容不可用。

RabbitMQ 集群中可以共享 user、vhost、exchange等，所有的数据和状态都是必须在所有节点上复制的，例外就是上面所说的消息队列。RabbitMQ 节点可以动态的加入到集群中。

当在集群中声明队列、交换器、绑定的时候，这些操作会直到所有集群节点都成功提交元数据变更后才返回。集群中有内存节点和磁盘节点两种类型，内存节点虽然不写入磁盘，但是它的执行比磁盘节点要好。内存节点可以提供出色的性能，磁盘节点能保障配置信息在节点重启后仍然可用，那集群中如何平衡这两者呢？

RabbitMQ 只要求集群中至少有一个磁盘节点，所有其他节点可以是内存节点，当节点加入火离开集群时，它们必须要将该变更通知到至少一个磁盘节点。如果只有一个磁盘节点，刚好又是该节点崩溃了，那么集群可以继续路由消息，但不能创建队列、创建交换器、创建绑定、添加用户、更改权限、添加或删除集群节点。换句话说集群中的唯一磁盘节点崩溃的话，集群仍然可以运行，但知道该节点恢复，否则无法更改任何东西。

##### RabbitMQ 集群配置和启动

如果是在一台机器上同时启动多个 RabbitMQ 节点来组建集群的话，只用上面介绍的方式启动第二、第三个节点将会因为节点名称和端口冲突导致启动失败。所以在每次调用 rabbitmq-server 命令前，设置环境变量 RABBITMQ_NODENAME 和 RABBITMQ_NODE_PORT 来明确指定唯一的节点名称和端口。下面的例子端口号从5672开始，每个新启动的节点都加1，节点也分别命名为test_rabbit_1、test_rabbit_2、test_rabbit_3。

启动第1个节点：

```
RABBITMQ_NODENAME=test_rabbit_1 RABBITMQ_NODE_PORT=5672 ./sbin/rabbitmq-server -detached
```

启动第2个节点：

```
RABBITMQ_NODENAME=test_rabbit_2 RABBITMQ_NODE_PORT=5673 ./sbin/rabbitmq-server -detached
```

启动第2个节点前建议将 RabbitMQ 默认激活的插件关掉，否则会存在使用了某个插件的端口号冲突，导致节点启动不成功。

现在第2个节点和第1个节点都是独立节点，它们并不知道其他节点的存在。集群中除第一个节点外后加入的节点需要获取集群中的元数据，所以要先停止 Erlang 节点上运行的 RabbitMQ 应用程序，并重置该节点元数据，再加入并且获取集群的元数据，最后重新启动 RabbitMQ 应用程序。

停止第2个节点的应用程序：

```
./sbin/rabbitmqctl -n test_rabbit_2 stop_app
```

重置第2个节点元数据：

```
./sbin/rabbitmqctl -n test_rabbit_2 reset
```

第2节点加入第1个节点组成的集群：

```
./sbin/rabbitmqctl -n test_rabbit_2 join_cluster test_rabbit_1@localhost
```

启动第2个节点的应用程序

```
./sbin/rabbitmqctl -n test_rabbit_2 start_app
```

第3个节点的配置过程和第2个节点类似：

```
RABBITMQ_NODENAME=test_rabbit_3 RABBITMQ_NODE_PORT=5674 ./sbin/rabbitmq-server -detached

./sbin/rabbitmqctl -n test_rabbit_3 stop_app

./sbin/rabbitmqctl -n test_rabbit_3 reset

./sbin/rabbitmqctl -n test_rabbit_3 join_cluster test_rabbit_1@localhost

./sbin/rabbitmqctl -n test_rabbit_3 start_app
```

##### RabbitMQ 集群运维

停止某个指定的节点，比如停止第2个节点：

```
RABBITMQ_NODENAME=test_rabbit_2 ./sbin/rabbitmqctl stop
```

查看节点3的集群状态：

```
./sbin/rabbitmqctl -n test_rabbit_3 cluster_status
```

作者：预流

链接：https://www.jianshu.com/p/79ca08116d57/

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。