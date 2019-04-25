using Microsoft.AspNetCore.Antiforgery;
using DgERM.Controllers;

namespace DgERM.Web.Host.Controllers
{
    public class AntiForgeryController : DgERMControllerBase
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
