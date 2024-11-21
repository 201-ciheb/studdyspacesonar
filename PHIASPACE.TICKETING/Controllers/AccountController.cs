using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.TICKETING.Controllers{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult LogOut()
        {
            return SignOut("PHIASpace", "oidc");
        }
    }
}