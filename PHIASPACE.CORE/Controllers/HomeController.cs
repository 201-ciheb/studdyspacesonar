using System.Diagnostics;
using System.Security.Claims;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.Models;
using PHIASPACE.CORE.Utility;

namespace PHIASPACE.CORE.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IProjectService _projectService;
    private readonly UserManager<PhiaSpaceUser> _userManager;
    private readonly IAppService _appService;

    public HomeController(IProjectService projectService, UserManager<PhiaSpaceUser> userManager, IAppService appService)
    {
        _projectService = projectService;
        _userManager=userManager;
        _appService=appService;
    }

    public IActionResult Index()
    {
        List<PhiaApp> apps = new List<PhiaApp>();
        var value=Request.Cookies["PHIASpace.Project"];
        if(!string.IsNullOrEmpty(value)){
            var project = _projectService.GetProject(value).Result;
            if(project == null)
                return RedirectToAction("ProjectError", new {project=value});
            apps.AddRange(_appService.GetProjectApps(project.ProjectAbbrev).Result);
        }
        return View(apps);
    }

    
    public IActionResult Faqs()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ProjectError(string project)
    {
        return View("ProjectError", project);
    }

    [HttpGet]
    public IActionResult ProjectErrorReset()
    {
        Response.Cookies.Delete("PHIASpace.Project");
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddDays(1) }
        );
        return LocalRedirect(returnUrl);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetProjectView(){
        UiModel uiModel = new UiModel();
        var user_id = _userManager.GetUserAsync(User).Result.Id;
        uiModel.LoadUiModel(new DbUtils(_projectService), user_id);
        return PartialView("CountryCanvas",uiModel);
    }

    [HttpGet]
    public dynamic ChangeProject(string project, string returnUrl)
    {
        Response.Cookies.Append("PHIASpace.Project", project, new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddDays(1) });
        //return LocalRedirect(returnUrl);
        return project;
    }


}
