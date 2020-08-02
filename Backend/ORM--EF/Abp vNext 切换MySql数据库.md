#  			[Abp vNext 切换MySql数据库](https://www.cnblogs.com/inday/p/abp-vNext-for-Mysql.html) 		



Abp vNext是Abp的下一代版本，目前还在经一步完善，代码已经全部重写了，好的东西保留了下来，去除了很多笨重的东西，从官宣来看，Abp vNext主要是为了以后微服务架构而诞生的。

从源码来看，Abp vNext已经支持了多种数据库，Sql Server，MySql，PostgreSql等。默认情况下，你创建的项目使用的是Sql Server版本，如果需要切换到MySql的话，仅需要：

第一步，在你的EntityFrameworkCore（Abp的EF框架模块，用来创建DbContext，数据迁移用的）中，从NuGet中安装`Volo.Abp.EntifyFrameworkCore.MySql`

第二步，打开`TGDbContextFactory.cs`

第三部，修改代码：

```
public TGDbContext CreateDbContext(string[] args)
{
    var configuration = BuildConfiguration();

    var builder = new DbContextOptionsBuilder<TGDbContext>()
        
        .UseSqlServer(configuration.GetConnectionString("Default"));

    return new TGDbContext(builder.Options);
}
```

改成

```
public TGDbContext CreateDbContext(string[] args)
{
    var configuration = BuildConfiguration();

    var builder = new DbContextOptionsBuilder<TGDbContext>()
        
        .UseMySQL(configuration.GetConnectionString("Default"));

    return new TGDbContext(builder.Options);
}
```

原本以为这样就能ok的，update-database的时候一堆错误，去issue上看了下，都有这个问题，有人建议用Pomele的MySql驱动，还提了PR，当我今天(3月9号)去看的时候PR已经通过，但Nuget包还未更新。

自给自足丰衣足食，自己来吧，其实非常简单

先去掉刚引入的`Volo.Abp.EntityFrameworkCore.MySql`,然后引入`Pomelo.EntityFrameworkCore.MySql`,随后上述代码改为：

```
public TGDbContext CreateDbContext(string[] args)
{
    var configuration = BuildConfiguration();

    var builder = new DbContextOptionsBuilder<TGDbContext>()
        
        .UseMySql(configuration.GetConnectionString("Default"));

    return new TGDbContext(builder.Options);
}
```

ok，简单改造完成，我们再来update-database，我们的创建顺利的完成了。

你以为结束了吗，幼稚！默认启动的时候他还是会选择Sql Server，我们看下代码，在`Web`项目下的`xxWebModule.cs`，xx是你的项目名，这个是我们web的Module文件，我们知道Abp是一个Module加载的框架。在这个项目中同样引入`Pomelo.EntityFrameworkCore.MySql`，随后修改`ConfigureDatabaseServices`方法，

```
private void ConfigureDatabaseServices()
{
    Configure<AbpDbContextOptions>(options =>
    {
        options.Configure(context =>
        {
            if (context.ExistingConnection != null)
            {
                context.DbContextOptions.UseMySql(context.ExistingConnection);
            }
            else
            {
                context.DbContextOptions.UseMySql(context.ConnectionString);
            }
        });
    });
}
```

改完以上的代码，你就可以顺利启动Abp vNext for MySql了。

改的不是很优雅，毕竟下一个版本应该会解决这个问题。之前用Abp  Core做了小程序并放到了生产环境，启动慢了点，但是运行什么都比较稳定，开发也比较便捷，看了Abp  vNext后，感觉一种小清新，相信在不久之后，我会使用它放到生产环境。其实一些老鸟的话，自己都有自己的框架，说实在的，Abp的好处就在于能够让大家有一种统一的快速的开发方式。

PS：陪儿子写字写了3个小时，写1个字发呆5分钟以上，能理解这种痛苦吗？

邮箱：james@taogame.com
 QQ:785418
 微信：jamesying1
 QQ群：376248054 通关：cnblogs 
  技术改变生活，技术改变人生！用技术来创造价值，拥有技术，不仅仅是开发，您将获得更多！如果您觉得我能帮到您，您可以通过扫描下面二维码来【捐助】我！