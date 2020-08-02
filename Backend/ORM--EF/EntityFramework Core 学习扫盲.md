#  			[EntityFramework Core 学习扫盲](https://www.cnblogs.com/Wddpct/p/6835574.html) 		



- - [0. 写在前面](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#0-写在前面)
  - [1. 建立运行环境](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#1-建立运行环境)
  - \2. 添加实体和映射数据库
    - - [1. 准备工作](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#2-1-准备工作)
      - [2. Data Annotations](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#2-2-data-annotations)
      - [3. Fluent Api](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#2-3-fluent-api)
  - \3. 包含和排除实体类型
    - - [1. Data Annotations [NotMapped\] 排除实体和属性](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#1-data-annotations-notmapped-排除实体和属性)
      - [2. Fluent API [Ignore\] 排除实体和属性](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#2-fluent-api-ignore-排除实体和属性)
  - \4. 列名称和类型映射
    - - [1. Data Annotations](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#4-1-data-annotations)
  - \5. 主键
    - - [1. Data Annotations [Key\]](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#1-data-annotations-key)
      - [2. Fluent API [HasKey\]](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#2-fluent-api-haskey)
  - \6. 备用键
    - - [1. Fluent API](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#6-1-fluent-api)
  - \7. 计算列
    - - [1. Fluent API](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#7-1-fluent-api)
  - \8. 生成值
    - - [1. Data Annotations](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#8-1-data-annotations)
      - [2. Fluent API](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#8-2-fluent-api)
  - [9. 默认值](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#9-默认值)
  - \10. 索引
    - - [1. Fluent API](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#10-1-fluent-api)
  - \11. 主体和唯一标识
    - - [1. 备用键](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#1-备用键)
      - [2. 唯一索引](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#2-唯一索引)
  - [12. 继承](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#12-继承)
  - [13. 关系](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#13-关系)
  - [14. Console中的EntityframeworkCore](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#14-Console中的EntityframeworkCore)
  - [15. 参考链接和优秀博客](http://www.cnblogs.com/Wddpct/archive/2017/05/10/6835574.html#15-参考链接和优秀博客)

## 0. 写在前面

本篇文章虽说是入门学习，但是也不会循规蹈矩地把EF1.0版本一直到现在即将到来的EF Core 2.0版本相关的所有历史和细节完完整整还原出来。在后文中，笔者会直接进入正题，所以这篇文章仍然还是需要一定的EF ORM基础。

对于纯新手用户，不妨先去看看文末链接中一些优秀博客，笔者当初也是从这些博客起家，也从中得到了巨大的帮助。当然了，[官方教程](https://docs.microsoft.com/zh-cn/ef/core/)同样至关重要，笔者之前也贡献过部分EF CORE 官方文档资料（基本都是勘误，逃…），本篇文章中很多内容都是撷取自官方的英文文档和示例。

> 下文示例中将使用Visual Studio自带的Local Sql Server作为演示数据库进行演示，不过可以放心的是，大部分示例都能流畅地在各种关系型数据库中实现运行，前提是更换不同的DATABASE PROVIDERS，如[NPGSQL](http://www.npgsql.org/)，[MYSQL](https://dev.mysql.com/)等。

> 对于未涉及到的知识点（CLI工具，Shadow Property，Logging，从Exsiting Database反向工程生成Context等），只能说笔者最近一直在忙着毕业收尾的事情，有空的时候会把草稿整理下在博文中贴出的，一晃四年，终于也要毕业了。

## 1. 建立运行环境

- 新建一个APS.NET CORE WEB模板项目
- 安装相关Nuget包

```
//Sql Server Database Provider
Install-Package Microsoft.EntityFrameworkCore.SqlServer
    
//提供熟悉的Add-Migration，Update-Database等Powershell命令，不区分关系型数据库类型
Install-Package Microsoft.EntityFrameworkCore.Tools 
```

- 自定义Context

```
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options):base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
```

- 在Startup的ConfigurationServices方法中添加EF CORE服务

```
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();

    services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
}
        
// 需要在appsettings.json中新增一个ConnectionStrings节点，用于存放连接字符串。
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApplication4;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

- 在powershell中运行相关迁移命令

```
Add-Migration Initialize

Update-Database
```

## 2. 添加实体和映射数据库

使用EF CORE中添加实体，约束属性和关系，最后将其映射到数据库中的方式有两种，一种是Data Annotations，另一种是Fluent Api，这两种方式并没有优劣之分，全凭开发者喜好和需求，不过相对而言，Fluent Api提供的功能更多。

### **1. 准备工作**

- 新增三个实体

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}

public class AuditEntry
{
    public int AuditEntryId { get; set; }
    public string Username { get; set; }
    public string Action { get; set; }
}
```

### **2. Data Annotations**

- 在自定义的MyContext中添加以下属性信息，并在每个自定义的实体名称上部增加[Table("XXX")]，其中XXX为开发者指定的表名称。

```
//在自定义的MyContext中添加以下三行代码
public DbSet<Blog> Blogs { get; set; }
public DbSet<Post> Posts { get; set; }
public DbSet<AuditEntry> AuditEntries { get; set; }

//添加Table特性，第一个属性代表数据库表名称
[Table("Blogs")]
public class Blog
{
    [Key]
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}
```

- 运行`Add-Migration         Initialize`和`Update-Database`即可成功运行。虽然我们目前还没有添加任何约束，但是EF Core会自动地根据`Id/XXId`的命名方式生成自增主键，而且如果没有在实体上增加[Table]Attribute的话，表的命名也是根据属性命名而定。查询相关的Create Table语句，清晰易见，Identity（1,1）代表Id从1开始，每次插入递增1。

```
//BLOG Table
CREATE TABLE [dbo].[Blogs] (
    [BlogId] INT            IDENTITY (1, 1) NOT NULL,
    [Url]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED ([BlogId] ASC)
);

CREATE TABLE [dbo].[Posts] (
    [PostId]  INT            IDENTITY (1, 1) NOT NULL,
    [BlogId]  INT            NULL,
    [Content] NVARCHAR (MAX) NULL,
    [Title]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([PostId] ASC),
    CONSTRAINT [FK_Posts_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blogs] ([BlogId])
);

//POST Table
GO
CREATE NONCLUSTERED INDEX [IX_Posts_BlogId]
    ON [dbo].[Posts]([BlogId] ASC);
```

### **3. Fluent Api**

Fluent Api俗名流式接口，其实就是C#中的扩展接口形式而已，大家日常应该接触过很多了。还记得我们第一步中MyContext定义的OnModelCreating方法吗，Fluent Api就是在那里面使用的

- 增加以下代码至OnModelCreating方法。

```
modelBuilder.Entity<Blog>().ToTable("Blogs").HasKey(c=>c.BlogId);
modelBuilder.Entity<Post>().ToTable("Posts").HasKey(c=>c.PostId);
modelBuilder.Entity<AuditEntry>().ToTable("AuditEntries").HasKey(c=>c.AuditEntryId);
```

- 运行`Add-Migration Initialize`和`Update-Database`即可成功运行。

> 无论是使用DbSet< TEntity >的形式抑或是使用modelBuilder.Entity< TEntity >的形式都能将定义的实体映射到数据库中，下文也会继续做出说明。

## 3. 包含和排除实体类型

将实体在Context中映射到数据库有多种方式：

- 使用DbSet< TEntity >定义属性。
- 在OnModelCreating方法中使用Fluent Api配置。
- 假如导航属性中存在对其他实体的引用，那么即便不把被引用实体配置为显式引用，被引用实体也可以隐式地映射到数据库中。

如以下代码所示。Blog实体包含对Post实体的引用，而独立的AuditEntry则可以在OnModelCreating方法中进行配置。

```
class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditEntry>();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}

public class AuditEntry
{
    public int AuditEntryId { get; set; }
    public string Username { get; set; }
    public string Action { get; set; }
}
```

以上内容指的是**“包含实体”**的操作，**“排除实体”**操作也十分地简便。通过以下两种配置方式，在运行了迁移命令后，BlogMetadata实体是不会映射到数据库中的。

### **1. Data Annotations [NotMapped] 排除实体和属性**

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public BlogMetadata Metadata { get; set; }
}

[NotMapped]
public class BlogMetadata
{
    public DateTime LoadedFromDatabase { get; set; }
}
```

### **2. Fluent API [Ignore] 排除实体和属性**

```
class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<BlogMetadata>();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public BlogMetadata Metadata { get; set; }
}

public class BlogMetadata
{
    public DateTime LoadedFromDatabase { get; set; }
}
```

> NotMapped特性不仅可以用在实体类上，也可以用在指定的属性上。而在Fluent Api中，对应的操作则是
>  `modelBuilder.Entity<Blog>().Ignore(b => b.LoadedFromDatabase);`

## 4. 列名称和类型映射

> Property方法对应数据库中的Column。

默认情况下，我们不需要更改任何实体中包含的属性名，EF  CORE会自动地根据属性名称映射到数据库中的列名。当开发者需要进行自定义修改名称时（  比如每种关系型数据库的命名规则不一样，虽然笔者一直喜欢使用帕斯卡命名以保持和项目代码结构中的统一），可以使用以下的方式。

少数的几个CLR类型在不做处理的情况下，映射到数据库中时将存在**可空**选项，如string，int?，这种情况也在下列方式中做了说明。其中Data Annotations对应[Required]特性，Fluent API对应IsRequired方法。

### **1. Data Annotations**

Column特性可用于属性上，它接收多个参数，其中比较重要的是Name和TypeName，前者表示数据库表映射的列名，后者表示数据类型和格式。假如不指定Url属性上的`[Column(TypeName="varchar(200)")]`，数据库Blog表的Url列默认数据格式将为`varchar(max)`。

```
public class Blog
{
    [Column("blog_id")]
    public int BlogId { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Url { get; set; }
}
```

### **2. Fluent API**

```
class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(b => b.BlogId)
            .HasColumnName("blog_id");
            
            modelBuilder.Entity<Blog>()
            .Property(b => b.Url)
            .HasColumnType("varchar(200)")
            .IsRequired();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}
```

> 由于各种关系型数据库对于数据类型的名称有所区别，所以自定义数据类型时，一定要参阅目标数据库的数据类型定义。比如PostgreSql支持Json格式，那么就需要添加以下代码——builder.Entity().Property(b => b.SomeStringProperty).HasColumnType("jsonb");

## 5. 主键

默认情况下，EF CORE会将实体中命名为Id或者[TypeName]Id的属性映射为数据库表中的主键。当然有些开发者不喜欢将主键命名为Id，EF CORE也提供了两种方式进行主键的相关设置。

### **1. Data Annotations [Key]**

Data Annotations方式不支持定义外键名称，主键配置如下代码所示。

```
class Car
{
    [Key]
    public string LicensePlate { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}
```

### **2. Fluent API [HasKey]**

Fluent Api方式中的HasKey方法可以将属性映射为主键，对于复合主键（多个属性组合而成的主键标识）也可以很容易地进行表示。

```
class MyContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasKey(c => c.LicensePlate);
        
        //复合主键
        //modelBuilder.Entity<Car>()
            //.HasKey(c => new { c.State, c.LicensePlate });
            
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogId)
            .HasConstraintName("ForeignKey_Post_Blog");
    }
}

class Car
{
    public string State { get; set; }
    public string LicensePlate { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}
```

## 6. 备用键

Alternate Keys是EF CORE引入的新功能，EF  6.X版本中并没有此功能。备用键可以用作实体中除主键和索引外的唯一标识符，还可以用作外键目标。在Fluent  Api中，有两种方法可以指定备用键，一种是当开发者将实体中的属性作为另一个实体的外键目标，另一种是手动指定。EF CORE的默认约束是前者。

备用键和主键的作用十分相似，同样也存在复合备用键的功能，请大家注意区分。在要求单表列的一致性的场景中，使用唯一索引比使用备用键更佳。

### **1. Fluent API**

```
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 第一种方法
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogUrl)
            .HasPrincipalKey(b => b.Url);
            
        // 第二种方法
        modelBuilder.Entity<Car>()
            .HasAlternateKey(c => c.LicensePlate)
            .HasName("AlternateKey_LicensePlate");
    }
}

public class Car
{
    public int CarId { get; set; }
    public string LicensePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public string BlogUrl { get; set; }
    public Blog Blog { get; set; }
}
```

上述代码中的第一种方法指定Post实体中的BlogUrl属性作为Blog对应Post的外键，指定Blog实体中的Url属性作为备用键（HasPrincipalKey方法将在下文的唯一标识节中讲解），此时Url将被配置为唯一列，扮演BlogId的作用。

## 7. 计算列

计算列指的是列的数据由数据库计算生成，在EF CORE层面，我们只需要定义计算规则即可。目前EF CORE 1.1 版本中，暂不支持使用Data Annotations方式定义。

### **1 Fluent API**

```
class MyContext : DbContext
{
    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.DisplayName)
            .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
    }
}

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
}
```

以上代码指明DisplayName由LastName和FirstName结合计算而成，这项工作由数据库代劳，查看P的视图设计器，我们也可以发现数据库在生成表时便指定了详细规则。

```
CREATE TABLE [dbo].[People] (
    [PersonId]    INT            IDENTITY (1, 1) NOT NULL,
    [DisplayName] AS             (([LastName]+', ')+[FirstName]),
    [FirstName]   NVARCHAR (MAX) NULL,
    [LastName]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([PersonId] ASC)
);
```

## 8. 生成值

前文中已经介绍过，假如属性被命名为`Id/[TypeName]Id`的形式，EF CORE会将该属性设置为主键。进一步说，如果属性是整数或是Guid类型，那么该属性将会被EF CORE设置为自动生成。这是EF CORE的语法糖之一。

那由用户手动设置呢？EF CORE在Data Annotations和Fluent Api形式上为开发者分别提供了三种方法。

### **1 Data Annotations**

- 不生成（默认方式）

```
public class Blog
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int BlogId { get; set; }
    public string Url { get; set; }
}
```

- 新增实体信息时自动添加

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Inserted { get; set; }
}
```

- 新增或更新实体时自动添加

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastUpdated { get; set; }
}
```

### **2 Fluent API**

- 不生成（默认方式）

```
modelBuilder.Entity<Blog>()
    .Property(b => b.BlogId)
    .ValueGeneratedNever();
```

- 新增实体信息时自动添加

```
modelBuilder.Entity<Blog>()
    .Property(b => b.Inserted)
    .ValueGeneratedOnAdd();
```

- 新增或更新实体时自动添加

```
modelBuilder.Entity<Blog>()
    .Property(b => b.LastUpdated)
    .ValueGeneratedOnAddOrUpdate();
```

> 值得注意的是，上述对DateTime类型的自动添加操作都是不可行的，这是因为EF CORE只支持部分类型的自动操作，详见[Default Values](https://docs.microsoft.com/zh-cn/ef/core/modeling/relational/default-values)。对于DateTime类型，我们可以用以下代码实现自动插入
>  `modelBuilder.Entity<Blog>().Property(b => b.Created).HasDefaultValueSql("getdate()");`，
>  这也是第7点默认值的一种用法。

## 9. 默认值

默认值与计算列定义十分相似，只是计算列无法由用户手动输入。而默认值更多指的是当用户不手动输入时，使用默认值进行数据库相应列的填充。以下代码表示假如操作中不指定Rating的值，那么数据库将默认填充3。

```
class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(b => b.Rating)
            .HasDefaultValue(3);
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public int Rating { get; set; }
}
```

## 10. 索引

EF CORE中的索引概念和关系型数据库中的索引概念没有什么不同，比如在Sql Server，将Blog映射到数据库时，将为BlogId建立主键默认持有的聚集索引，将Post映射到数据库中时，将为Post的BlogId建议外键默认的非聚集索引。

```
GO
CREATE NONCLUSTERED INDEX [IX_Posts_BlogId]
    ON [dbo].[Posts]([BlogId] ASC);
```

至于为一个或多个属性手动建立索引，可以使用形如以下代码。

### **1. Fluent API**

```
class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Person> People { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasIndex(b => b.Url)
            .HasName("IX_Url");
        
        modelBuilder.Entity<Person>()
            .HasIndex(p => new { p.FirstName, p.LastName });
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

假如你需要配置一个**唯一索引**，请使用IsUnique方法。形如以下代码：

```
modelBuilder.Entity<Blog>()
            .HasIndex(b => b.Url)
            .HasName("IX_Url")
            .IsUnique();
```

## 11. 主体和唯一标识

在这一节中，让我们来回顾一下HasPrincipalKey方法和唯一标识。

在EF CORE中，主体（Principal  Entity）指的是包含主键/备用键的实体。所以在一般情况下，所有的实体都是主体。而主体键（Principal  Key）指的是主体中的主键/备用键。大家都知道，主键/备用键都是不可为空且唯一的，这就引出了唯一标识列的写法。

唯一标识列一般有**“主体键”，“唯一索引”**两种写法，其中主体键中的主键没有什么讨论的价值。让我们来看看其他两种的写法。

### **1. 备用键**

备用键在之前的小节中已经提过，使用以下代码配置的列将自动设置为唯一标识列。

```
modelBuilder.Entity<Car>()
                .HasAlternateKey(c => new {c.LicensePlate, c.Model})
                .HasName("AlternateKey_LicensePlate");
                
modelBuilder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogUrl)
                .HasPrincipalKey(b => b.Url);
```

注意这里的HasPrincipalKey方法，它通常跟在HasForeignKey和WithMany方法后，用以指定实体中的一个或多个属性作为备用键。再次重申一遍，备用键和主键有相似之处，它通常用来指定一个明确的外键目标——当开发者不想用单纯无意义的Id作为外键标识时。

> 虽然主体键也包括主键，但是主键在EF CORE中时强制定义的，所以HasPrincipalKey只会将属性配置为备用键。

### **2. 唯一索引**

索引及其唯一性只由Fluent Api方式指定，由索引来指定唯一列是比备用键更好的选择。

```
modelBuilder.Entity<Blog>()
            .HasIndex(b => b.Url)
            .IsUnique();
```

## 12. 继承

继承通常被用来控制实体类接口如何映射到数据库表结构中。在EF CORE 当前版本中，TPC和TPT暂不被支持，TPH是默认且唯一的继承方式。

那么什么是TPH（table-per-hierarchy）呢？顾名思义，一种继承结构全部映射到一张表中，比如Person父类，Student子类和Teacher子类，由EF  CORE映射到数据库中时，将会只存在Person类，而Student和Teacher将以列标识的形式出现。

目前只有Fluent Api方式支持TPH，具体实体类代码如下，其中RssBlog继承自Blog。

```
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}

public class RssBlog : Blog
{
    public string RssUrl { get; set; }
}
```

在MyContext中，我们将原来的代码修改成如下形式：

```
class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasDiscriminator<string>("blog_type")
            .HasValue<Blog>("blog_base")
            .HasValue<RssBlog>("blog_rss");
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}

public class RssBlog : Blog
{
    public string RssUrl { get; set; }
}
```

观察OnModelCreating方法，HasDiscriminator提供修改标识列名的功能，HasValue提供新增或修改实体时，根据实体类型将不同的标识自动写入标识列中。如新增Blog时，blog_type列将写入blog_base字符串，新增RssBlog时，blog_type列将写入blog_rss字符串。

> 笔者不推荐用继承的方式设计数据库，只是这个功能相对新奇，就列出来说了。

## 13. 关系

关系型数据库模型的设计中，最重要的一点便是“关系”的设计了。常见的关系有1-1,1-n，n-n，除此以外，关系的两边还有可空不可空的控制。那么在EF CORE中，我们怎么实现这些关系呢？

以下内容用代码的方式给出了一对一，一对多和多对多的关系，两边关系设为不可空。其实可空不可空的控制十分简单，只要注意是否需要加上IsRequired的扩展Api即可。

不得不说，相比EF6.X的HasRequired和WithOptional等方法，EF CORE中的Api和关系配置清晰直观了不少。

> 唯一需要注意的是，关系设置请从子端（如User和Blog呈一对多对应时，从Blog开始）开始，否则配置不慎容易出现多个外键的情况。

```
public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Blog与Post之间为 1 - N 关系
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                // 使用HasConstraintName方法配置外键名称
                .HasConstraintName("ForeignKey_Post_Blog")
                .IsRequired();

            // User与UserAccount之间为 1 - 1 关系
            modelBuilder.Entity<UserAccount>()
                .HasKey(c => c.UserAccountId);

            modelBuilder.Entity<UserAccount>()
                .HasOne(c => c.User)
                .WithOne(c => c.UserAccount)
                .HasForeignKey<UserAccount>(c => c.UserAccountId)
                .IsRequired();

            // User与Blog之间为 1 - N 关系
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.User)
                .WithMany(c => c.Blogs)
                .HasForeignKey(c => c.UserId)
                .IsRequired();

            // Post与Tag之间为 N - N 关系
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new {t.PostId, t.TagId});

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId)
                .IsRequired();

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId)
                .IsRequired();
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<Blog> Blogs { get; set; }
        public UserAccount UserAccount { get; set; }
    }

    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public string UserAccountName { get; set; }
        public bool IsValid { get; set; }
        public User User { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public List<PostTag> PostTags { get; set; }
    }

    public class Tag
    {
        public string TagId { get; set; }
        public List<PostTag> PostTags { get; set; }
    }

    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
```

## 14. Console中的EntityframeworkCore（2017年7月21日新增）

> 工作中时常会用到一些简单的EF场景，使用Console是最方便不过了，所以特此记录下。

- 新建一个APS.NET CORE WEB模板项目
- 安装相关Nuget包

```
    //Sql Server Database Provider
    Install-Package Microsoft.EntityFrameworkCore.SqlServer
    
    //提供熟悉的Add-Migration，Update-Database等Powershell命令，不区分关系型数据库类型
    Install-Package Microsoft.EntityFrameworkCore.Tools 
```

- 自定义Context

```
public class MyContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
```

注意我们删除了 `public MyContext(DbContextOptions<MyContext> options) : base(options)` 构造方法，以便我们可以直接 `using(var context = new MyContext())` 的方法。简单来说，当你有依赖注入的需求时，便需要使用第一种构造模型。

- 添加中文解码和 Configuration Nuget包

```
Install-Package System.Text.Encoding.CodePages
Install-Package Microsoft.Extensions.Configuration.Json
Install-Package Microsoft.Extensions.Configuration.EnvironmentVariables

// 需要新增appsettings.json文件，并添加ConnectionStrings节点，用于存放连接字符串。
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Console4;Trusted_Connection=True;MultipleActiveResultSets=true;"
  },
```

- 将以下代码添加到MyContext中

```
private static IConfigurationRoot Configuration { get; set; }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    //.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\.."))

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();

    Configuration = builder.Build();

    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
}
```

- 在powershell中定位到MyContext所在文件夹运行相关迁移命令，要注意的是，appsettings文件也需要放到此文件夹中

```
Add-Migration Initialize
Update-Database
```

之后便可以在Console中使用形如 `using (var context = new MyContext())`的语法对数据库进行操作了。

## 15. 参考链接和优秀博客

1. [EF CORE OFFICIAL DOC](https://docs.microsoft.com/zh-cn/ef/core/)
2. [Introduction to Entity Framework](https://msdn.microsoft.com/en-us/library/aa937723(v=vs.113).aspx)
3. [Feature Comparison](https://docs.microsoft.com/zh-cn/ef/efcore-and-ef6/features)
4. [Entity Framework教程(第二版)](http://www.cnblogs.com/lsxqw2004/p/4701979.html)





标签: [EntityFramework](https://www.cnblogs.com/Wddpct/tag/EntityFramework/), [基础](https://www.cnblogs.com/Wddpct/tag/基础/), [.NET Core](https://www.cnblogs.com/Wddpct/tag/.NET Core/)

​         [好文要顶](javascript:void(0);)             [关注我](javascript:void(0);)     [收藏该文](javascript:void(0);)     [![img](assets/icon_weibo_24-1558594494824.png)](javascript:void(0);)     [![img](assets/wechat-1558594494825.png)](javascript:void(0);) 

![img](assets/20151216145018.png)

​             [白细胞](https://home.cnblogs.com/u/Wddpct/) 

 

​     



[« ](https://www.cnblogs.com/Wddpct/p/6801615.html) 上一篇：[从输入url到页面返回到底发生了什么](https://www.cnblogs.com/Wddpct/p/6801615.html)
[» ](https://www.cnblogs.com/Wddpct/p/6885817.html) 下一篇：[Dapper连接与事务的简单封装](https://www.cnblogs.com/Wddpct/p/6885817.html)