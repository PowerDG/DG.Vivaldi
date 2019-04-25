using Abp.Events.Bus;
using DgERM.Authorization.Users;
using DgERM.DgCore.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgERM.DgDemo
{
//    这个类包含了处理这个事件所需要包含的属性。
//EventData 定义了 EventSource(那个对象触发了该事件) 
//        以及 EventTime(什么时候触发的) 属性。
    public class TaskEventData : EventData
    {
        public Task Task { get; set; } 
    }

    public class TaskCreatedEventData : TaskEventData
    {
        public User CreatorUser { get; set; }
    }

    public class TaskCompletedEventData : TaskEventData
    {
        public User CompletorUser { get; set; }
        public int TaskId { get; internal set; }
    }
}
