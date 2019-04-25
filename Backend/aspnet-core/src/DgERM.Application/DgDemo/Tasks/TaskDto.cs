using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using DgERM.DgCore.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DgERM.DgDemo.Tasks
{
    /// <summary>
    /// A DTO class that can be used in various application service methods when needed to send/receive Task objects.
    /// </summary>
    /// 


//        使用AutoMapper自动映射DTO与实体
//4.1. 简要介绍AutoMapper
//开始之前，如果对AutoMapper不是很了解，
//建议看下这篇文章AutoMapper小结。
//    http://www.cnblogs.com/jobs2/p/3503990.html
//AutoMapper的使用步骤，简单总结下：


//创建映射规则（Mapper.CreateMap<source, destination>();）
//类型映射转换（Mapper.Map<source, destination>(sourceModel)）

//在Abp中有两种方式创建映射规则：

//特性数据注解方式：

//AutoMapFrom、AutoMapTo 特性创建单向映射
//AutoMap 特性创建双向映射


//代码创建映射规则：

//Mapper.CreateMap<source, destination>();

//作者：圣杰
//链接：https://www.jianshu.com/p/da69ca7b27c6
//来源：简书
//简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。
    [AutoMapTo(typeof(Task))] //定义单向映射    
    public class TaskDto : EntityDto
    {
        public long? AssignedPersonId { get; set; }

        public string AssignedPersonName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        //This method is just used by the Console Application to list tasks
        public override string ToString()
        {
            return string.Format(
                "[Task Id={0}, Description={1}, CreationTime={2}, AssignedPersonName={3}, State={4}]",
                Id,
                Description,
                CreationTime,
                AssignedPersonId,
                (TaskState)State
                );
        }
    }

    //public enum TaskState : byte
    //{
    //    Open = 0,
    //    Completed = 1
    //}

    [AutoMapTo(typeof(Task))] //定义单向映射    
    public class GetTasksOutput
    {
        public List<TaskDto> Tasks { get; set; }
    }

    [AutoMapTo(typeof(Task))] //定义单向映射    
    public class CreateTaskInput
    {
        public int? AssignedPersonId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        public TaskState State { get; set; }
        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedPersonId = {0}, Description = {1}]", AssignedPersonId, Description);
        }
    }/// <summary>
     /// This DTO class is used to send needed data to <see cref="ITaskAppService.UpdateTask"/> method.
     /// 
     /// Implements <see cref="ICustomValidate"/> for additional custom validation.
     /// </summary>

    [AutoMapTo(typeof(Task))] //定义单向映射    
    public class UpdateTaskInput : ICustomValidate
    {
        [Range(1, Int32.MaxValue)] //Data annotation attributes work as expected.
        public int Id { get; set; }

        public int? AssignedPersonId { get; set; }

        public TaskState? State { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (AssignedPersonId == null && State == null)
            {
                context.Results.Add(new ValidationResult("Both of AssignedPersonId and State can not be null in order to update a Task!", new[] { "AssignedPersonId", "State" }));
            }
        }

        public override string ToString()
        {
            return string.Format("[UpdateTaskInput > TaskId = {0}, AssignedPersonId = {1}, State = {2}]", Id, AssignedPersonId, State);
        }
    }


    [AutoMapTo(typeof(Task))] //定义单向映射    
    public class GetTasksInput
    {
        public TaskState? State { get; set; }

        public int? AssignedPersonId { get; set; }
        public int? Sorting { get;   set; }
        public string Filter { get;   set; }
    }
}
