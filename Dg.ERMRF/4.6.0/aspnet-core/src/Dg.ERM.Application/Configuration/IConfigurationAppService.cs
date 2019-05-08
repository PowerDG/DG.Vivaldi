using System.Threading.Tasks;
using Dg.ERM.Configuration.Dto;

namespace Dg.ERM.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
