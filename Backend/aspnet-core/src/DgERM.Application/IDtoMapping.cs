using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgERM
{
    /// <summary>
    ///     实现该接口以进行映射规则创建
    /// </summary>
//    /// 
//    定义抽象接口IDtoMapping

//应用服务层根目录创建IDtoMapping接口，定义CreateMapping方法由映射规则类实现。
    internal interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}
