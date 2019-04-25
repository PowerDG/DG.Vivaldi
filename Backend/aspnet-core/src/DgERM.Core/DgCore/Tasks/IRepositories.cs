using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgERM.DgCore.Tasks
{
    /// <summary>
    /// 自定义仓储示例
    /// </summary>
    public interface IBackendTaskRepository : IRepository<Task>
    {
        /// <summary>
        /// 获取某个用户分配了哪些任务
        /// </summary>
        /// <param name="personId">用户Id</param>
        /// <returns>任务列表</returns>
        List<Task> GetTaskByAssignedPersonId(long personId);


//        仓储的注意事项

//仓储方法中，ABP自动进行数据库连接的开启和关闭。
//仓储方法被调用时，数据库连接自动开启且启动事务。
//当仓储方法调用另外一个仓储的方法，它们实际上共享的是同一个数据库连接和事务。
//仓储对象都是暂时性的，因为IRepository接口默认继承自ITransientDependency接口。所以，仓储对象只有在需要注入的时候，才会由Ioc容器自动创建新实例。
//默认的泛型仓储能满足我们大部分的需求。只有在不满足的情况下，才创建定制化的仓储。

//作者：圣杰
//链接：https://www.jianshu.com/p/6e90a94aeba4
//来源：简书
//简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。
    }
}
