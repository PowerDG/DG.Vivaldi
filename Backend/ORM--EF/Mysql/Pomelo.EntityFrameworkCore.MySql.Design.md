在EntityFrameworkCore项目中添加Pomelo.EntityFrameworkCore.MySql和Pomelo.EntityFrameworkCore.MySql.Design两个包，以便支持MySql。为什么不用Oracle提供的MySql.Data.EntityFrameworkCore呢？在微软的文档《MySQL EF Core Database Provider》中，该包只支持MySql，且还是预览版状态，而在Pomelo EF Core Database Provider for MySQL中，该包不单支持MySQL，还支持MariaDB，所以，怎么选择已经很明显，可以说，笔者是毫不犹豫的选择了Pomelo包。
--------------------- 
作者：上将军 
来源：CSDN 
原文：https://blog.csdn.net/tianxiaode/article/details/78816876 
版权声明：本文为博主原创文章，转载请附上博文链接！







至于测试数据库，笔者选择的是XAMMP带的MySql，版本是10.1.13-MariaDB。数据库所选的字符集为utf8_general_ci。虽然Pomelo建议选择utf8mb4作为字符集（详细信息请参阅Pomelo《Getting Started》），但建议不要选择，因为“max key length is 767 bytes”这个错误会把你折磨死的。出现这个错误的原因是utf8以3字节方式存储一个字符的，而一列的索引长度最大值只能是767字节，如果字段的长度是256，那么，索引的长度就是768了，正好超过1字节，迁移就出问题了。如果使用utf8mb4，那么，一个字符的长度就是4字节，字段的最大长度只能是191了，而这要修改的字段就有很多了，所以在这练习了还是采用utf8算了。在实际项目中，建议使用utf8mb4，但要考虑带索引的字段的长度是191是否足够。要想彻底解决这个问题，需要将字段的索引扩大到3072字段，但实现这个需要执行以下4个步骤：

    SET GLOBAL innodb_file_format=Barracuda;
    SET GLOBAL innodb_file_per_table=ON;
    ROW_FORMAT=DYNAMIC; – or COMPRESSED (goes on end of CREATE)
    innodb_large_prefix=1

以上4个步骤，1、2和4都可以在数据库中直接修改。第3步则有点难度了，因为实现这个需要在创建表格时在CREATE语句中添加附加信息，而这个在迁移代码中暂时没找到解决方案，除非是使用Database First的方式来实现。

MySql的驱动准备好以后，需要修改数据库链接，打开Migrations项目和Web.Host项目中的appsettings.json文件，将ConnectionStrings的Default值修改为“Server=localhost;database=simplecmswithabp;uid=root;pwd=abcd-1234;charset=UTF8;”。在生产环境中，不要使用root作为数据库的连接用户，这个大家应该都懂的。

修改数据库连接后，还需要修改EntityFrameworkCore项目中的SimpleCmsWithAbpDbContextConfigurer.cs文件，将方法中的UseSqlServer方法修改为UseMySql方法，完成后的代码如下：

    public static class SimpleCmsWithAbpDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SimpleCmsWithAbpDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseMySql(connectionString);           
        }
    
        public static void Configure(DbContextOptionsBuilder<SimpleCmsWithAbpDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseMySql(connection);
        }
    }
    
    1
    2
    3
    4
    5
    6
    7
    8
    9
    10
    11
    12
    13
    14

下一步要修改字段长度为256，且需要创建索引的字段的长度。打开20170424115119_Initial_Migrations.cs文件，然后使用搜索功能寻找256，经过一轮搜索和索引字段对比后，最终需要修改的字段包括：

    AbpLanguageTexts表：Key
    AbpUsers表：EmailAddress和NormalizedEmailAddress
    AbpUserLogins表：ProviderKey
    AbpSettings表：Name

长度修改为255就行了。如果使用utf8mb4，需要修改为191。字段修改完成后，将Migrator项目设置为启动项目，然后运行，会看到如下图所示的结果：

---------------------
作者：上将军 
来源：CSDN 
原文：https://blog.csdn.net/tianxiaode/article/details/78816876 
版权声明：本文为博主原创文章，转载请附上博文链接！