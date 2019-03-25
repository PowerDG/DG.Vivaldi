> [DDD理论学习系列——案例及目录](https://www.jianshu.com/p/6e2917551e63)

------

# 1. 引言

> A domain event is a full-fledged part of the domain model, a representation of something that happened in the domain. Ignore irrelevant domain activity while making explicit the events that the domain experts want to track or be notified of, or which are associated with state change in the other model objects.
>  *领域事件是一个领域模型中极其重要的部分，用来表示领域中发生的事件。忽略不相关的领域活动，同时明确领域专家要跟踪或希望被通知的事情，或与其他模型对象中的状态更改相关联。*

针对官方释义，我们可以理出以下几个要点：

1. 领域事件作为领域模型的重要部分，是领域建模的工具之一。
2. 用来捕获领域中已经发生的事情。
3. 并不是领域中所有发生的事情都要建模为领域事件，要忽略无业务价值的事件。
4. 领域事件是领域专家所关心的（需要跟踪的、希望被通知的、会引起其他模型对象改变状态的）发生在领域中的一些事情。

简而言之，领域事件是用来捕获领域中发生的具有业务价值的一些事情。它的本质就是事件，不要将其复杂化。在DDD中，领域事件作为通用语言的一种，是为了清晰表述领域中产生的事件概念，帮助我们深入理解领域模型。

# 2. 认识领域事件

> 当用户在购物车点击结算时，生成待付款订单，若支付成功，则更新订单状态为已支付，扣减库存，并推送捡货通知信息到捡货中心。

在这个用例中，“订单支付成功”就是一个领域事件。

考虑一下，在你没有接触领域事件或EDA（事件驱动架构）之前，你会如何实现这个用例。肯定是简单直接的方法调用，在一个事务中分别去调用状态更新方法、扣减库存方法、发送捡货通知方法。这无可厚非，毕竟之前都是这样干的。

那这样设计有什么问题？

1. 试想一下，若现在要求支付成功后，需要额外发送一条付款成功通知到微信公众号，我们怎么实现？想必我们需要额外定义发送微信通知的接口并封装参数，然后再添加对方法的调用。这种做法虽然可以解决需求的变更，但很显然不够灵活耦合性强，也违反了OCP。
2. 将多个操作放在同一个事务中，使用事务一致性可以保证多个操作要么全部成功要么全部失败。在一个事务中处理多个操作，若其中一个操作失败，则全部失败。但是，这在业务上是不允许的。客户成功支付了，却发现订单依旧为待付款，这会导致纠纷的。
3. 违反了聚合的一大原则：在一个事务中，只对一个聚合进行修改。在这个用例中，很明显我们在一个事务中对订单聚合和库存聚合进行了修改。

那如何解决这些问题？我们可以借助领域事件的力量。

1. 解耦，可以通过发布订阅模式，发布领域事件，让订阅者自行订阅；
2. 通过领域事件来达到最终一致性，提高系统的稳定性和性能；
3. 事件溯源；
4. 等等。

下面我们就来一一深入。

# 3.建模领域事件

如何使用领域事件来解耦呢？
 当然是封装不变，应对万变。那针对上面的用例，不变的是什么，变的又是什么？不变的是订单支付成功这个事件；变化的是针对这个事件的不同处理手段。

而我们要如何封装呢？
 这时我们就要理清事件的本质，事件有因必有果，事件是由事件源和事件处理组合而成的。通过事件源我们来辨别事件的来源，事件处理来表示事件导致的下一步操作。



![img](https:////upload-images.jianshu.io/upload_images/2799767-ecd81e1865814899.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)



## 3.1. 抽象事件源

事件源应该至少包含事件发生的时间和触发事件的对象。我们提取`IEventData`接口来封装事件源：

```
/// <summary>
/// 定义事件源接口，所有的事件源都要实现该接口
/// </summary>
public interface IEventData
{
    /// <summary>
    /// 事件发生的时间
    /// </summary>
    DateTime EventTime { get; set; }

    /// <summary>
    /// 触发事件的对象
    /// </summary>
    object EventSource { get; set; }
}
```

通过实现`IEventData`我们可以根据自己的需要添加自定义的事件属性。

## 3.2. 抽象事件处理

针对事件处理，我们提取一个`IEventHandler`接口：

```
 /// <summary>
 /// 定义事件处理器公共接口，所有的事件处理都要实现该接口
 /// </summary>
 public interface IEventHandler
 {
 }
```

事件处理要与事件源进行绑定，所以我们再来定义一个泛型接口：

```
 /// <summary>
 /// 泛型事件处理器接口
 /// </summary>
 /// <typeparam name="TEventData"></typeparam>
 public interface IEventHandler<TEventData> : IEventHandler where TEventData : IEventData
 {
     /// <summary>
     /// 事件处理器实现该方法来处理事件
     /// </summary>
     /// <param name="eventData"></param>
     void HandleEvent(TEventData eventData);
 }
```

以上，我们就完成了领域事件的抽象。在代码中我们通过实现一个`IEventHandler<T>`来表达领域事件的概念。

## 3.3. 领域事件的发布和订阅

领域事件不是无缘无故产生的，它有一个发布方。同理，它也要有一个订阅方。

那如何和订阅和发布领域事件呢？
 领域事件的发布可以使用**发布--订阅模式**来实现。而比较常见的实现方式就是**事件总线**。



![img](https:////upload-images.jianshu.io/upload_images/2799767-6f44bdefa88a23a2.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/987)



事件总线是一种集中式事件处理机制，允许不同的组件之间进行彼此通信而又不需要相互依赖，达到一种解耦的目的。Event Bus就相当于一个介于Publisher（发布方）和Subscriber（订阅方）中间的桥梁。它隔离了Publlisher和Subscriber之间的直接依赖，接管了所有事件的发布和订阅逻辑，并负责事件的中转。

这里就简要说明一下事件总线的实现的要点：

1. 事件总线维护一个事件源与事件处理的映射字典；
2. 通过单例模式，确保事件总线的唯一入口；
3. 利用反射或依赖注入完成事件源与事件处理的初始化绑定；
4. 提供统一的事件注册、取消注册和触发接口。

最后，我们看下事件总线的接口定义：

```
public interface IEventBus
 {
    void Register < TEventData > (IEventHandler eventHandler);

    void UnRegister < TEventData > (Type handlerType) where TEventData: IEventData;

    void Trigger < TEventData > (Type eventHandlerType, TEventData eventData) where TEventData: IEventData;
}
```

在应用服务和领域服务中，我们都可以直接调用`Register`方法来完成领域事件的注册，调用`Trigger`方法来完成领域事件的发布。

而关于事件总线的具体实现，可参考我的这篇博文——[事件总线知多少](https://www.jianshu.com/p/22fbe7a7c120)。

# 4. 最终一致性

说到一致性，我们要先搞明白下面几个概念。

**事务一致性**
 事务一致性是是数据库事务的四个特性之一，也就是ACID特性之一：

> **原子性（Atomicity）**：事务作为一个整体被执行，包含在其中的对数据库的操作要么全部被执行，要么都不执行。
>  **一致性（Consistency）**：事务应确保数据库的状态从一个一致状态转变为另一个一致状态。
>  **隔离性（Isolation）**：多个事务并发执行时，一个事务的执行不应影响其他事务的执行。
>  **持久性（Durability）**：已被提交的事务对数据库的修改应该永久保存在数据库中。

我们用一张图来理解一下：



![img](https:////upload-images.jianshu.io/upload_images/2799767-37a2232be8fa3348.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/933)

事务一致性

在事务一致性的保证下，上面的图示只会有两个结果：

1. A和B两个操作都成功了。
2. A和B两个操作都失败了。

**数据一致性**
 举个简单的例子，假设10个人，每人有100个虚拟币，虚拟币仅能在这10人内流通，不管怎么流通，最终的虚拟币总数都是1000个，这就是数据一致性。

**领域一致性**
 简单理解就是在领域中的操作要满足领域中定义的业务规则。比如你转账，并不是你余额充足就可以转账的，还要求账户的状态为非挂失、锁定状态。

回到我们的案例，当支付成功后，更新订单状态，扣减库存，并发送捡货通知。按照我们以往的做法，为了维护订单和库存的数据一致性，我们将这三个操作放到一个应用服务去做（因为应用服务管理事务），事务的一致性可以保证要么全部成功要么全部失败。但是，复杂业务嵌套的多个操作放在一个事务中，很容易造成事务超时，而往往为了性能考虑，可能会放弃事务嵌套，这样就又很可能会导致：客户支付成功后，订单依旧为待付款状态，这会引起纠纷。另外，由于库存没有及时扣减，很可能会导致库存超卖。怎么办呢？
 将事务拆解，使用领域事件来达到最终一致性。

**最终一致性**
 “最终一致性”是一种设计方法，可以通过将某些操作的执行延迟到稍后的时间来提高应用程序的可扩展性和性能。



![img](https:////upload-images.jianshu.io/upload_images/2799767-2ade05818668d245.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/918)

最终一致性

对于常见于分布式系统的最终一致性工作流中，客户同样在系统中执行一个命令，但这个系统只为维护事务中的领域一致性运行部分的操作，剩余的操作在允许延后执行。针对上图的结果：

1. A操作执行成功，B操作将延后执行。
2. A操作失败，B操作将不会执行。

而针对我们的案例，我们如何使用领域事件来进行事务拆分呢？我们看下下面这张图你就明白了。



![img](https:////upload-images.jianshu.io/upload_images/2799767-c652cb896fdf6c4e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/989)

领域事件在最终一致性的位置

分析一下，针对我们案例，我们发现一个用例需要修改多个聚合根的情况，并且不同的聚合根还处于不同的限界上下文中。其中订单和库存均为聚合根，分别属于订单系统和库存系统。我们可以这样做：

1. 在订单所在的聚合根中更新订单支付状态，并发布“订单成功支付”的领域事件；
2. 然后库存系统订阅并处理库存扣减逻辑；
3. 通知系统订阅并处理捡货通知。

通过这种方式，我们即保证了聚合的原则，又保证了数据的最终一致性。

# 5. 事件存储和事件溯源

关于事件存储（Event Store）和事件溯源（Event Sourcing）是一个比较复杂的概念，我们这里就简单介绍下，不做过多展开，后续再设章节详述。



![img](https:////upload-images.jianshu.io/upload_images/2799767-0783b31ecaf12c07.jpg?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)



事件存储，顾名思义，即事件的持久化。那为什么要持久化事件？

1. 当事件发布失败时，可用于重新发布。
2. 通过消息中间件去分发事件，提高系统的吞吐量。
3. 用于事件溯源。

源代码管理工具我们都用过，如Git、TFS、SVN等，通过记录文件每一次的修改记录，以便我们跟踪每一次对源代码的修改，从而我们可以随时回滚到文件的指定修改版本。

事件溯源的本质亦是如此，不过它存储的并非聚合每次变化的结果，而是存储应用在该聚合上的历史领域事件。当需要恢复某个状态时，需要把应用在聚合的领域事件按序“重放”到要恢复状态对应的领域事件为止。

# 6.总结

经过上面的分析，我们知道引入领域事件的目的主要有两个，一是解耦，二是使用领域事件进行事务的拆分，通过引入事件存储，来实现数据的最终一致性。

最后，对于领域事件，我们可以这样理解：
 通过将领域中所发生的活动建模成一系列的离散事件，并将每个事件都用领域对象来表示，来跟踪领域中发生的事情。
 也可以简要理解为：**领域事件 = 事件发布 + 事件存储 + 事件分发 + 事件处理**。

以上，仅是个人理解，DDD水很深，剪不断，理还乱，有问题或见解，欢迎指正交流。

> 参考资料：
>  [在微服务中使用领域事件](https://link.jianshu.com?t=http%3A%2F%2Finsights.thoughtworkers.org%2Fuse-domain-events-in-microservices%2F)
>  [使用聚合、事件溯源和CQRS开发事务型微服务](https://link.jianshu.com?t=http%3A%2F%2Fwww.infoq.com%2Fcn%2Farticles%2Fmicroservices-aggregates-events-cqrs-part-1-richardson%3Futm_source%3Dinfoq%26utm_campaign%3Duser_page%26utm_medium%3Dlink)
>  [如何理解数据库事务中的一致性的概念？](https://link.jianshu.com?t=https%3A%2F%2Fwww.zhihu.com%2Fquestion%2F31346392%2Fanswer%2F61742840)
>  [Eventual Consistency via Domain Events and Azure Service Bus](https://link.jianshu.com?t=http%3A%2F%2Fwww.reflectivesoftware.com%2F2015%2F09%2F01%2Feventual-consistency-via-domain-events-and-azure-service-bus%2F)

作者：圣杰

链接：https://www.jianshu.com/p/c10b7fd9bec1

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。