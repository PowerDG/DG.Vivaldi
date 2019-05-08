

using Abp.Domain.Services;

namespace Dg.ERM
{
	public abstract class ERMDomainServiceBase : DomainService
	{
		/* Add your common members for all your domain services. */
		/*在领域服务中添加你的自定义公共方法*/





		protected ERMDomainServiceBase()
		{
			LocalizationSourceName = ERMConsts.LocalizationSourceName;
		}
	}
}
