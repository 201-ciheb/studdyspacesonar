using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.RTDMS.Controllers{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return SignOut("PHIASpace", "oidc");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RedirectToCore(){
            var coreUrl = _configuration["CoreUrl"];
            if (!string.IsNullOrEmpty(coreUrl))
                return Redirect(coreUrl);
            else
                return Redirect("/");
        }
    }
}