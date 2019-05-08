using Microsoft.AspNetCore.Antiforgery;
using Dg.ERM.Controllers;

namespace Dg.ERM.Web.Host.Controllers
{
    public class AntiForgeryController : ERMControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
