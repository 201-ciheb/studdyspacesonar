using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.RTDMS.Controllers;

[Authorize]
public class WallboardController : Controller
{

    public IActionResult Household()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult National()
    {
        return View();
    }

    public IActionResult Age()
    {
        return View();
    }

    public IActionResult Lab()
    {
        return View();
    }

    public IActionResult Screening()
    {
        return View();
    }
}