# ABP框架的理解和总结

​                                                   2018年09月30日 15:24:49           [roadlord](https://me.csdn.net/roadlord)           阅读数：3565                   

​                   

1、使用automapper实现实体与DTO之间的映射。
 2、ABP使用Castle Windsor为整个程序框架提供依赖注入的功能。 使用log4Net日志记录组件， 提供给其他各层调用以进行日志记录。
 4、ABP的多租户模式是关闭的，我们可以在模块PreInitialize 方法中开启他。Configuration.Multitency.IsEnabled = true;
 5、如果你使用了module zero，那么你不需要关心tenant store
 6、OWIN是Open Web Server Interface for .NET的首字母缩写。是web server的规范，你可以依据OWIN规范开发自己的web server，让web应用跑在自己的web server下。
      你能使用任何你想的来替换IIS(比如：Katana或者Nowin)，并且在必要时随时升级，而不是更新操作系统。当然，如果你需要的话，你可以构建自定义的宿主和Pipeline去处理Http请求。
      这一切的改变都是由于OWIN的出现，他提供了明晰的规范以便我们快速灵活的去扩展Pipeline来处理Http请求，甚至可以不写任何一句代码来切换不同的Web Server，前提是这些Web Server遵循OWIN规范。
 7、在VS2017中集成WBP源码可以调试分析。
 8、依赖注入：构造函数注入、属性注入、依赖注入框架
 9、ASP.NET  Core已经内置了依赖注入：Microsoft.Extensions.DependencyInjection。在ASP.NET  Core中ABP使用Castle.Windsor.MsDependencyInjection实现了依赖注入。所以你不需要考虑它。
 10、☆☆会话管理：ABP提供了 IAbpSession 接口获取当前用户以及租户信息，而不是使用ASP.NET的Session。
 11、☆☆缓存管理：使用 MemoryCache 来实现了该抽象基类。它能够被任何其它的缓存类来扩展。Abp.RedisCache 包就扩展了该缓存基类。缓存的过期时间默认是60分钟。可做Redis Cache 集成。
 12、日志管理：ABP使用Castle Windsor's logging facility日志记录工具，并且可以使用不同的日志类库，比如：Log4Net, NLog, Serilog... 等等。更厉害的是，
       你还可以在客户端调用日志记录器。在客户端，ABP有对应的 javascript 日志API，这意味着你可以记录下来浏览器的日志，实现代码如下：abp.log.warn('a sample log message...'); 
 13、设置管理：ABP框架提供强大的基础架构，我们可以在服务端或者客户端设置，来存储/获取应用程序、 租户和用户级别的配置。设置的范围：Application、Tenant、User
 14、对象之间的映射：automapper集成。
 15、邮件发送：MailKit集成。
 16、实体（entity）有自己的唯一标识，而值对象（vo）是没有标识的。
 17、领域服务可以被应用服务和其它的领域服务调用，但是不可以被表现层直接调用(表现层可以直接调用应用服务)。可以结合自己画的技术路线图进行理解。
 18、工作单元：ABP默认使用 工作单元 来管理数据库连接和事务。
 19、领域事件：事件总线-定义事件-触发事件-事件处理-注册处理器-取消注册事件
 20、数据过滤器：达到检索不到逻辑删除数据的效果,需要在SQL的Where条件IsDeleted = false。这是个乏味的工作，可借助过滤器工作。
 20、☆☆规约模式：通过链接业务规则与使用boolean逻辑来重组业务规则。主要是用来对实体和其它业务对象构造可重用的过滤器。
 21、验证DTO：验证包含客户端、服务端两次。客户端侧重于表单验证，服务端验证更重要。
 22、ABP鉴权：应用层，使用Abp.Authorization.AbpAuthorize类。Web层，使用  Abp.Web.Mvc.Authorization.AbpMvcAuthorize类。在ASP.NET Web API中，使用  Abp.WebApi.Authorization.AbpApiAuthorize特性。
 23、ABP功能管理：Saas应用都有不同功能的版本。可以给租户提供不同的价格和功能选项。
 24、审计日志：I使用IAuditingStore接口来保存审计信息。module-zero项目是这个接口的完整实现，如果你不想自己实现这个接口，SimpleLogAuditingStore类可以直接拿来使用，它是实现方式是将审计信息写入日志中。
 25、实体历史：ABP 提供了一个基础设施，可以自动的记录所有实体以及属性的变更历史。默认开启，一般应用不重要可以在预初始化PreInitialize 方法中禁用他Configuration.EntityHistory.IsEnabled = false;
 26、动态webapi层：Abp框架能够通过应用层自动生成web api 。Http谓词：get、put、delete、post
 27、集成OData：开放数据协议（Open Data Protocol，缩写OData）是一种描述如何创建和访问Restful服务。你可以在Abp中使用OData，只需要通过Nuget来安装Abp.Web.Api.OData.
 28、集成SwaggerUI:开启Swagger，你可以获得一个交互式的文档，生成和发现客户端SDK。
 29、ABP表现层 - 本地化：模块预初始化时：Configuration.Localization.Languages.Add(new  LanguageInfo("en", "English", "famfamfam-flag-england", true));
 30、ABP表现层 - 导航栏：在Abp中，需要创建一个派生自NavigationProvider的类来定义一个菜单项。
 31、异常处理：ABP会自动的记录这些异常并且以适当的格式做出响应返回到客户端。<customErrors mode="On" />
 32、ABP表现层 - Javascript API：ABP提供了一系列的对象和函数，使用这些对象和函数使得脚本开发更容易且标准化。
 33、ABP表现层 - 嵌入资源文件：将文件单独修改为资源文件。使用嵌入式文件：<script  type="text/javascript"  src="~/AbpZero/Metronic/assets/global/scripts/metronic.js"></script>
 34、ASP.NET Core：将老项目迁移到ASP.NET Core。参考本节。
 35、CSRF或（cross-site request forgery）XSRF保护【跨站请求伪造】。ABP框架尽可能的简化且自动化CSRF保护。启动模板 实现了开箱即用的预配置。
 36、嵌入式资源：ABP提供了一个简单的方法来使用嵌入式的 Razor视图(.cshtml文件)和 其它资源(css，js，img等文件)。比如选中 index.cshtml 文件，然后打开属性窗口(快捷键：F4)，更改 生成操作 的值为 嵌入式资源。
 37、一、ABP后台服务 - 后台作业：ABP提供了后台作业和后台工人，来执行应用程序中的后台线程的某些任务。默认作业超时设置是2天。
        禁用后台作业：这种需求及其少见。但是想象一下，同时开启同一个应用程序的多个实例且使用的是同一个数据库。在这种情况下，每个应用程序的作业将查询相同的数据库并执行它们。
         这会导致相同的任务的多次执行，并还会导致其它的一些问题。为了阻止这个，你有两个选择：①、你可以仅为你应用程序的一个实例开启后台作业。②、你可以禁用Web应用的所有实例的后台作业，并且创建一个独立的应用程序(如：Windows服务)来执行你的后台作业。
        二、ABP后台服务 - 后台工人（特点是周期性执行某件事情）：比如①、周期性地执行旧日志的删除；②、周期性地确定非活跃性用户并且发送邮件给这些用户，使这些用户返回到你的网站中。
        三、ABP后台服务 - 集成Hangfire。Hangfire是一个综合性的后台作业管理工具。你可以用Hangfire来替换ABP中默认实现的后台作业管理者。
        四、ABP后台服务 - 集成Quartz。Quartz是一个功能齐全，且开源的作业调度系统，小型应用到大型企业级系统都可以使用它。Quartz是个好的选择，如果你对后台作业工人有很高的调度需要的话。
 38、一、ABP实时服务 - 通知系统：ABP提供了一个基于实时通知的基础设施 pub/sub.
        二、ABP实时服务 - 集成SignalR
        三、ABP实时服务 - 集成Abp.AspNetCore.SignalR
 39、一、ABP基础设施层 - 集成Entity Framework：ABP可以与任何ORM框架协同工作，它内置了对EntityFramework的集成支持。ABP中使用EntityFramework作为ORM框架。
        二、ABP基础设施层 - 集成NHibernate
        三、ABP 基础设施层 - 集成 Entity Framework Core
        四、ABP 基础设施层 - 集成 Entity Framework MySql：启动模板默认设计是使用Sql Server，但是你可以很容易的修改它来使用MySql。
        五、ABP基础设施层 - 集成Dapper：Dapper 是基于.NET的一种对象关系映射工具。
 40、ABP vNext 是下一代ABP框架，支持微服务并使它们相互通信。自身模块化粒度更小。新的ABP框架将基于.net standard。最终目标是完全抽象的底层数据存储系统和开发与EF Core无关的模块。
        将MongoDB作为第一级别的数据库，并在没有任何关系数据库或ORM假设的情况下设计实体和存储库。Bootstrap Tag Helpers用于简化为Bootstrap 4.x编写HTML。