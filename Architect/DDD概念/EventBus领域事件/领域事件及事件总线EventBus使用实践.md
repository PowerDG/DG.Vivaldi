# 领域事件及事件总线EventBus使用实践

​             ![96](assets/59c85de5-220d-4197-97ba-39ba6cbb9646.jpg) 

​             [尴尬年代的尴尬人儿](https://www.jianshu.com/u/fe1a473a6ec7)





> [@大鹏开源](https://link.jianshu.com?t=https%3A%2F%2Fgithub.com%2Fdapeng-soa):别看我有点萌，我可以秒变大鹏😄



![img](https:////upload-images.jianshu.io/upload_images/5261016-ea687a67ed9ea66e..jpg?imageMogr2/auto-orient/strip%7CimageView2/2/w/218)

image

## DDD 与领域事件

> 在过去的 30 多年，就已经有领域建模和设计的思潮；Eric Evans 将其定义为领域驱动设计（Domain-Driven Design，简称DDD）。领域模型是领域驱动的核心，而领域事件又作为领域模型中的重要模块，解决了开发者日常开发中的很多痛点，比如，代码藕合降低，拓展性增强。

领域模型不是高大上的东西，所有的领域模型抽象都来自于具体的业务及业务的需求,而脱离业务需求的应用设计是没有任何价值的！

比如在Today的新零售电商架构中:门店、采购、订单、供应商、物流、商品、台账等等都是应用设计中的不同领域模型，必然还存在或多或少的子域模型。而对于技术人员来说，这些抽象出来的领域，就代表应用架构存在若干子系统。

系统与系统间，势必会存在某些关联。比如说A领域“发生某件事情”、“当什么产生变化的时候”、“如果什么状态变更”...，都将可能成为B领域所要关心的事件。

## 事件总线的大体流程

- 在这里发出事件通知的一方称为发送者 (Publisher) ,关心事件的一方称为订阅者 (Subscriber)。
- 关心一件事，便会收集这件事情相关的信息。而这些都将会转换为消息流，在订阅这件事情的领域间传播，一旦命中所要关心的事情，就由订阅者自行去处理接下来的事情。



![img](https:////upload-images.jianshu.io/upload_images/5261016-18dada5f6e6fadce..jpg?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

image

以上eventbus示意图大致流程是这样的：

- 服务接口触发事件
- eventbus 分发事件，如果存在领域内订阅者，直接分发到指定订阅者，再将事件消息存库定时发送至 kafka
- 如果不存在领域内订阅者，事件消息直接存库并定时发送 kafka
- 消息在发送成功以后会被清除，为了保证事务的一致性。建议事件db共享业务数据源
- 订阅者只需要订阅事件双方规约好的 topic 和事件类型就可以命中需要的事件消息

## 为什么要引入事件

在代码中通常这样去描述针对某些状态对应做什么事。

```
code match {
    case 0 => 
        // do A
    case 1 => 
        // do B
    case _ => 
        // do ...
} 
```

当遇到某个状态，需要第三方系统作出应对，开发者可能不是那么愿意为此去加入冗长的代码甚至引入别人的 api ，而这与当前领域相关性也不会太大。领域事件帮助开发者更加优雅的解决了这个问题！

> do A 就变成了 do event_A

#### 降低耦合，高拓展性

- 事件对于触发的一方来说只是一个函数，而不再是一大堆的业务逻辑，将这些与领域解藕，让业务系统只关心业务！
- 如果发送方需要发送更多的事件，只需要触发更多的方法即可；对于订阅者来说，可以订阅任何领域发过来的事件消息

#### 高复用性

- 事件可能只需要做一次，而发送者与订阅者是 1:N 的，一个事件可以被不同的接口多次触发。

#### 最终一致性与弱关联

通常需要保证数据的最终一致性，这对于事件消息来说还是很容易的，这得益于基于消息的重试机制。与主业务关系不大的业务，如：发邮件，发短信等,这种弱关联是可以在事件中获益！

## 如何实践事件总线

> 在 today 中台服务团队的各领域实践中，已经开始投产 eventbus ，并且效果可观，三方系统的订阅对接相当便捷.那这样的事件机制该如何去使用？

为了给第三方系统和本部门的业务开发人员提供一致性的开发体验，我们将事件总线从dapeng的框架中剥离出来, 单独提供了一套类库用于实现事件的发布以及订阅。

### 事件总线eventBus的核心库

```
"com.today" % "event-bus_2.12" % "0.1-SNAPSHOT"
```

### 事件内容及状态暂存支持

- 需要在业务数据库加入一张如下结构数据表,这将作为事件消息的暂存队列和事件发送状态存储表

```
CREATE TABLE `common_event` (
  `id` bigint(20) NOT NULL  COMMENT '事件id',
  `event_type` varchar(255) DEFAULT NULL COMMENT '事件类型',
  `event_binary` blob DEFAULT NULL COMMENT '事件内容',
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
```

> 这里以商品变价审核的状态变更为例，了解在开发中如何做事件发送与订阅.
>  具体来说,就是三步:

1. 定义事件结构体
2. 在服务接口方法中声明待发布的事件
3. 通过EventBus发布事件
4. 通过EventBus接受事件

下面具体说明一下每个步骤

## 作为事件消息发送者

### 消息的定义和声明

1.事件收发双方共同协定定义事件消息的内容, 一个领域的所有消息定义都在同一个独立的idl文件中. 这个idl文件应该放在发布者的API包中.

2.我们的事件对象需要定义一个事件 id (建议通过分布式取号服务来获取), 订阅者可以自己决定是否需要用这个事件 id 来做消息的幂等处理

```
==> goods_events.thrift
namespace java com.today.api.goods.events

/**
* 商品变价审核通过事件
**/
struct SkuPriceUpdateApprovedEvent {
    /**
    * 事件id
    **/
    1:i64 id,
    /**
    * sku_id
    **/
    2:i64 skuId
}

...more
```

### 声明事件

> 秉承代码及文档一致的理念，所有的服务都会在统一的文档站点进行开放展示，每个服务/每个接口的描述,包括出入参都一目了然.

- 我们在服务接口方法里面声明需要发布的事件，这些事件清单将会在文档站点对应的服务方法中得到展示，减少服务开发人员的沟通成本，一看便知。

```
== >goods_service.thrift
namespace java com.today.api.goods.service
/**
* 用户服务
**/
service GoodsAdminService{
/**
# 商品审核
## 事件
    1.SkuPriceUpdateApprovedEvent  商品变价审核通过事件
    2.SkuAttributeUpdateApprovedEvent  商品属性审核通过事件
**/
    i64 approveSku(/** 审核请求实体**/1:goods_request.ApproveSkuRequest request)
    (events="com.today.api.goods.events.SkuPriceUpdateApprovedEvent,com.today.api.goods.events.SkuAttributeUpdateApprovedEvent")
    ...more
}(group="goods")
```

- 在文档站点方法上效果如下：

  

  ![img](https:////upload-images.jianshu.io/upload_images/5261016-5194b9ca58a5800f..jpg?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

  image

- 显示独立的事件清单

  

  ![img](https:////upload-images.jianshu.io/upload_images/5261016-a7c62652e48a5bb3..jpg?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

  image

> 注：如果想要了解更多有关文档站点的内容，请留意后期的 dapeng 文档站点专题

### 定义事件发布任务

```
==> goods_event_task.thrift
namespace java com.today.api.goods.service

/**
* 商品事件发布任务
**/
service GoodsScheduledService {
/**
# 事件发布
## 注意事项
    1.商品服务的事件发布任务
    2.不需在文档站点测试
**/
    void publishEventMsg()
}(group="Scheduler")
```

为发布任务服务提供以下实现模版

```
==> task/GoodsScheduledServiceImpl.scala
@ScheduledTask
class GoodsScheduledServiceImpl extends GoodsScheduledService{
  @Resource(name = "messageTask")
  var msgScheduler: MsgPublishTask = _

  @ScheduledTaskCron(cron = "*/2 * * * * ?")
  override def publishEventMsg(): Unit = {
    //EventBus已经处理了多节点同时触发的问题了
    msgScheduler.doPublishMessages()
  }
}
```

### 关键性的bean配置

- 所有的事件消息，最终都会发送到 kafka 的队列中，等待订阅者消费；所以每一个配置都将必不可少。
   `==> spring/services.xml` 

```
<!--messageTask 事件发布任务bean-->
<bean id="messageTask" class="com.today.eventbus.scheduler.MsgPublishTask">
    <constructor-arg name="topic" value="${KAFKA_TOPIC}"/>
    <constructor-arg name="kafkaHost" value="${KAFKA_PRODUCER_HOST}"/>
    <constructor-arg name="tidPrefix" value="${KAFKA_TID_PREFIX}"/>
    <constructor-arg name="dataSource" ref="tx_goods_dataSource"/>
</bean>
```

-  `topic` kafka 消息 topic，领域区分(建议:领域_版本号_event)
-  `kafkaHost` kafka 集群地址(如:127.0.0.1:9091,127.0.0.1:9092)
-  `tidPrefix` kafka 事务 id 前缀，领域区分
-  `dataSource` 使用业务的 dataSource

```
==> config_user_service.properties
# event config
KAFKA_TOPIC=goods_1.0.0_event
KAFKA_PRODUCER_HOST=127.0.0.1:9092
KAFKA_TID_PREFIX=goods_0.0.1
```



### 事件的触发

- 在做事件触发前,你需要实现 `AbstractEventBus` ,并将其交由 spring 托管，来做好自定义的本地监听分发

```
==> commons/EventBus.scala
object EventBus extends AbstractEventBus {

  /**
    * 事件在触发后，可能存在领域内的订阅者，以及跨领域的订阅者
    * 领域内的订阅者可以通过实现该方法进行分发
    * 同时,也会将事件发送到其他领域的事件消息订阅者
    * @param event
    */
  override def dispatchEvent(event: Any): Unit = {}
  override def getInstance: EventBus.this.type = this
}
==> spring/services.xml
<bean id="eventBus" class="com.today.service.commons.EventBus" factory-method="getInstance">
    <property name="dataSource" ref="tx_goods_dataSource"/>
</bean>
```

- 事件发布
- 之前的配置都是一劳永逸的。之后的所有事件发布都会和下面的一行代码一样简单！

```
EventBus.fireEvent(RegisteredEvent(event_id,user.id))
```

## 订阅你感兴趣的事件

### 对于领域内订阅者

`EventBus` 的 `dispatchEvent` 方法提供领域内订阅者的事件分发，以便本地订阅者可以订阅到关注的事件消息。这些领域内的订阅者，只需要在 `dispatchEvent` 中模式匹配进行分发。是不是已经是相当的简洁呢？

```
...
 override def dispatchEvent(event: Any): Unit = {
     event match {
      case e:SkuPriceUpdateApprovedEvent =>
        // do somthing 
      case _ =>
        LOGGER.info(" nothing ")
 }
...
```

### 对于跨领域的订阅者

#### 依赖

- 针对其他领域服务及第三方系统，提供了一致的 api。

```
<!--if => maven project-->
<dependency>
    <groupId>com.today</groupId>
    <artifactId>event-bus_2.12</artifactId>
    <version>0.1-SNAPSHOT</version>
</dependency>
<!--if => sbt project--> 
"com.today" % "event-bus_2.12" % "0.1-SNAPSHOT"
```

- 注解扫描支持配置：

```
<bean id="postProcessor" class="com.today.eventbus.MsgAnnotationBeanPostProcessor"/>
```

- 订阅事件消息
   同一个领域的事件在同一个消费者类中处理

```
// java
@KafkaConsumer(groupId = "goodsEventConsumer", topic = "goods_1.0.0_event",kafkaHostKey = "kafka.consumer.host"))
public class GoodsEventsConsumer {
    @KafkaListener(serializer = SkuAttributeUpdateApprovedEventSerializer.class)
    public void subscribeSkuAttributeUpdateApprovedEvent(SkuAttributeUpdateApprovedEvent event) {
        System.out.println(event.skuId);
        // do somthing
    }
    ...
}
//scala
serializer = classOf[RegisteredEventSerializer]
...
```

-  `GoodsEventsConsumer` 也需要在 spring 上下文中托管。

> @KafkaConsumer

- groupId   kafka 消费者 groupId ，订阅者领域区分

- topic     订阅的 kafka 消息 topic

- kafkaHostKey  可自行配置的 kafka 地址，默认值为

  ```
  dapeng.kafka.consumer.host
  ```

  。可以自定义以覆盖默认值 

  - 用户只要负责把这些配置放到 env 或者 properties 里面
  - 如：`System.setProperty("kafka.consumer.host","127.0.0.1:9092");` 

> @KafkaListener

- serializer 事件消息解码器，由事件发送方提供.

## 领域内消费事件与跨领域消费事件的异同

通过以上已经知道在事件中，存在领域内的订阅者消费事件消息，也可能存在跨领域的事件订阅者消费事件消息。这两者有何异同？

- 领域内的事件订阅者，通常是不能脱离领域的存在，存在领域内强关系的。但又需要解藕！可以用领域内消费事件。eventbus 领域内的消息分发订阅，就可以保证这种强关系一致存在
- 在 eventbus 中，领域内消费事件之后还是会将事件消息广播出去。因为不能保证不存在其他领域的事件订阅者！
- 而跨领域的事件消息订阅，通常是需要保证最终一致性即可，或者是弱关联其他领域的，他们对于事件发送发送方没有强依赖关系，只需要得到自己需要的东西就可以。

> 比如商品领域的商品变价审核通过后，广播了一个事件。将商品id发送给关注此事的领域，这与领域内部是没有关联的，甚至于发送者并不知道事件消息订阅者如何处理。而订阅者也只想得到这部分信息而已！

## 订阅业务数据库binlog消息

将订阅者 api 进行了有趣的拓展，和事件消息类似的。加入新的点子来做 binlog 的订阅处理。

```
  @BinlogListener
    public void binlogListener(List<BinlogEvent> events){
        // do somthing
    }
```

## 总结

总体来说，不论是事件的发送还是订阅，对于开发者而言都是易用的。并且没有多余的配置。对于第三方系统的支持也做的非常优秀，希望在日常开发中能够更加灵活的运用。尽量减少不必要的耦合！并能经受实践考验！

- 原文地址: [http://git66.com/2018/03/10/DDD-event-bus/](https://link.jianshu.com?t=http%3A%2F%2Fgit66.com%2F2018%2F03%2F10%2FDDD-event-bus%2F) 
- 同步简书地址: <https://www.jianshu.com/p/88abce9326aa> 

> 有关eventBus的具体实现细节，将由小伙伴 hz.lei 来进行剖析！

- hz.lei: [DDD-事件总线实现架构原理分析](https://www.jianshu.com/p/f89741097113)

作者：尴尬年代的尴尬人儿

链接：https://www.jianshu.com/p/88abce9326aa

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。