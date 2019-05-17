​                                                   2019年03月02日 07:00:00           [dotNET跨平台](https://me.csdn.net/sD7O95O)           阅读数：176                   

​                   

![640?wx_fmt=png](assets/p)在ABP vNext上的第一个公告之后,我们对代码库进行了很多改进(GitHub存储库上的1100多次提交).我们已经创建了功能,示例,文档等等.在这篇文章中,我想告诉你一些新闻和项目的状态.

## ABP微服务演示解决方案

ABP框架的主要目标之一是提供创建微服务解决方案的便利基础设施.

我们一直在努力开发微服务解决方案演示.初始版本已完成并文档化.该示例解决方案旨在演示一个简单而完整的微服务解决方案;

- 具有多个独立的,可自我部署的**微服务**.

- 多个**Web应用程序**,每个都使用不同的API网关.

- 使用Ocelot库开发了多个**网关** / BFF(后端为前端(Backend for Frontends)).

- 使用IdentityServer框架开发**身份验证服务**.它也是一个带有必要UI的SSO(单点登录)应用程序.

- 有**多个数据库**.一些微服务有自己的数据库,而一些服务/应用程序共享一个数据库(以演示不同的用例).

- 具有不同类型的数据库：**SQL Server**(使用**Entity Framework Core** ORM)和**MongoDB**.

- 有一个**控制台应用程序**来显示通过身份验证使用服务的最简单方法.

- 使用Redis进行**分布式缓存**.

- 使用RabbitMQ进行服务到服务(service-to-service)的**消息传递**.

- 使用Docker和Kubernates**部署**并运行所有服务和应用程序.

- 使用Elasticsearch和Kibana存储和可视化日志(使用Serilog编写).

  

有关解决方案的详细说明,请参阅其文档.

## 改进/功能

我们已经开发了许多功能,包括**分布式事件总线**(与RabbitMQ集成),**IdentityServer4集成**以及几乎所有功能的增强.我们不断重构和添加测试,以使框架更稳定和生产就绪.它正在快速增长.

## 路线图

在第一个稳定版本(v1.0)之前还有很多工作要做.您可以在GitHub仓库上看到优先的积压项目.

根据我们的估计,我们计划在2019年第二季度(可能在五月或六月)发布v1.0.所以,不用等待太长时间了.我们也对第一个稳定版本感到非常兴奋.

我们还将完善文档,因为它现在还远未完成.

第一个版本可能不包含SPA模板.但是,如果可能的话,我们想要准备一个简单些的.SPA框架还没有确定下来.备选有:**Angular,React和Blazor**.请将您的想法写为对此帖的评论.

## Abp中文网

中国有一个大型的ABP社区.他们创建了一个中文版的abp.io网站:https://cn.abp.io/. 他们一直在保持更新.感谢中国的开发人员,特别是maliming.

## NDC {London} 2019

很高兴作为合作伙伴参加NDC {London}2019 .我们已经与许多开发人员讨论过当前的ASP.NET Boilerplate和ABP vNext,我们得到了很好的反馈.

我们还有机会与Scott Hanselman和Jon Galloway交谈.他们参观了我们的展位,我们谈到了ABP vNext的想法.他们喜欢新的ABP框架的功能,方法和目标.在twitter上查看一些照片和评论:

![640?wx_fmt=png](assets/p)

由于微信不允许外部链接,你需要点击页尾左下角的“阅读原文”,才能访问文中的链接.

# 原文地址：https://cn.abp.io/blog/Abp/Microservice-Demo-Projects-Status-and-Road-Map

```

```

.NET社区新闻，深度好文，欢迎访问公众号文章汇总 http://www.csharpkit.com 
![640?wx_fmt=jpeg](assets/p)