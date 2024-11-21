using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.MAPPINGLISTING.Models;

namespace PHIASPACE.MAPPINGLISTING.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //[Authorize(Policy = "PERMISSION.PROJECT.CREATE")]
    public IActionResult Index()
    {
        string projectCookie = HttpContext.Request.Cookies["PHIASpace.Project"];
        
        return View();
    }

    public IActionResult Map()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
