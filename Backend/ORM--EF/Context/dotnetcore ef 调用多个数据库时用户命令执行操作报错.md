dotnetcore ef 调用多个数据库时用户命令执行操作报错

http://www.cnblogs.com/citycomputing/p/9815854.html



1、多个DbContext 时报错：

![img](https://img2018.cnblogs.com/blog/1007357/201810/1007357-20181019125521136-105497218.png)

报错：

More than one DbContext was found. Specify which one to use. Use the  '-Context' parameter for PowerShell commands and the '--context'  parameter for dotnet commands.

解决办法：

dotnet ef migrations add initail -c PermissionDbContext

成功之后

dotnet ef database update -c PermissionDbContext

2、如果 DbContext 在另一个 DLL 中时报错：

![img](https://img2018.cnblogs.com/blog/1007357/201810/1007357-20181019130017711-1101216567.png)

解决办法：

 services.AddDbContext<PermissionDbContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("PermissionConnection"),  b => b.MigrationsAssembly(assemblyName)));

其中 assemblyName 是主DLL 名称的字符串常量。（不知道为什么，它不能为变量 AppDomain.CurrentDomain.FriendlyName）。

 



分类: [DotNetCore](https://www.cnblogs.com/citycomputing/category/1319636.html)



​         [好文要顶](javascript:void(0);)             [关注我](javascript:void(0);)     [收藏该文](javascript:void(0);)     [![img](assets/icon_weibo_24.png)](javascript:void(0);)     [![img](assets/wechat.png)](javascript:void(0);) 

![img](http://pic.cnblogs.com/face/sample_face.gif)

​             [廖贵宾](http://home.cnblogs.com/u/citycomputing/)
​             [关注 - 13](http://home.cnblogs.com/u/citycomputing/followees)
​             [粉丝 - 4](http://home.cnblogs.com/u/citycomputing/followers)         





​                 [+加关注](javascript:void(0);)     

​         0     

​         0     



​     



[« ](https://www.cnblogs.com/citycomputing/p/9792765.html) 上一篇：[DotNetCore 部署到IIS 上](https://www.cnblogs.com/citycomputing/p/9792765.html)
[» ](https://www.cnblogs.com/citycomputing/p/9850029.html) 下一篇：[在dotnetcore的MVC项目中，创建支持 vue.js 的最小工程模板](https://www.cnblogs.com/citycomputing/p/9850029.html)