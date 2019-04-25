using System.Threading.Tasks;
using DgERM.Configuration.Dto;

namespace DgERM.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
