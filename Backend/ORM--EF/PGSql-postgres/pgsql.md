1.安装我们需要的Nuget Packages:

```
install-package npgsql -version 3.1.1
Install-Package EntityFramework6.Npgsql -Version 3.1.1
```

严重性	代码	说明	项目	文件	行	禁止显示状态
错误	NU1605	检测到包降级: Microsoft.Extensions.Logging 从 2.2.0 降级到 2.1.1。直接从项目引用包以选择不同版本。 
 DgRNCore.Web -> DgRNCore.EntityFrameworkCore -> Npgsql.EntityFrameworkCore.PostgreSQL 2.2.0 -> Microsoft.EntityFrameworkCore 2.2.0 -> Microsoft.Extensions.Logging (>= 2.2.0) 
 DgRNCore.Web -> Microsoft.Extensions.Logging (>= 2.1.1)	DgRNCore.Web	E:\DgHub\BedRock\DgRNCore\3.8.0\src\DgRNCore.Web\DgRNCore.Web.csproj	1	



```
"Default": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=DgERM;Pooling=true;"
```

```
Add-Migration Init
Update-Database
```

```
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL 
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL.Design

```



   #####  public static class ERMDbContextConfigurer

```csharp
 
    public static void Configure(DbContextOptionsBuilder<ERMDbContext> builder, string connectionString)
    {
        //builder.UseSqlServer(connectionString); 
        builder.UseNpgsql(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<ERMDbContext> builder, DbConnection connection)
    {
        //    builder.UseSqlServer(connection); 
        builder.UseNpgsql (connection);
    }
 ---
     
 public DbSet<Product> Products { get; set; }
```
----





````



严重性	代码	说明	项目	文件	行	禁止显示状态
错误	CS1705	标识为“Fonour.EntityFrameworkCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null”的程序集“Fonour.EntityFrameworkCore”所使用的“Microsoft.EntityFrameworkCore, Version=2.2.4.0, Culture=neutral, PublicKeyToken=adb9793829ddae60”版本高于所引用的标识为“Microsoft.EntityFrameworkCore, Version=2.2.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60”的程序集“Microsoft.EntityFrameworkCore”	Fonour.Host	E:\DgProject\DDDgPractice\Dg.ERM\Trump.Dg\Trump\Fonour.Host\CSC	1	活动的

````



首先下载安装包

    Microsoft.EntityFrameworkCore：核心包，不多说
    Microsoft.EntityFrameworkCore.Tool：支持 PS 命令的 Code First 工具包
    Microsoft.EntityFrameworkCore.Design：Code First 必备包

下面的包是自定义数据驱动的包，想要啥自己去Nuget上找就对了

    Microsoft.EntityFrameworkCore.SqlServer：Sql Server 驱动
    Microsoft.EntityFrameworkCore.Sqlite：Sqlite 驱动

其他的自己去nuget上找吧！我这里以 Sql Server 为例子。
--------------------- 
作者：佛系最高指挥官 
来源：CSDN 
原文：https://blog.csdn.net/playermaker57/article/details/79148884 
版权声明：本文为博主原创文章，转载请附上博文链接！