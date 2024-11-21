
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.DAL.Model.Enum;
using PHIASPACE.CORE.Models;
using PHIASPACE.CORE.Utility;

namespace PHIASPACE.CORE.Controllers;

[Authorize]
public class AppController : Controller
{
    private readonly IAppService _AppService;
    private readonly IProjectService _projectService;

    public AppController(IAppService AppService, IProjectService projectService)
    {
        _AppService=AppService;
        _projectService = projectService;
    }

    [Authorize(Policy = "PERMISSION.APP.VIEW")]
    public IActionResult Index(){
        return View();
    }

    [HttpGet]
    [Authorize(Policy = "PERMISSION.APP.CREATE")]
    public IActionResult Add(){
        ViewData["Projects"] = new SelectList(_projectService.GetActiveProjects().Result.ToList(), "Id", "ProjectName");
        return View();
    }

    [HttpPost]
    [Authorize(Policy = "PERMISSION.APP.CREATE")]
    public IActionResult Add(PhiaApp app, IFormFile AppIconPath){
        try
        {            
            if(AppIconPath != null){
                if (!Directory.Exists("PHIASpaceDocuments/AppLogo"))
                {
                    Directory.CreateDirectory("PHIASpaceDocuments/AppLogo");
                }
                var savingPath = Path.Combine("PHIASpaceDocuments/AppLogo/", AppIconPath.FileName);
                using (var stream = System.IO.File.Create(savingPath))
                {
                    AppIconPath.CopyTo(stream);
                }
                app.AppIconPath = savingPath;
            }    
            ViewData["Projects"] = new SelectList(_projectService.GetActiveProjects().Result.ToList(), "Id", "ProjectName");
            app.AppStatus = AppStatus.Review.ToString();
            var app_id = _AppService.AddPhiaApp(app).Result;
            if(app_id > 0)
                return RedirectToAction(nameof(Index));
            return View(app);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(app);
        }        
    }

    [HttpGet]    
    [Authorize(Policy = "PERMISSION.APP.EDIT")]
    public IActionResult Edit(int id){
        ViewData["Projects"] = new SelectList(_projectService.GetActiveProjects().Result.ToList(), "Id", "ProjectName");
        var appv = _AppService.GetApp(id).Result;
        return View(appv);
    }

    [HttpPost]
    [Authorize(Policy = "PERMISSION.APP.EDIT")]
    public IActionResult Edit(PhiaApp app, IFormFile AppIconPath){
        try
        {            
            ViewData["Projects"] = new SelectList(_projectService.GetActiveProjects().Result.ToList(), "Id", "ProjectName");
            app.AppStatus = AppStatus.Review.ToString();
            if(AppIconPath != null){
                if (!Directory.Exists("PHIASpaceDocuments/AppLogo"))
                {
                    Directory.CreateDirectory("PHIASpaceDocuments/AppLogo");
                }
                var savingPath = Path.Combine("PHIASpaceDocuments/AppLogo/", AppIconPath.FileName);
                using (var stream = System.IO.File.Create(savingPath))
                {
                    AppIconPath.CopyTo(stream);
                }
                app.AppIconPath = savingPath;
            }          
            var app_id = _AppService.UpdatePhiaApp(app).Result;
            if(app_id > 0)
                return RedirectToAction(nameof(Index));
            return View(app);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(app);
        }            
    }

    public JsonResult GetActiveApps(GridTableModel grid)
    {
        List<PhiaApp> records;
        int total;
        var query = _AppService.GetActiveApps().Result;
        // if (!string.IsNullOrWhiteSpace(grid.name))
        // {
        //     query = query.Where(q => q.AppName.Contains(name)).ToList();
        // }        
        if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
        {
            if (grid.direction.Trim().ToLower() == "asc")
            {
                switch (grid.order.Trim().ToLower())
                {
                    case "Name":
                        query = query.OrderBy(q => q.AppName).ToList();
                        break;
                }
            }
            else
            {
                switch (grid.order.Trim().ToLower())
                {
                    case "Name":
                        query = query.OrderByDescending(q => q.AppName).ToList();
                        break;
                }
            }
        }
        else
        {
            query = query.OrderBy(q => q.Id).ToList();
        }
        total = query.Count();
        if (grid.page.HasValue && grid.limit != 0)
        {
            int start = (grid.page.Value - 1) * grid.limit;
            records = query.Skip(start).Take(grid.limit).ToList();
        }
        else
        {
            records = query.ToList();
        }
        return this.Json(new { records, total });
    }

    public async Task<JsonResult> GetApps(GridTableModel grid)
    {
        List<PhiaApp> records;
        int total;
        var query = _AppService.GetApps();
        //search
        if(!string.IsNullOrWhiteSpace(grid.search)){
            query = query.Where(q => q.AppName.Contains(grid.search) || q.AppDescription.Contains(grid.search) || q.AppStatus.Contains(grid.search));
        }
        //order
        if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
        {
            var propertyName = grid.order.Trim();
            var parameter = Expression.Parameter(typeof(PhiaApp)); 
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<PhiaApp, object>>(Expression.Convert(property, typeof(object)), parameter);

            if (grid.direction.Trim().ToLower() == "asc")
            {
                query = query.OrderBy(lambda);
            }
            else
            {
                query = query.OrderByDescending(lambda);
            }
        }
        else
        {
            query = query.OrderBy(q => q.Id); // Default ordering when no specific order is provided
        }
        // if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
        // {
        //     if (grid.direction.Trim().ToLower() == "asc")
        //     {
        //         switch (grid.order.Trim().ToLower())
        //         {
        //             case "AppName":
        //                 query = query.OrderBy(q => q.AppName).ToList();
        //                 break;
        //         }
        //     }
        //     else
        //     {
        //         switch (grid.order.Trim().ToLower())
        //         {
        //             case "AppName":
        //                 query = query.OrderByDescending(q => q.AppName).ToList();
        //                 break;
        //         }
        //     }
        // }
        // else
        // {
        //     query = query.OrderBy(q => q.Id).ToList();
        // }
        //paginate       
        total = await query.CountAsync();
        if (grid.page.HasValue && grid.limit != 0)
        {
            int start = (grid.page.Value - 1) * grid.limit;
            records = query.Skip(start).Take(grid.limit).ToList();
        }
        else
        {
            records = await query.ToListAsync();
        }
        return this.Json(new { records, total });
    }
    
    [Authorize(Policy = "PERMISSION.APP.EDIT")]
    public IActionResult StartApp(int id){
        _AppService.ToggleAppStatus(id, AppStatus.Active);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "PERMISSION.APP.EDIT")]
    public IActionResult StopApp(int id){
        _AppService.ToggleAppStatus(id, AppStatus.Inactive);
        return RedirectToAction(nameof(Index));
    }
}