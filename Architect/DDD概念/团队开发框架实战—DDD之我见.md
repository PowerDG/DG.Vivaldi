​          [![Nav logo](https://cdn2.jianshu.io/assets/web/nav-logo-4c7bbafe27adc892f3046e6978459bac.png)](https://www.jianshu.com/)                    

​           [![120](https://upload.jianshu.io/users/upload_avatars/686440/7ddf6469-07dc-4212-9973-5b830be3bb39.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/120/h/120)](https://www.jianshu.com/u/b2372a42e28d)         

 

​         

​       









# 团队开发框架实战—DDD之我见

​             ![96](https://upload.jianshu.io/users/upload_avatars/1845730/fcc5c83665a9.png?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) 

​             [Bobby0322](https://www.jianshu.com/u/99ee95856388)                          

​                                                    0.4                                                 2016.10.13 15:21*               字数 4605             阅读 7519评论 4喜欢 41

# Evans DDD

- 2004年Eric Evans 发表《Domain-Driven Design –Tackling Complexity in the Heart of Software》 （领域驱动设计 ）简称Evans DDD
- 领域建模是一种艺术的技术，它是用来解决复杂软件快速应付变化的解决之道

**领域驱动设计之父**
 



![img](https://upload-images.jianshu.io/upload_images/1845730-9c349f583fb5aea6.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/689)

领域驱动设计之父.png



**领域模型相关领导人物**
 



![img](https://upload-images.jianshu.io/upload_images/1845730-7fd4799ffa8cce94.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/654)

领域模型相关领导人物.png



# 分析设计发展的三个阶段

- 第一阶段：围绕**数据库的驱动设计**，新项目总是从设计数据库及其字段开始。
- 第二层次：面向对象的分析设计方法诞生后，有了专门的分析和设计阶段之分，**分析阶段和设计阶段是断裂的**。
- 第三阶段：融合了**分析阶段和设计阶段的领域驱动设计**（Evans: DDD）。

## 第一阶段：传统的数据库方式

过去软件系统分析设计总是从数据库开始，这种围绕数据库分析设计的缺点非常明显：

- 分析方面：不能迅速有效全面分析需求。
- 设计方面：导致过程化设计编程，丧失了面向对象设计的优点。
- 运行方面：导致软件运行时负载集中在数据库端，系统性能难于扩展，闲置了中间件J2EE服务器处理性能。
     **对象和关系数据库存在阻抗，本身是矛盾竞争的**。

## 第二阶段：分析和设计分裂

第二阶段比第一阶段进步很多，开始采取面向对象的方法来分析设计需求。
 分析人员的职责：是负责从需求领域中收集基本概念。面向需求。
 设计人员的职责：必须指明一组能北项目中适应编程工具构造的组件，这些组件必须能够在目标环境中有效执行，并能够正确解决应用程序出现的问题
 **两个阶段目标不一致，导致分裂，项目失败**。

## 新阶段：分析设计统一语言

统一领域模型，它**同时满足分析原型和软件设计**，如果一个模型实现时不实用，重新寻找新模型。
 一个无处不在(ubiquitous)的语言，项目中所有人统一交流的语言。
 减少沟通疑惑，减少传达走样。使得软件更加适合需求。

# 概念、价值、重点、范围、好处

**概念**：一种模型驱动设计（MDD） ，强调分析和设计不分离，一个领域模型体现分析与设计的结果
 **过程**：业务需求->领域建模，领域模型->编码实现
 **价值**：模型实现业务需求，反应业务核心价值
 **重点**：领域建模，深入分析业务需求是关键
 **范围**：长期维护、有价值、业务复杂的系统
 **好处**：

- 有效防止最终代码实现的走样
- 模型封装状态与行为，无外部依赖，单元测试保证正确性
- 业务逻辑实现代码：集中、无重复、健壮、维护性好
- 快速应对需求变化
- 模型可以被重用，模型积累
- 模型直接表达业务需求，基于模型沟通更方便

# 领域建模前要理解的点

- 侧重点：对行为结果的建模，将对象理解为某个事实的结果，从事实观的角度去理解对象；不同于行为驱动开发（BDD），BDD基于：Data、Context、Interaction三个要素，从对象扮演不同角色参与交互活动的角度进行分析和建模
- 模型的目的：是为了实现用户需求，而不是实现用户与系统的交互过程，所以模型中不包含系统使用者，即用户；需要区分：行为驱动者、行为参与者、用户的关系
- 思考问题出发点：不是思考用户有哪些职责行为，也不是思考用户如何与系统交互，而是思考系统需要实现哪些需求，有哪些业务场景，每个业务场景会涉及哪些对象，这些对象如何协作完成业务场景

# 领域建模分析思路

- 先努力尽量全面深入理解需求
- 找出业务场景，画出用例图
- 从结构以及行为两个角度进行分析，找出所有你能想到的对象，列出这些对象
- 分析哪些对象需要建模，哪些不需要
- 分析有哪些**聚合**，确定聚合边界
- 分析聚合内对象的关系、聚合之间的关系
- 分析历史模型
- **分析聚合之间如何协作，发现领域服务**
- 分析模型可重用性

# 领域建模步骤参考

- 先从需求中考虑一些业务场景，和领域专家交谈场景的过程，从中识别出一些明显的领域概念，以及它们的关联，关联可以暂时没有方向但需要有（1：1，1：N，M：N）这些关系；可以用文字精确的没有歧义的描述出每个领域概念的涵义以及包含的主要信息；
- 根据上面分析得到的领域概念建立一个初步的领域模型
- 分析主要的软件应用程序功能，识别出主要的应用层的类；这样有助于及早发现哪些是应用层的职责，哪些是领域层的职责；
- 进一步分析领域模型，**识别出哪些是实体，哪些是值对象，哪些是领域服务**；
- 分析关联，通过对业务的更深入分析以及各种软件设计原则及性能方面的权衡，明确关联的方向或者去掉一些不需要的关联；
     **找出聚合边界及聚合根，这是一件很有难度的事情**；因为你在分析的过程中往往会碰到很多模棱两可的难以清晰判断的选择问题，所以，需要我们平时一些分析经验的积累才能找出正确的聚合根；
- 为**聚合根分配仓储，为每个聚合分配一个仓储，此时只要设计好仓储的接口**即可；
- 走查场景，确定我们设计的领域模型能够有效地解决业务需求；
- 考虑如何创建领域实体或值对象，是通过工厂还是直接通过构造函数；
- 停下来重构模型。寻找模型中觉得有些疑问或者是蹩脚的地方，比如思考一些对象应该通过关联导航得到还是应该从仓储获取？聚合设计的是否正确？考虑模型的性能怎样，等等；

# 分层架构



![img]()

领域驱动设计标准分层架构.png

| 层             | 职责                                                         |
| -------------- | ------------------------------------------------------------ |
| User Interface | 负责向用户展现信息以及解释用户命令。                         |
| Application    | 很薄的一层，定义软件要完成的所有任务。对外为展现层提供各种应用功能（包括查询或命令），对内调用领域层（领域对象或领域服务）完成各种业务逻辑，应用层不包含业务逻辑，但包含流程控制逻辑。 |
| Domain         | 负责表达业务概念，业务状态信息以及业务规则，领域模型处于这一层，是业务软件的核心。 |
| Infrastructure | 为其他层提供通用的技术能力，通过架构和框架来支持其他层的各种技术需求。如提供持久化领域对象的支持。 |

# Important Tips

- 在领域驱动设计中要先**设计领域模型**，接着写Domain逻辑，至于数据库，**仅仅是用来存储数据的工具**。使用**database first**那不叫领域驱动设计，很明显你先设计的表结构，所以应该叫数据库驱动设计更为准确。更不要引入数据库独有的技术，例如触发器，存储过程等。数据库除了存储数据外，其余一切逻辑都是Domain逻辑。
- 在领域驱动设计中，当领域层的代码完成后，领域专家查看的时候，不会看领域层，而是**直接看单元测试中的代码**，因为领域专家不懂代码，并且他也不懂你是如何实现的，它关心的是我该如何使用它？我想要的业务操作，你有没有完全实现？单元测试就是最好的体现。
- DDD 倾向于**“测试先行，逐步改进”**的设计思路。测试代码本身便是通用语言在程序中的表达，在开发人员的帮助下，领域专家可以阅读测试代码来检验领域对象是否满足业务需求。
- 领域事件一般**没有返回值的设计**，它只是去**通知事件订阅者**执行，**并不一定需要事件订阅者返回结果给它**，那我们如果判断是否执行正确呢？就是通过**异常判断**，如果领域事件发生异常，后面的操作也将不会正常执行。
- 在解决方案中，我们可以看到只有**领域层、基础设施层和领域层单元测试**的项目，并没有应用层和表现层的实现，但到目前为止，我们似乎把整个系统都完成了一样，这种感觉是很美妙的，领域模型在我手心中，任你是  Web 实现或者 WebApi 实现，又或者是其他技术框架，我都不怕，一切都是自然而然的工作，所以，关于后面的实现，你也可以交给其他人去完成，**地基由我奠基，盖楼你来完成**。
- Unit Of Work的职责只有一个，就是**负责搜集所有的更改，并提供一次性提交更改的能力**。工作单元接口（IUnitOfWork）只有一个方法：Commit；UnitOfWork的定义：维护对象状态，统一提交更改。第一句指的RegisterNew、RegisterDirty、RegisterClean、RegisterDeleted等，第二句指的是Commit，现在IUnitOfWork只有一个Commit。对应用层服务，只需要暴露Commit方法即可，Rollback其实可以去掉，因为Commit里内部完全可以自动回滚的。而那4个register方法，不是给应用层服务看到的，**而是给仓储基类使用的**。 [UnitOfWork 的定义](https://link.jianshu.com?t=http://martinfowler.com/eaaCatalog/unitOfWork.html) 
- 经典DDD中是在应用层服务中调用领域服务，**CQRS架构**则是在**command handler**中调用领域服务。
-  **DDD+TDD**，我个人非常推崇这种开发方式。
- IRepository与IDAL的本质区别，应该是领域需要什么，定义什么，否则搞出个CURD的东西来，还不如直接ORM。
- 如果用领域对象来解决查询问题，肯定很别扭的。**CQRS才是让领域层摆脱查询负担的终极方法。**效果很好，模型很纯净，查询很轻松自由高性能；**查询我直接用dapper**，灵活高性能，想怎么查就怎么查，想返回什么字段就返回什么字段；模型用**DDD+Event Sourcing**；
- 领域服务的存在，一定是由于光用**聚合本身**封装不了当前业务场景的完整业务逻辑的时候，才需要引入。比如**一次修改多个聚合根**，或者注册用户是要判断用户名唯一性，等；而像你这个，假如注册用户就是简单的new  user，那我觉得没必要设计领域服务，只要在应用层userRepository.Add(new  User())即可；而userRepository.Add(user),  orderRepository.Add(order)这种应该在应用层出现，目的是为了持久化领域聚合根；
- Repository 不进行持久化，**应用层会调用Unit Of Work去做**。 **在领域层中用到的Repository应该都是读的方法，而所有写的方法比如说Add, Update 都应该被提到应用层去操作**，对么？ 这样做可以让领域层更纯洁，只处理领域实体和相关的业务，持久化就给应用层来做。
- EF 中的Context 是**Unit Of Work** 的概念，而IDbSet 就是Repository的概念。
- 架构分层思想也是一种**分离关注点的思想**，每一层只关注自己的职责，层与层之间通过接口耦合，我觉得没什么不妥；OO的本质是强调消息发送，但是消息不一定是一定要发送到消息队列，然后对方接收才叫消息发送和接收；a直接调用b的方法也是一种消息传递的表达，只不过此时消息发送者和接收者是强耦合的；
- 因为要注册一个账户，所以要在Domain中添加一个UserService。这种做法有点让我费解，那岂不是增加一个产品类还要建一个CategoryService，增加一个产品要加一个ProductService这种注册其实是添加用户的行为，**领域模型专注于业务模型**，注册其实是新产生一个用户。我觉得UserService Register操作直接返回一个用户实体会更合理一些（不持久化）。
- 在领域层那里不使用repository的更新类操作（即Insert/Update/Delete)，**只使用查询类操作即（GetById,或者是Get)**。把所有的更新类操作都放到应用层，这样由应用层去决定什么时候把实体更新到repository，以及什么时候去提交到数据库中。
- IRepository正如它的名字一样，它就像一个容器，允许我们把东西放进去或者取出来，**它离真正的数据库还有一步之遥，并且通过Unit Of Work**，把对事务以及持久化的控制都交到了外面。而不是像DAL那样直接就反映到数据库中去了。IRepository解除了领域层对基础设施层的依懒，这个也是大家经常提到了Repository的优点之一。但是未必这一点一定非得需要IRepository，把IDAL接口移个位置同样也可以实现，不信您看看洋葱架构。
- 领域驱动不会使用任何ORM，因为数据库只是输出输出存储设备，和领域资产没有所谓的对应关系，**领域资产凌驾于所有应用程序之上**，他不会因为什么数据结构啊，表单啊，表格的不同而影响其通用性。
- 真正的DDD其实应该连数据如何持久化和用什么ORM都不应该关心，他完全是对一个领域的需求抽象出通用模型，**对通用业务逻辑的抽象实现**。对持久化技术之类的，只提供通用接口。你对一个领域了解的越多，能抽象的模型和服务越多。所以关健在于领域知识，和DDD的基本知识。DDD设计出来的东西必须应用到具体业务中才能有真正的实现。

# 更多资料和资源

- [我眼中的领域驱动设计](https://link.jianshu.com?t=http://www.cnblogs.com/richieyang/p/5373250.html)
- [DDD 领域驱动设计－如何 DDD？](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/how-to-implement-ddd.html)
- [DDD 领域驱动设计－如何完善 Domain Model（领域模型）？](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/how-to-improve-domain-model.html)
- [DDD 领域驱动设计－如何控制业务流程？](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/how-to-control-the-business-processes.html)
- [DDD 领域驱动设计－领域模型中的用户设计](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/domain-model-with-user-design.html)
- [DDD 领域驱动设计－两个实体的碰撞火花](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/ddd-design-two-entities.html)
- [DDD 领域驱动设计－谈谈 Repository、IUnitOfWork 和 IDbContext 的实践（1）](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/ddd-repository-iunitofwork-and-idbcontext.html)
- [DDD 领域驱动设计－谈谈 Repository、IUnitOfWork 和 IDbContext 的实践（2）](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/ddd-repository-iunitofwork-and-idbcontext-part-2.html)
- [DDD 领域驱动设计－谈谈 Repository、IUnitOfWork 和 IDbContext 的实践（3）](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/ddd-repository-iunitofwork-and-idbcontext-part-3.html)
- [关于Repository、IUnitOfWork 在领域层和应用服务层之间的代码分布与实现](https://link.jianshu.com?t=http://www.cnblogs.com/zuowj/p/4884947.html)
- [**工作单元模式(UnitOfWork)学习总结**](https://link.jianshu.com?t=http://www.cnblogs.com/GaoHuhu/p/3443145.html)
- [**关于MVC EF架构及Repository模式的一点心得**](https://link.jianshu.com?t=http://www.cnblogs.com/crazyboy/p/4895256.html)
- [Repository 返回 IQueryable？还是 IEnumerable？](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/repository-return-iqueryable-or-ienumerable.html)
- [开发笔记：基于EntityFramework.Extended用EF实现指定字段的更新](https://link.jianshu.com?t=http://www.cnblogs.com/dudu/p/4735211.html)
- [领域驱动设计案例：Tiny Library：领域模型](https://link.jianshu.com?t=http://www.cnblogs.com/daxnet/archive/2010/10/27/1862752.html)
- [**领域驱动设计之单元测试最佳实践(一)**](https://link.jianshu.com?t=http://www.cnblogs.com/richieyang/p/5451440.html)
- [**初探领域驱动设计（1）为复杂业务而生**](https://link.jianshu.com?t=http://www.cnblogs.com/jesse2013/p/the-first-glance-of-ddd.html)
- [**初探领域驱动设计（2）Repository在DDD中的应用**](https://link.jianshu.com?t=http://www.cnblogs.com/jesse2013/p/ddd-repository.html)
- [DDD领域驱动设计实践篇之如何提取模型](https://link.jianshu.com?t=http://www.cnblogs.com/liubiaocai/p/3910903.html)
- [DDD领域驱动设计之聚合、实体、值对象](https://link.jianshu.com?t=http://www.cnblogs.com/liubiaocai/p/3938192.html)
- [DDD领域驱动设计之领域基础设施层](https://link.jianshu.com?t=http://www.cnblogs.com/liubiaocai/p/3938230.html)
- [DDD领域驱动设计之领域服务](https://link.jianshu.com?t=http://www.cnblogs.com/liubiaocai/p/3938259.html)
- [DDD领域驱动设计之运用层代码](https://link.jianshu.com?t=http://www.cnblogs.com/liubiaocai/p/3938275.html)
- [Unity依赖注入使用详解](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/3670292.html)
- [一种简单的CQRS架构设计及其实现](https://link.jianshu.com?t=http://www.cnblogs.com/richieyang/p/5636434.html)
- [dax.net](https://link.jianshu.com?t=http://www.cnblogs.com/daxnet/)
- [**汤雪华的博客**](https://link.jianshu.com?t=http://www.cnblogs.com/netfocus/)
- [一年的回顾：我的领域驱动设计之路](https://link.jianshu.com?t=http://www.cnblogs.com/xishuai/p/ddd-one-year.html)

# 参考框架

-  [Apworks](https://link.jianshu.com?t=https://github.com/daxnet/Apworks)
     Apworks is a flexible, scalable, configurable and efficient .NET based  application development framework that helps software developers to  easily build enterprise applications by applying either Classic Layering  or Command-Query Responsibility Segregation (CQRS) architectural  patterns.
-  [enode](https://link.jianshu.com?t=https://github.com/tangxuehua/enode)
     ENode is a framework aims to help us developing ddd, cqrs, eda, and event sourcing style applications.
-  [基于asp.net mvc + DDD 构架的开源.net cms系统.](https://link.jianshu.com?t=https://github.com/jsix/cms)
     一个基于DDD的开源项目,各种技术！

# 参考书籍

- [《企业应用架构模式》，Martin Fowler著，机械工业出版社](https://link.jianshu.com?t=https://book.douban.com/subject/1230559/)
- [《敏捷软件开发 : 原则、模式与实践》，Robert C.Martin Micah Martin著，人民邮电出版社](https://link.jianshu.com?t=https://book.douban.com/subject/2347790/)
- [《领域驱动设计—软件核心复杂性应对之道》，Eric Evans著，人民邮电出版社](https://link.jianshu.com?t=https://book.douban.com/subject/5344973/)
- [《实现领域驱动设计》，Vaughn Vernon著，电子工业出版社](https://link.jianshu.com?t=https://book.douban.com/subject/25844633/)
- [《领域驱动设计C# 2008实现:问题·设计·解决方案》，Tim McCarthy著，清华大学出版社](https://link.jianshu.com?t=https://book.douban.com/subject/4734187/)
- [《领域驱动设计与模式实战》，Jimmy Nilsson著，人民邮电出版社](https://link.jianshu.com?t=https://book.douban.com/subject/4058874/)
- [《Microsoft .NET企业级应用架构设计》，Dino等编著，人民邮电出版社](https://link.jianshu.com?t=https://book.douban.com/subject/4870838/)
- [《C#企业应用开发艺术—CSLA.NET 框架开发实战》，Rockford Lhotka著，人民邮电出版社](https://link.jianshu.com?t=https://book.douban.com/subject/4291405/)

小礼物走一走，来简书关注我



​                      团队开发框架 

​           © 著作权归作者所有         

​           举报文章         

​             [               ![96](https://upload.jianshu.io/users/upload_avatars/1845730/fcc5c83665a9.png?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) ](https://www.jianshu.com/u/99ee95856388)            

Bobby0322

写了 382278 字，被 541 人关注，获得了 877 个喜欢

Swifter,很高兴能和大家一起探索Swift开发的...



 

​                                      [                    ](javascript:void((function(s,d,e,r,l,p,t,z,c){var%20f='http://v.t.sina.com.cn/share/share.php?appkey=1881139527',u=z||d.location,p=['&url=',e(u),'&title=',e(t||d.title),'&source=',e(r),'&sourceUrl=',e(l),'&content=',c||'gb2312','&pic=',e(p||'')].join('');function%20a(){if(!window.open([f,p].join(''),'mb',['toolbar=0,status=0,resizable=1,width=440,height=430,left=',(s.width-440)/2,',top=',(s.height-430)/2].join('')))u.href=[f,p].join('');};if(/Firefox/.test(navigator.userAgent))setTimeout(a,0);else%20a();})(screen,document,encodeURIComponent,'','','', '推荐 Bobby0322 的文章《团队开发框架实战—DDD之我见》（ 分享自 @简书 ）','https://www.jianshu.com/p/f4f40c7dd7fc?utm_campaign=maleskine&utm_content=note&utm_medium=reader_share&utm_source=weibo','页面编码gb2312|utf-8默认gb2312'));)                                                   [更多分享](javascript:void(0);)       

![Web note ad 1](https://cdn2.jianshu.io/assets/web/web-note-ad-1-c2e1746859dbf03abe49248893c9bea4.png)



- 
-  
- 
- 
-  

被以下专题收入，发现更多相似内容

收入我的专题



领域与业务建模



DDD



从码农到CTO



银行间市场业务...



技术架构



​               ![240](https://upload-images.jianshu.io/upload_images/12413187-b2d754c15ca3139f.png?imageMogr2/auto-orient/strip|imageView2/1/w/300/h/240) 

MVC与JavaEE开发

【目录】1 什么是MVC2 Java EE开发  2.1  JavaWeb开发：Servlet+JSP+Javabean   2.2 JavaEE开发：三层架构与常用框架3  MVC和JavaEE三层架构的关系4 给新手的三个小tips 1 什么是MVC MVC是Model(模...

​                 ![48](https://upload.jianshu.io/users/upload_avatars/12413187/9c09fbe1-de13-4356-a782-f1e8f78a80bc?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

花无缺_0159

Token+Session实现不同后台系统单点登录

1.需求描述  有两个后台系统A、B。现在需要A系统的后台用户，登录A系统之后，能够访问B系统的公告功能。针对这个需求有两种解决方案：1>将A系统的所有用户接入B系统中；（开发工作量大，业务逻辑复杂）；2>采用Token+Session方式，实现A系统的后台用户单点登录（开发...

​                 ![48](https://upload.jianshu.io/users/upload_avatars/8877984/3329dca9-0f13-4257-83f1-e68ee93ff64c?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

享受孤独_2ae4

​               ![240](https://upload-images.jianshu.io/upload_images/10574922-4ed5e96f0392d39d.png?imageMogr2/auto-orient/strip|imageView2/1/w/300/h/240) 

（三）从0开始写框架—可靠消息事务最终一致性

回顾    上一篇文章我们说到，各种分布式事务解决方案的特点，其中最后提到了可靠消息事务最终一致性这种解决方案，而我们这篇文章的标题也是它，没错，我们接下来要详细的分析该解决方案的实现细节了，上一篇文章在介绍该解决方案时，已经说了那个执行流程分析图，仅仅只是一个粗略图而已，实...

​                 ![48](https://upload.jianshu.io/users/upload_avatars/10574922/e57ba2de-40ba-4f6f-a602-f180312752ed?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

LuoHaiPeng

SpringBoot实例:医院统一信息平台(资源服务oauth2)

如果资源服务是独立的服务时，需要指向认证服务，前面已经完成的用户服务就存在了认证服务。作为同一系统的一个服务，它不必作为SSO单点登录的客户端，只需要指定由谁认证。这样更合理些。SpringBoot2与SpringBoot1包含的组件有点区别。在SpringBoot1是，包...

​                 ![48](https://upload.jianshu.io/users/upload_avatars/5222486/26b986cc-d1a4-434c-b9e7-89e09a762654.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

碧波之心

​               ![240](https://upload-images.jianshu.io/upload_images/8387238-4ccb0af34274da2c.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/300/h/240) 

第七层 数据治理之模型设计

模型层次 1）数据操作层：把数据几乎无处理的放在数据仓库中 ①  同步：保存增存量的结构化数据② 结构化：把非结构化数据结构化处理并保存③ 积累历史数据：根据需求保存历史数据④ 数据清洗  2）公共维度层：存放明细事实数据、维表数据、公共指标汇总 ① 明细事实数据、维表数据从O...

​                 ![48](https://upload.jianshu.io/users/upload_avatars/8387238/8189d0a4-3bd2-4a7d-b45d-dc563f5d5f58.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

被爱的天青色

​               ![240](https://upload-images.jianshu.io/upload_images/972806-36074e3d4b606070.png?imageMogr2/auto-orient/strip|imageView2/1/w/300/h/240) 

Markdown基本语法

标题2 标题6 1.有序列表2.有序列表 无序列表 无序列表 引用的内容引用内可以嵌套标题、列表等 $str = "carisok"

​                 ![48](https://upload.jianshu.io/users/upload_avatars/972806/a0421b699a84.png?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

圣泉山人

​               ![240](https://upload-images.jianshu.io/upload_images/5320380-e3a771ec20a8a3c2.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/300/h/240) 

那田间地头，树梢房顶，都是我的青春

“本文参加#青春不一YOUNG#征稿活动，本人承诺，文章内容为原创，且未在其他平台发表过。”  青春，回忆一不小心就飞到了我小的时候。农村长大的孩子，比起现在只会抱着手机和平板玩的小孩子来说，真是幸福多了。  我们那会儿基本上都是散养，田间地头，树梢房顶，爱上哪玩上哪玩，家长都...

​                 ![48](https://upload.jianshu.io/users/upload_avatars/5320380/52ea40f8-0870-41f9-a3bc-b863b7f5acc2.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

长颈鹿的小毛鬃

穿越大唐

草泥马上班呢多看看对吧发来的你玩吧

​                 ![48](https://upload.jianshu.io/users/upload_avatars/4320833/fa2e800286f6.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/48/h/48)               

独眼龙哥