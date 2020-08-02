# [【无私分享：ASP.NET CORE 项目实战（第五章）】Repository仓储 UnitofWork](https://www.cnblogs.com/yuangang/p/5725201.html)

# 目录索引　

 

[【无私分享：ASP.NET CORE 项目实战】目录索引](http://www.cnblogs.com/yuangang/p/5694064.html) 

# 简介

 　　本章我们来创建仓储类Repository 并且引入 UnitOfWork

# 我对UnitOfWork的一些理解

 

　　 UnitOfWork 工作单元，对于这个的解释和实例，网上很多很多大神之作，我在这就不班门弄斧了，只是浅谈 一下个人理解，不一定对，希望大家指正：

　　UnitOfWork  看了很多，个人理解就是 统一事务，在很多操作中我们可能是进行多项操作的，比如  同时保存两个表的数据，如果我们先保存一个表（SaveChanges()之后），再保存另一个表（SaveChanges()）其实是两个不同的事务，通俗的讲，我们对数据库提交了两次数据。而UnitOfWork  就是 把本次操作（分别保存两张表）放在统一事务中，所有的CRUD完成后，统一提交数据，即保证数据的一致性，又减少了提交次数，提高性能。

　　举个例子，我们之前的Repository的保存操作：



````csharp
　　Repository:

　　public virtual bool Save(T entity)
        　　{
            　　　　_Context.Add(entity);
            　　　　return _Context.SaveChanges() > 0;           
        　　}

 

　　Controller: 

　　public ActionResult Save()

　　{

　　　　var Users=new Users(){Id=1,UserName="张三"};

　　　　var Logs=new Logs(){log="注册了用户张三"};

　　　　_UserService.Save(Users);

　　　　_LogService.Save(Logs);

　　}


````





因为本身EF就是带事务的，但是_Context.SaveChanges()之后，事务就结束了，也提交了数据。所以，上面的例子 应该是开启了两次事务，提交了两次数据（一次是Users 一次是 Logs）

这显然是不合理的，首先不说提交两次数据性能的问题，如果用户注册失败，那么日志也不应该添加，应该回滚。但是，显然，上面没有这么做。

所以有了UnitOfWork。

使用UnitOfWork之后，代码应该是这样的：

 

　　UnitOfWork:

　　public bool Commit()
　　{
　　　　return _Context.SaveChanges() > 0; 
　　}

 

　　Repository:

　　public virtual bool Save(T entity)
　　{
　　　　_Context.Add(entity);
　　}

 

　　Controller: 

　　public ActionResult Save()

　　{

　　　　var Users=new Users(){Id=1,UserName="张三"};

　　　　var Logs=new Logs(){log="注册了用户张三"};

　　　　_UserService.Save(Users);

　　　　_LogService.Save(Logs);

　　　　_UnitOfWork.Commit();

　　}

　　

#  

# UnitOfWork接口和实现类

 我们在wkmvc.Core类库下，新建一个接口 IUnitOfWork：

[![复制代码](../assets/copycode-1557672705351.gif)](javascript:void(0);)

```
 1 namespace wkmvc.Core
 2 {
 3     /// <summary>
 4     /// Describe：工作单元接口
 5     /// Author：yuangang
 6     /// Date：2016/07/16
 7     /// Blogs:http://www.cnblogs.com/yuangang
 8     /// </summary>
 9     public interface IUnitOfWork
10     {
11         bool Commit();
12     }
13 }
```

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

  

　　IUnitOfWork 实现类 UnitOfWork：

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
 1 using System;
 2 using wkmvc.Data;
 3 
 4 namespace wkmvc.Core
 5 {
 6     /// <summary>
 7     /// Describe：工作单元实现类
 8     /// Author：yuangang
 9     /// Date：2016/07/16
10     /// Blogs:http://www.cnblogs.com/yuangang
11     /// </summary>
12     public class UnitOfWork : IUnitOfWork, IDisposable
13     {
14         #region 数据上下文
15 
16         /// <summary>
17         /// 数据上下文
18         /// </summary>
19         private ApplicationDbContext _Context;
20         public UnitOfWork(ApplicationDbContext Context)
21         {
22             _Context = Context;
23         }
24 
25         #endregion
26 
27         public bool Commit()
28         {
29             return _Context.SaveChanges() > 0;
30         }
31 
32         public void Dispose()
33         {
34             if(_Context!=null)
35             {
36                 _Context.Dispose();
37             }
38             GC.SuppressFinalize(this);
39         }
40     }
41 }
```

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

 

　　 这样，UnitOfWork 就完成了。下面我们来添加仓储：

 

# 仓储 IRepository、Repository

新建接口 IRepository 添加基本的操作方法：

 

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
 1 using System;
 2 using System.Linq.Expressions;
 3 
 4 namespace wkmvc.Core
 5 {
 6     /// <summary>
 7     /// Describe：仓储接口
 8     /// Author：yuangang
 9     /// Date：2016/07/16
10     /// Blogs:http://www.cnblogs.com/yuangang
11     /// </summary>
12     /// <typeparam name="T">实体模型</typeparam>
13     public interface IRepository<T> where T : class
14     {
15         #region 单模型 CRUD 操作
16 
17         /// <summary>
18         /// 增加一条记录
19         /// </summary>
20         /// <param name="entity">实体模型</param>
21         /// <param name="IsCommit">是否提交（默认提交）</param>
22         /// <returns></returns>
23         bool Save(T entity, bool IsCommit = true);
24 
25         /// <summary>
26         /// 更新一条记录
27         /// </summary>
28         /// <param name="entity">实体模型</param>
29         /// <param name="IsCommit">是否提交（默认提交）</param>
30         /// <returns></returns>
31         bool Update(T entity, bool IsCommit = true);
32 
33         /// <summary>
34         /// 增加或更新一条记录
35         /// </summary>
36         /// <param name="entity">实体模型</param>
37         /// <param name="IsSave">是否增加</param>
38         /// <param name="IsCommit">是否提交（默认提交）</param>
39         /// <returns></returns>
40         bool SaveOrUpdate(T entity, bool IsSave, bool IsCommit = true);
41 
42         /// <summary>
43         /// 通过Lamda表达式获取实体
44         /// </summary>
45         /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
46         /// <returns></returns>
47         T Get(Expression<Func<T, bool>> predicate);
48 
49         /// <summary>
50         /// 删除一条记录
51         /// </summary>
52         /// <param name="entity">实体模型</param>
53         /// <param name="IsCommit">是否提交（默认提交）</param>
54         /// <returns></returns>
55         bool Delete(T entity, bool IsCommit = true);
56 
57         #endregion
58     }
59 }
```

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

 

　　 实现类 Repository :

 

 

 

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
  1 using Microsoft.EntityFrameworkCore;
  2 using System;
  3 using System.Linq;
  4 using System.Linq.Expressions;
  5 using wkmvc.Data;
  6 
  7 namespace wkmvc.Core
  8 {
  9     /// <summary>
 10     /// Describe：仓储实现类
 11     /// Author：yuangang
 12     /// Date：2016/07/16
 13     /// Blogs:http://www.cnblogs.com/yuangang
 14     /// </summary>
 15     /// <typeparam name="T">实体模型</typeparam>
 16     public abstract class Repository<T> : IRepository<T> where T : class
 17     {
 18         #region 数据上下文
 19 
 20         /// <summary>
 21         /// 数据上下文
 22         /// </summary>
 23         private ApplicationDbContext _Context;
 24 
 25         /// <summary>
 26         /// 工作单元
 27         /// </summary>
 28         UnitOfWork _UnitOfWork;
 29 
 30         public Repository(ApplicationDbContext Context)
 31         {
 32             _Context = Context;
 33             _UnitOfWork = new UnitOfWork(_Context);
 34         }
 35 
 36 
 37         #endregion
 38 
 39         #region 单模型 CRUD 操作
 40 
 41         /// <summary>
 42         /// 增加一条记录
 43         /// </summary>
 44         /// <param name="entity">实体模型</param>
 45         /// <param name="IsCommit">是否提交（默认提交）</param>
 46         /// <returns></returns>
 47         public virtual bool Save(T entity,bool IsCommit=true)
 48         {
 49             _Context.Add(entity);
 50             if (IsCommit)
 51                 return _UnitOfWork.Commit();
 52             else
 53                 return false;
 54         }
 55 
 56         /// <summary>
 57         /// 更新一条记录
 58         /// </summary>
 59         /// <param name="entity">实体模型</param>
 60         /// <param name="IsCommit">是否提交（默认提交）</param>
 61         /// <returns></returns>
 62         public virtual bool Update(T entity, bool IsCommit = true)
 63         {
 64             _Context.Attach(entity);
 65             _Context.Entry(entity).State = EntityState.Modified;
 66             if (IsCommit)
 67                 return _UnitOfWork.Commit();
 68             else
 69                 return false;
 70         }
 71 
 72         /// <summary>
 73         /// 增加或更新一条记录
 74         /// </summary>
 75         /// <param name="entity">实体模型</param>
 76         /// <param name="IsSave">是否增加</param>
 77         /// <param name="IsCommit">是否提交（默认提交）</param>
 78         /// <returns></returns>
 79         public virtual bool SaveOrUpdate(T entity, bool IsSave, bool IsCommit = true)
 80         {
 81             return IsSave ? Save(entity, IsCommit) : Update(entity, IsCommit);
 82         }
 83 
 84         /// <summary>
 85         /// 通过Lamda表达式获取实体
 86         /// </summary>
 87         /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
 88         /// <returns></returns>
 89         public virtual T Get(Expression<Func<T, bool>> predicate)
 90         {
 91             return _Context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
 92         }
 93 
 94 
 95         /// <summary>
 96         /// 删除一条记录
 97         /// </summary>
 98         /// <param name="entity">实体模型</param>
 99         /// <param name="IsCommit">是否提交（默认提交）</param>
100         /// <returns></returns>
101         public virtual bool Delete(T entity, bool IsCommit = true)
102         {
103             if (entity == null) return false;
104             _Context.Remove(entity);
105 
106             if (IsCommit)
107                 return _UnitOfWork.Commit();
108             else
109                 return false;
110         }
111 
112         #endregion
113 
114 
115     }
116 }
```

[![复制代码](http://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

我们都添加了一个  bool IsCommit = true 参数，是为了方便，如果仅仅是进行一项操作，那就没必要使用 UnitOfWork 统一 直接 SaveChanages() 就行了

 

 

 

我们来看下使用： 

 　　//第一种
        　　public bool TestOne()
        　　{
            　　　　var users = new SYS_USER() { UserName="张三",Account="zhangsan",Password="123456" };

​            　　　　return _UserService.Save(users);
​        　　}


        　　//第二种
        　　public bool TestTwo()
        　　{
            　　　　var users1 = new SYS_USER() { UserName = "张三", Account = "zhangsan", Password = "123456" };
            　　　　var users2 = new SYS_USER() { UserName = "李四", Account = "lisi", Password = "456789" };
            　　　　var users3 = new SYS_USER() { UserName = "王五", Account = "wangwu", Password = "321654" };

​            　　　　_UserService.Save(users1);
​            　　　　_UserService.Save(users2);
​            　　　　_UserService.Save(users3);

​            　　　　return _UnitOfWork.Commit();
​        　　}

 

本篇主要是介绍用法，具体的解释清大家参考大神们的讲解，如有不对的地方希望指正。具体的实现代码，我们后面一起一点一点写。

 

 

# 给大家贴一下个人实现的仓储 IRepository、Repository代码：

 

# IRepository：

![img](../assets/ContractedBlock.gif)  View Code

 

# Repository：

![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif)  View Code

 

 

 

 

 

希望跟大家一起学习Asp.net Core 

刚开始接触，水平有限，很多东西都是自己的理解和翻阅网上大神的资料，如果有不对的地方和不理解的地方，希望大家指正！

虽然Asp.net Core 现在很火热，但是网上的很多资料都是前篇一律的复制，所以有很多问题我也暂时没有解决，希望大家能共同帮助一下！

 

原创文章 转载请尊重劳动成果 [http://yuangang.cnblogs.com](http://yuangang.cnblogs.com/)