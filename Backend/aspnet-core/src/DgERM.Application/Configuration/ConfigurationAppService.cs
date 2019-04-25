using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DgERM.Configuration.Dto;

namespace DgERM.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DgERMAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
