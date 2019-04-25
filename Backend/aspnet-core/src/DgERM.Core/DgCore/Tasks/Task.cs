using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using DgERM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgERM.DgCore.Tasks
{
    public class Task : Entity, IHasCreationTime
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024;//64kb

        public long? AssignedPersonId { get; set; }

        [ForeignKey("AssignedPersonId")]
        public User AssignedPerson { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public TaskState State { get; set; }
        public DateTime CreationTime { get; set; }

        public Task()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open; ;
        }

        public Task(string title, string description = null) : this()
        {
            Title = title;
            Description = description;
        }




        //实体变更 
        //    对于实体变更，ABP也定义了一下事件的泛型版本: 
        //    EntityCreatingEventData<TEntity>, EntityCreatedEventData<TEntity>, 
        //    EntityUpdatingEventData<TEntity>, EntityUpdatedEventData<TEntity>, 
        //    EntityDeletingEventData<TEntity> and EntityDeletedEventData<TEntity>。
        //    还有，对于泛型事件：
        //    EntityChangingEventData<TEntity> 和 EntityChangedEventData<TEntity> 
        //    是在某个实体发生插入，更新或者删除的时候出发。 

        //    ing 事件是指(例如：EntityUpdating) 该类事件被触发是在保存实体更改到数据库之前。
        //    所以你可以在这些事件里面抛出某个异常来回滚工作单元来阻止当前的更改操作。
            
        //    ed 事件是指(例如：EntityUpdated) 该类事件被触发是在保存实体更改到数据库之后，
        //    这样就不可能有机会回滚工作单元。


        //    实体更改事件被定义在 Abp.Events.Bus.Entities 命名空间，
        //        并且在某个实体被插入，更新或者删除的时候，ABP可以自动的触发它们。
        //        如果你有一个Person实体, 可以注册 EntityCreatedEventData<Person> 事件来产生通知，
        //        当一个新的的Person实体被创建且插入到数据库的时候，ABP就会自动的触发该事件。
        //        这些事件也支持继承。如果Student类继承自Person类，
        //        并且你注册了 EntityCreatedEventData<Person> 事件, 
        //        接着你将会在Person或Student实体被插入后后收到触发。 
    }

    public enum TaskState : byte
    {
        Open = 0,
        Completed = 1
    }
}
