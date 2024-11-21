using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.TRAINING.Controllers{
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
            return Redirect(coreUrl);
        }
    }
}