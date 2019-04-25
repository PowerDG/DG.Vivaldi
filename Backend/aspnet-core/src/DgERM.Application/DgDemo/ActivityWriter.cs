using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgERM.DgDemo
{
    public class ActivityWriter :
    IEventHandler<TaskCompletedEventData>,
    IEventHandler<TaskCreatedEventData>,

    ITransientDependency
    {
         
        public void HandleEvent(TaskCompletedEventData eventData)
        {
            //TODO: handle the event...
            WriteActivity("A task is completed by id = " + eventData.TaskId);
        }
         
        public void HandleEvent(TaskCreatedEventData eventData)
        {
            //TODO: handle the event...
        }


        private void WriteActivity(string v)
        {
            throw new NotImplementedException();
        }

    }

    public class ActivityWriter2 : 
        IEventHandler<TaskEventData>, ITransientDependency
    {
        public void HandleEvent(TaskEventData eventData)
        {
            if (eventData is TaskCreatedEventData)
            {
                //...
            }
            else if (eventData is TaskCompletedEventData)
            {
                //...
            }
        }
    }
}
