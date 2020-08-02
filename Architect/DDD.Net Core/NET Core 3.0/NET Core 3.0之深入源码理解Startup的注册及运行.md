

# .NET Core 3.0之深入源码理解Startup的注册及运行





开发.NET  Core应用，直接映入眼帘的就是Startup类和Program类，它们是.NET  Core应用程序的起点。通过使用Startup，可以配置化处理所有向应用程序所做的请求的管道，同时也可以减少.NET应用程序对单一服务器的依赖性，使我们在更大程度上专注于面向多服务器为中心的开发模式。

目录：

- Startup讨论
  - Starup所承担的角色
  - Startup编写规范
  - ConfigureServices
  - Configure
  - 扩展Startup方法
- 深入源码查看Startup是如何注册和执行的
  - UseStartup源码
  - 创建Startup实例
  - ConfigureServices和Configure

## Starup所承担的角色

Startup作为一个概念是ASP.NET   Core程序中所必须的，Startup类本身可以使用多种修饰符(public、protect，private、internal)，作为ASP.NET  Core应用程序的入口，它包含与应用程序相关配置的功能或者说是接口。

虽然在程序里我们使用的类名就是Startup，但是需要注意的是，Startup是一个**抽象概念**，你完全可以名称成其他的，比如MyAppStartup或者其他的什么名称，只要你在Program类中启动你所定义的启动类即可。

当然如果不想写Startup，可以在Program类中配置服务和请求处理管道，请参见**评论区5楼，**非常感谢**Emrys**耐心而又全面的指正**。**

以下是基于ASP.NET Core Preview 3模板中提供的写法：

![640?wx_fmt=png](assets/p-1558099483137)

不管你命名成什么，只要将**webBuilder.UseStartup<>()**中的泛型类配置成你定义的入口类即可;

## Startup编写规范

下面是ASP.NET Core 3.0 Preview 3模板中Startup的写法： 

![640?wx_fmt=png](assets/p-1558099483189)

通过以上代码可以知道，Startup类中一般包括

- 构造函数：通过我们以前的开发经验，我们可以知道，该构造方法可以包括多个对象
  - IConfiguration：表示一组键/值应用程序配置属性。
  - IApplicationBuilder：是一个包含与当前环境相关的属性和方法的接口。它用于获取应用程序中的环境变量。
  - IHostingEnvironment：是一个包含与运行应用程序的Web宿主环境相关信息的接口。使用这个接口方法，我们可以改变应用程序的行为。
  - ILoggerFactory：是为ASP.NET Core中的日志记录系统提供配置的接口。它还创建日志系统的实例。
- ConfigureServices
- Configure

> Startup在创建服务时，会执行依赖项注册服务，以便在应用程序的其它地方使用这些依赖项。ConfigureServices  用于注册服务，Configure 方法允许我们向HTTP管道添加中间件和服务。这就是ConfigureServices先于Configure  之前调用的原因。

## ConfigureServices

该方法时可选的，非强制约束，它主要用于对依赖注入或ApplicationServices在整个应用中的支持，该方法必须是public的，其典型模式是调用所有 `Add{Service}` 方法，主要场景包括实体框架、认证和 MVC 注册服务：

![640?wx_fmt=png](assets/p-1558099483188)

## Configure

该方法主要用于定义应用程序对每个HTTP请求的响应方式，即我们可以控制ASP.NET管道，还可用于在HTTP管道中配置中间件。请求管道中的每个中间件组件负责调用管道中的下一个组件，或在适当情况下使链发生短路。  如果中间件链中未发生短路，则每个中间件都有第二次机会在将请求发送到客户端前处理该请求。

该方法接受IApplicationBuilder作为参数，同时还可以接收其他一些可选参数，如IHostingEnvironment和ILoggerFactory。

一般而言，只要将服务注册到configureServices方法中时，都可以在该方法中使用。

```
  
```

## 扩展Startup方法 

使用IStartupFilter来对Startup功能进行扩展，在应用的Configure中间件管道的开头或末尾使用IStartupFilter来配置中间件。IStartupFilter有助于确保当库在应用请求处理管道的开端或末尾添加中间件的前后运行中间件。

