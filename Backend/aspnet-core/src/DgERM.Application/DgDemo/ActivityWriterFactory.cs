using Abp.Events.Bus.Factories;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgERM.DgDemo
{
    public class ActivityWriterFactory : IEventHandlerFactory
    {
//        此次，事件总线为每个事件创建一个新的ActivityWriter，
//            如果它是disposable（可释放），并调用ActivityWriter.Dispose方法。

//最后，你可以注册一个事件处理程序工作，负责处理程序的创建。
//            一个处理程序工厂有两个方法：
//            GetHandler和ReleaseHandler。
//            例如：

        public IEventHandler GetHandler()
        {
            return new ActivityWriter();
        }

        public Type GetHandlerType()
        {
            throw new NotImplementedException();
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            //TODO: release/dispose the activity writer instance (handler)
        }
    }
}
