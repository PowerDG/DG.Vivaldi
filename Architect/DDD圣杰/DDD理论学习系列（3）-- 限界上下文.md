# DDD理论学习系列（3）-- 限界上下文

​             ![96](https://upload.jianshu.io/users/upload_avatars/2799767/0b0f3fb5-f8b9-4bf4-ac1d-b94468c2e1c8.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) 

​             [圣杰](https://www.jianshu.com/u/39ec0e6b1844)                          

​                                2017.05.19 13:06*               字数 1177             阅读 2970评论 5喜欢 13

> [DDD理论学习系列——案例及目录](https://www.jianshu.com/p/6e2917551e63)

------

# 1. 引言

限界上下文可以拆分为两个词，限界和上下文。
 限界：是指一个界限，具体的某一个范围。
 上下文：个人理解就是语境。

比如我们常说的段子：

> “我想静静。”
>  这个句子一般是想表达“我想静一静”的意思。但是我们却把它玩笑成“静静是谁？”。
>  可见上下文语境很重要。

这个例子只是个开胃菜，我们接着往下看。

# 2. 案例分析

> 整个应用程序之内的一个概念性边界。
>  边界之内的每种领域术语、词组或句子--也即通用语言，都有确定的上下文含义。
>  边界之外，这些术语可能表示不同的意思。

每次看到这种解释就头大。我们还是结合我们的案例来聊一聊吧。

根据上一节对领域的剖析，我们把案例主要拆分成几个子域，其中销售子域是核心域，商品子域和物流子域为支撑子域。在这三个子域中，都要和商品打交道。如果把商品抽象为Product对象的话，按我们一般的常规思路（抛开子域的划分）来说，不管是商品销售还是发货，我们都可以共用同一个Product对象。
 但在DDD中，在商品子域和销售子域中，可以共享这个Product对象，但在物流子域，就有点大材小用。为什么呢？因为毕竟物流子域关注的是商品的发货处理和物流跟踪。针对发货流程而言，我只关心商品的数量、大小、重量等规格，而不必了解商品的价格等其他信息。所以说物流子域应该关注的是货物的发货处理而不是商品。

那为什么我们之前的开发思路会共用同一个Product对象呢？
 答案很简单，没有进行领域的划分。把整个项目一概而论，统一建模导致的结果。

在DDD的思想下，当划分子域之后，每个子域都对应有各自的上下文。在销售子域和商品子域所在的上下文语境中，商品就是商品，无二义性。在物流子域的上下文语境中，我们也可以说商品的发货处理，但这时的商品就特指货物了。确定了真实面目之后，我想我们也会不由自主的抽象一个新的Cargo对象来处理物流相关的业务。这也是DDD带来的好处，让我们更清晰的建模。

# 3. 限界上下文的命名

限界上下文只是一个统一的命名，在我们划分子域后，每个子域一般对应一个上下文，也可以对应多个上下文。但如果子域对应多个上下文的时候，就要考虑一下是不是子域能否继续划分。
 命名方式很简单，领域名+上下文。
 比如我们的销售子域对应销售上下文，物流子域对应物流上下文。

# 4. 总结

通过我们上面的举例分析，限界上下文也并不是一个高深的概念。
 用官话来说限界上下文主要用来封装通用语言和领域对象。
 按我个人的理解它就是用来为领域提供上下文语境，保证在领域之内的一些术语、业务相关对象等（通用语言）有一个确切的含义，没有二义性。

# 参考资料

> [What are Bounded Contexts and Context Maps in Domain Driven Design?](https://link.jianshu.com?t=http://culttt.com/2014/11/19/bounded-contexts-context-maps-domain-driven-design)
>  In a perfect world, each bounded context has its own models/entities.  You wouldn't relate models/entities across bounded contexts. Instead,  model x from boundary y, would communicate with boundary z via some sort  of api.
>  This ensures a clear distinction between contexts and removes  dependencies. The great benefit of this is, if you have a large  application, you can segregate parts of your application to not be  critically based on other parts of the application, particularly between  businesses.

As a rather obvious example - you don't connect with Facebook's user  records - it's a completely different context. You consume the API they  provide in order to get the information you need. Think of your  application in the same way. When you consume their service, you'd  actually write a repository or something that would manage those  queries, and code in protection mechanisms in case something goes wrong.

喜欢就很好



​                      DDD理论学习 

​           © 著作权归作者所有         

​           举报文章         

​             [               ![96](https://upload.jianshu.io/users/upload_avatars/2799767/0b0f3fb5-f8b9-4bf4-ac1d-b94468c2e1c8.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) ](https://www.jianshu.com/u/39ec0e6b1844)            

圣杰



写了 195594 字，被 2917 人关注，获得了 2133 个喜欢