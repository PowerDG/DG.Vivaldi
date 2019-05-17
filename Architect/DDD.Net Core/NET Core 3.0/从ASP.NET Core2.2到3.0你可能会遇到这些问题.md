# 从ASP.NET Core2.2到3.0你可能会遇到这些问题

[依乐祝](https://cloud.tencent.com/developer/user/1181323)发表于[依乐祝](https://cloud.tencent.com/developer/column/5177)

96

## 在这篇文章中：

- [我遇到的问题](javascript:;)
- 2.0升3.0升级指南
  - [更新项目文件](javascript:;)
  - [Json.NET 支持](javascript:;)
  - [HostBuilder 替换 WebHostBuilder](javascript:;)
  - [更新 SignalR 代码](javascript:;)
  - [选择启用运行时编译](javascript:;)
- [总结](javascript:;)

趁着假期的时间所以想重新学习下微软的官方文档来巩固下基础知识。我们都知道微软目前已经发布了.NET Core3.0的第三个预览版，同时我家里的电脑也安装了vs2019。So，就用vs2019+.NET Core3.0来跟着做一下Contoso University这个WEB应用，但是在基于3.0进行操作的时候遇到了一些问题，所以我就查看了微软的《[从 ASP.NET Core 迁移 2.2 到 3.0 预览版 2](https://docs.microsoft.com/zh-cn/aspnet/core/migration/22-to-30?view=aspnetcore-3.0&tabs=visual-studio)》这篇文档，就着今天遇到的问题，所以我整理下，希望对大伙有所帮助，当然大伙也可以直接阅读微软的官方文档进行查看。但是我在阅读官方说明的时候，总感觉翻译的不是很准确，读起来很拗口，所以这里我是自己的理解对官方文档的一个补充。

>  作者：依乐祝  原文链接：<https://www.cnblogs.com/yilezhu/p/10661161.html>  

## 我遇到的问题

ASP.NET Core2.0时代，若要为项目添加 EF Core 支持，需要安装相应的数据库驱动包。 教程中使用 [SQL Server](https://cloud.tencent.com/product/sqlserver?from=10680)，相关驱动包[Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)。 此包包含在 [Microsoft.AspNetCore.App 元包](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/metapackage-app?view=aspnetcore-3.0)中，因此，如果应用具有对 `Microsoft.AspNetCore.App` 包的包引用，则无需引用该包。而2.0中的模板项目会自动为我们加载`Mcrosoft.AspNetCore.App`这个包的。但是3.0中没有了这个`Mcrosoft.AspNetCore.App`这个包，模块化的更彻底了！所需要的EF相关的包需要你自己来进行引用。

## 2.0升3.0升级指南

就着今天遇到的问题，所以我整理下ASP.NET Core从2.0升级3.0的一个升级指南，希望对大伙有所帮助，当然大伙也可以直接阅读微软的官方文档进行查看。但是我在阅读官方说明的时候，总感觉翻译的不是很准确，读起来很拗口，所以这里我是自己的理解对官方文档的一个补充。

### 更新项目文件

- 设置[TargetFramework](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks#referring-to-frameworks)到`netcoreapp3.0`:  <TargetFramework>netcoreapp3.0</TargetFramework>
- 删除[Microsoft.AspNetCore.All](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/metapackage?view=aspnetcore-3.0)或[Microsoft.AspNetCore.App](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/metapackage-app?view=aspnetcore-3.0)元包的任何`<PackageReference>`。
- 将`<PackageReference>`元素中剩余的`Microsoft.AspNetCore.*`程序包更新到当前的预览版中 (例如，3.0.0-preview3.19128.7)。  如果没有对应的 3.0 版本的包，则说明包可能会在 3.0 中弃用。 其中许多之前都属于`Microsoft.AspNetCore.App`并且不需要单独引用的包，如上面我遇到的问题，关于SQL Server的EF相关的包。 具体的不再在 3.0 中生成的包的列表，请参阅[aspnet/AspNetCore #3756](https://github.com/aspnet/AspNetCore/issues/3756)。
- 某些程序集已从2.x和3.0之间的Microsoft.aspnetcore.app中删除。如果您正在使用[aspnet/AspNetCore #3755](https://github.com/aspnet/AspNetCore/issues/3755)中列出的包中的API，则可能需要单独添加到。  例如，`Microsoft.EntityFrameworkCore`和`System.Data.SqlClient`不再属于`Microsoft.AspNetCore.App`得一部分。 Microsoft.aspnetcore.app中的程序集列表尚未定稿，将在3.0 RTM之前更改。
- 添加[Json.NET 支持](https://docs.microsoft.com/zh-cn/aspnet/core/migration/22-to-30?view=aspnetcore-3.0&tabs=visual-studio#json)。
- 项目默认为 ASP.NET Core 3.0 或更高版本设置为[进程内承载模型](https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.0#in-process-hosting-model)。 当然，如果其值为`InProcess`您还可以通过删除`<AspNetCoreHostingModel>`元素，来进行修改。

### Json.NET 支持

作为[提高 ASP.NET Core 共享的框架](https://blogs.msdn.microsoft.com/webdev/2018/10/29/a-first-look-at-changes-coming-in-asp-net-core-3-0/)工作的一部分， [Json.NET](https://www.newtonsoft.com/json/help/html/Introduction.htm)已从 ASP.NET Core 共享框架中删除。

若要在 ASP.NET Core 3.0 项目中使用 Json.NET:

- 添加到包引用[Microsoft.AspNetCore.Mvc.NewtonsoftJson](https://nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson)
- 更新`ConfigureServices`调用`AddNewtonsoftJson()`。  services.AddMvc()     .AddNewtonsoftJson();

Newtonsoft 的个性化设置可以设置为`AddNewtonsoftJson`:

```javascript
services.AddMvc()
    .AddNewtonsoftJson(options =>
           options.SerializerSettings.ContractResolver =
              new CamelCasePropertyNamesContractResolver());
```

### HostBuilder 替换 WebHostBuilder

使用 ASP.NET Core 3.0 模板[泛型宿主](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0)。 早期版本使用[Web 主机](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-3.0)。 下面的代码显示了生成 ASP.NET Core 3.0 模板`Program`类：

```javascript
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```

下面的代码演示模板生成 ASP.NET Core 2.2`Program`类：

```javascript
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}
```

[IWebHostBuilder](https://docs.microsoft.com/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder) 将保留在 3.0，是一种`webBuilder`上面的代码示例所示。 [WebHostBuilder](https://docs.microsoft.com/dotnet/api/microsoft.aspnetcore.hosting.webhostbuilder) 将在未来版本中弃用并替换为`HostBuilder`。

从`WebHostBuilder`到`HostBuilder`最显著的变化是[依赖关系注入 (DI)](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0)。 使用时`HostBuilder`，只能将[IConfiguration](https://docs.microsoft.com/dotnet/api/microsoft.extensions.configuration.iconfiguration)并[IHostingEnvironment](https://docs.microsoft.com/dotnet/api/microsoft.aspnetcore.hosting.ihostingenvironment)注入到`Startup`的构造函数中。 `HostBuilder` DI 约束：

- 使DI容器只能构建一次。
- 避免产生的对象生存期问题，例如解决多个单例实例。

### 更新 SignalR 代码

如果您调用`AddJsonProtocol`，将其替换为`AddNewtonsoftJsonProtocol`。

- 以下示例显示更改前后的服务器代码：  services.AddSignalR(...)         .AddJsonProtocol(...) // 2.2  services.AddSignalR(...)         .AddNewtonsoftJsonProtocol(...) // 3.0
- 以下示例显示更改前后的.NET客户端代码：  connection = new HubConnectionBuilder()     .WithUrl(...)     .AddJsonProtocol(...) // 2.2     .Build()  connection = new HubConnectionBuilder()     .WithUrl(...)     .AddNewtonsoftJsonProtocol(...) // 3.0     .Build()

### 选择启用运行时编译

在 3.0 中，运行时编译是可选的方案。 若要启用运行时编译，请参阅[ASP.NET Core 中的 Razor 文件编译](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/views/view-compilation?view=aspnetcore-3.0#runtime-compilation)。

## 总结

感觉微软在努力实现ASP.NET Core的模块化，减小各种依赖，让包变得更小。同时ASP.NET Core也已经很完善了，大伙是时候用起来ASP.NET Core了。另外需要说明的一点是，大伙如果是为了体验ASP.NET Core3.0的话，现在就可以，如果是用在生产环境的话最好还是等待正式版的发布吧。

本文参与[腾讯云自媒体分享计划](https://cloud.tencent.com/developer/support-plan)，欢迎正在阅读的你也加入，一起分享。

发表于  30 天前

[ASP.NET](https://cloud.tencent.com/developer/tag/10185?entry=article)[.NET](https://cloud.tencent.com/developer/tag/10180?entry=article)[官方文档](https://cloud.tencent.com/developer/tag/137?entry=article)[JSON](https://cloud.tencent.com/developer/tag/10207?entry=article)

[举报](javascript:;)

[2](javascript:;)

分享

- 

  ![Scan me!](data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAAEDElEQVR4nO2Uy3LDMAwD8/8/3V56SDwSuSBpx0nBGV8kPgCuksfj8fjpfsfIcnZ1x9oon+ig/WlEMyf2+PcZCI1LgXTEkbvKIgi4TJvykHa1ZP4gaAPJasn804BQE1SA2oOCXuXQMzVUn5FekGcgWRiIgcwCyRZH8wpmpOWT/s95tBfpbyBw/lcBqQKkUQF4vMvy6Rdp6+zSQAzEQCQgleWQvE6PjfAW3Iq2sx6cgRS1XQpk4ucb/S1MnNFFvFtb8zOQaW0G8k1AfgaDwFrdTfWlZ9H86mOcCgMBs94KZFr4Kj8UFOR1gESzpnxVdbzMj0wbyBuB0J8lPVND7Usfizpz1aMKpvKADMRA+HIMZLMQVVxnSdH8al1H93M/9S7TtryPGhvIaz/1LtO2udf+nogoarry0+9CmvCX1ap9D5+BTNcaCIiPBdJZJh2eCEI6JoBQHcSD4j/xZSAfAyRqRofTUOGctTjVQ+UBJ/s1kI4HA/l2IHIBEFDpoUKn0X1cqx5RTqWvgdwZiLoY9WepLnfyYagaO76mHpmBGIi+MGq62v+WQMhCdiKPZ6oZOpdqyvROPhJaR/UaCIhLgUwKxkOLoCf+Kip5mc6uv5d7YjoLAxkEQiCoJrJackfnV/JUbVFtlF983AZiIOCOzq/kqdqi2ii/BUQo2C5iJYT2r9Qc69QgXmht5bFs+hpIdkZqx4CoQlYDyB0VnM0iH61VtdHa1kcWki2J3BmIgaS1qrZLgKggooWtRFZ7rMxGdXTpVIcKfwq0gYj9LgMyKYAuWl3IdJ6qQ/VQ7GsgxcUZyHTerYFQA6qAVd+K0Kvzs1D3IZwZSCUuA0KFRAY75qOabBbpQWeqZ8oM6S9rorGBxDNCIKSALnjiL0kFQiP6y6hoUx8B3q1cYCAGQk1H8TFASOPKWbVHtkTlrvO4pvsJeg0ki7cA6TRWBVcXQQ2epU31UOxrIAZiIPu81FHBVNYjOlPnqrNWeTQ64PBDrgoxkIuAZIsjjaeMqQZJ/vQDUkGnc1UhBmIg/wuIunzVNBVCZnR0qEBWM9S5md7NvYFQHercTO/yXhUyEdRgN58+jAmIWQ7Wqy5uIgzEQJZ3Uf7bgUTL74qdElzRFOnoADntM5CbApmIs4Fkd+Ss4oGeEc+pv1ShEAaSewb++j8zKoD0oAajs06c9TAE0Aaym2EgwrKis07cBkhHODE1DVXVVl308312puwoqDGQWwOpLC4SEglSBSv9JkFHQDrfppeBGAhcsJpvIKBWXWbHNO27y9n1orOivoInA/l6INSMCk4FRH1ly4x8RbU0DMRADCSKEEilCcnr9FD70nx10WoehX44M5BK39OBdL5ICK2rzFGW1PHUAaf2MBADMZDo7BfZ4755vwnDMgAAAABJRU5ErkJggg==)

  

- 

  

- 

  

- 

  



![img](assets/wechat-qr.jpg)





### [依乐祝](https://cloud.tencent.com/developer/column/5177)

73 篇文章16 人订阅

- [ASP.NET Core 3.0 上的gRPC服务模板初体验(多图)](https://cloud.tencent.com/developer/article/1411354)
- [构建现代Web应用时究竟是选择传统web应用还是SPA](https://cloud.tencent.com/developer/article/1409112)
- [VS2017 无法连接到Web服务器“IIS Express”终极解决方案](https://cloud.tencent.com/developer/article/1406055)
- [What?VS2019创建新项目居然没有.NET Core3.0的模板?Bug?](https://cloud.tencent.com/developer/article/1414682)
- [进行API开发选gRPC还是HTTP APIs?](https://cloud.tencent.com/developer/article/1415407)