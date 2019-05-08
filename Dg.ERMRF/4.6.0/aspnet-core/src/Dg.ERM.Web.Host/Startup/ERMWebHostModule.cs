using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dg.ERM.Configuration;

namespace Dg.ERM.Web.Host.Startup
{
    [DependsOn(
       typeof(ERMWebCoreModule))]
    public class ERMWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ERMWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ERMWebHostModule).GetAssembly());
        }
    }
}
