using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.CORE.Controllers;

[Authorize(Roles ="Super Admin")]
public class OauthController : Controller
{
    public IActionResult Clients(){
        return View();
    }

    public IActionResult RedirectUri(){
        return View();
    }

    public IActionResult LogOutUri(){
        return View();
    }
}