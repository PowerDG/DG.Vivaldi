using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DgERM.Authorization;

namespace DgERM
{
    [DependsOn(
        typeof(DgERMCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DgERMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DgERMAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DgERMApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators
            //    .Add(
            //    // Scan the assembly for classes which inherit from AutoMapper.Profile
            //    cfg => cfg.AddProfiles(thisAssembly)
 
            //);
            .Add(mapper =>
            {
                var mappers = IocManager.IocContainer.ResolveAll<IDtoMapping>();
                foreach (var dtomap in mappers)
                    dtomap.CreateMapping(mapper);
            }); 
        }
    }
}
