
using AutoMapper;
using Dg.ERM.Authorization.ExtendInfos;
using Dg.ERM.Authorization.ExtendInfos.Dtos;

namespace Dg.ERM.Authorization.ExtendInfos.Mapper
{

	/// <summary>
    /// 配置ExtendInfo的AutoMapper
    /// </summary>
	internal static class ExtendInfoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ExtendInfo,ExtendInfoListDto>();
            configuration.CreateMap <ExtendInfoListDto,ExtendInfo>();

            configuration.CreateMap <ExtendInfoEditDto,ExtendInfo>();
            configuration.CreateMap <ExtendInfo,ExtendInfoEditDto>();

        }
	}
}
