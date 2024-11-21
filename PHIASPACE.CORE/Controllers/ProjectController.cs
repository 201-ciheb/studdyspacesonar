
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.Models;
using PHIASPACE.CORE.Utility;

namespace PHIASPACE.CORE.Controllers;

[Authorize]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService=projectService;
    }

    [Authorize(Policy = "PERMISSION.PROJECT.VIEW")]
    public IActionResult Index(){
        return View(_projectService.GetActiveProjects().Result);
    }

    [HttpGet]
    [Authorize(Policy = "PERMISSION.PROJECT.CREATE")]
    public IActionResult Add(){
        return View();
    }

    [HttpGet]
    [Authorize(Policy = "PERMISSION.PROJECT.EDIT")]
    public IActionResult Edit(int id){
        var project = _projectService.GetProject(id).Result;
        if(project == null) return NotFound();
        return View(project);
    }

    [HttpPost]
    [Authorize(Policy = "PERMISSION.PROJECT.CREATE")]
    public IActionResult Add(ProjectModel project, IFormFile Logo){
        //save logo

        _projectService.AddPhiaProject(new PhiaProject{
            Active = project.Active,
            ProjectAbbrev=project.Abbreviation,
            ProjectDescription=project.Description,
            ProjectName=project.Name,
            ProjectStartDate=DateTime.Parse(project.StartDate)
        });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = "PERMISSION.PROJECT.EDIT")]
    public IActionResult Edit(PhiaProject project){
        //save logo

        _projectService.EditPhiaProject(project);
        return RedirectToAction(nameof(Index));
    }


    public JsonResult GetActiveProjects(GridTableModel grid)
    {
        List<PhiaProject> records;
        int total;
        var query = _projectService.GetActiveProjects().Result;
        // if (!string.IsNullOrWhiteSpace(grid.name))
        // {
        //     query = query.Where(q => q.ProjectName.Contains(name)).ToList();
        // }        
        if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
        {
            if (grid.direction.Trim().ToLower() == "asc")
            {
                switch (grid.order.Trim().ToLower())
                {
                    case "projectName":
                        query = query.OrderBy(q => q.ProjectName).ToList();
                        break;
                }
            }
            else
            {
                switch (grid.order.Trim().ToLower())
                {
                    case "projectName":
                        query = query.OrderByDescending(q => q.ProjectName).ToList();
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
}