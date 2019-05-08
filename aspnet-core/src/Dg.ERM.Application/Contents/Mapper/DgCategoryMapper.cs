
using AutoMapper;
using Dg.ERM.Contents;
using Dg.ERM.Contents.Dtos;

namespace Dg.ERM.Contents.Mapper
{

	/// <summary>
    /// 配置DgCategory的AutoMapper
    /// </summary>
	internal static class DgCategoryMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <DgCategory,DgCategoryListDto>();
            configuration.CreateMap <DgCategoryListDto,DgCategory>();

            configuration.CreateMap <DgCategoryEditDto,DgCategory>();
            configuration.CreateMap <DgCategory,DgCategoryEditDto>();

        }
	}
}