以下是IStartupFilter的源代码，通过源代码我们可以知道，该接口有一个Action<IApplicationBuilder>类型，并命名为Configure的方法。由于传入参数类型和返回类型一样，这就保证了扩展的传递性及顺序性，具体的演示代码，可以参数MSDN

  

![640?wx_fmt=png](assets/p-1558099483070)

此段文字，只是我想深入了解其内部机制而写的，如果本身也不了解，其实是不影响我们正常编写.NET Core应用的。

## UseStartup源码

ASP.NET Core通过调用IWebHostBuilder.UseStartup方法，传入Startup类型，注意开篇就已经说过Startup是一个抽象概念，我们看下源代码：

  

![640?wx_fmt=png](assets/p-1558099483188)

**创建Startup实例**

```
  
```

![640?wx_fmt=png](assets/p-1558099483069)

关于ConfigureServices的定义及注册方式，是在IWebHostBuilder.ConfigureServices实现的，同时可以注意一下**25**行代码，向大家说明了多次注册Startup的ConfigureServices方法时，会合并起来的根源。此处抽象委托用的也非常多。

该类里面还有Build方法，我就不贴出代码了，只需要知道，主进程在此处开始了。接下来一个比较重要的方法，是**BuildCommonServices，**它向当前ServiceCollection中添加一些公共框架级服务，以下是部分代码，具体代码请查看WebHostBuilder。

   

![640?wx_fmt=png](assets/p-1558099483189)



```
由此可见，如果我们的Startup类直接实现IStartup，它可以并且将直接注册为IStartup的实现类型。只不过ASP.NET Core模板代码并没有实现IStartup，它更多的是一种约定，并通过DI调用委托，依此调用Startup内的构造函数还有另外两个方法。
同时上述代码还展示了如何创建Startup类型，就是用到了静态方法StartupLoader.LoadMethods类生成StartupMethods实例。
```

## **ConfigureServices**和**Configure**

```
当WebHost初始化时，框架会去查找相应的方法，这里，我们主要查看源代码，其中的核心方法是StartupLoader.FindMethods
```

 

![640?wx_fmt=png](assets/p-1558099483188)



```
它查找的第一个委托是ConfigureDelegate，该委托将用于构建应用程序的中间件管道。FindMethod完成了大部分工作，具体的代码请查看StartupLoader。此方法根据传递给它的methodName参数在Startup类中查找响应的方法。
我们知道，Startup的定义更多的是约定，所以会去查找Configure和ConfigureServices。当然，通过源代码我还知道，除了提供标准的“Configure”方法之外，我们还可以通过环境配置找到响应的Configure和ConfigureServices。根本来说，我们最终查找到的是ConfigureContainerDelegate。
接下来，一个比较重要的方法是LoadMethods
```

 

![640?wx_fmt=png](assets/p-1558099483245)

 

```
该方法通过查找对应的方法，由于Startup并未在DI中注册，所以会调用GetServiceOrCreateInstance创建一个Startup实例，此时构造函数也在此得到解析。
通过一系列的调用，最终到达了ConfigureServicesBuilder.Invoke里面。Invoke方法使用反射来获取和检查在Startup类上定义的ConfigureServices方法所需的参数。 
```

![640?wx_fmt=png](assets/p-1558099483187)

最后我们来看一下ConfigureBuilder类，它需要一个Action<IApplicationBuilder>委托变量，其中包含每个IStartupFilter的一组包装的Configure方法，最后一个是Startup.Configure方法的委托。此时，所调用的配置链首先命中的是AutoRequestServicesStartupFilter.Configure方法。并将该委托链作为下一个操作，之后会调用ConventionBasedStartup.Configure方法。这将在其本地StartupMethods对象上调用ConfigureDelegate。

```
  
```

![640?wx_fmt=png](assets/p-1558099483137)

Startup.Configure方法会调用ServiceProvider所解析的相应的参数，该方法还可以使用IApplicationBuilder将中间件添加到应用程序管道中。最终的RequestDelegate是从IApplicationBuilder构建并返回的，至此WebHost初始化完成。

*原文地址：https://www.cnblogs.com/edison0621/p/10743228.html*

```

```

.NET社区新闻，深度好文，欢迎访问公众号文章汇总 http://www.csharpkit.com 
![640?wx_fmt=jpeg](assets/p)