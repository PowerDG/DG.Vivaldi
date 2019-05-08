using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dg.ERM.Authorization;

namespace Dg.ERM
{
    [DependsOn(
        typeof(ERMCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ERMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ERMAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ERMApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
