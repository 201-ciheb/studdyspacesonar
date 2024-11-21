using System.Linq.Expressions;
using IdentityServer4.Events;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.Models;

namespace PHIASPACE.CORE.Controllers{
    [Authorize]
    public class UserController:Controller{
        private readonly UserManager<PhiaSpaceUser> _userManager;
        private readonly IProjectService _projectService;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionService _permissionService;

        public UserController(UserManager<PhiaSpaceUser> userManager, IProjectService projectService, RoleManager<IdentityRole> roleManager,
        IPermissionService permissionService){
            _userManager=userManager;
            _projectService=projectService;
            _roleManager=roleManager;
            _permissionService=permissionService;
        }

        [Authorize(Roles = "Super Admin")] 
        public IActionResult Index(){
            return View();
        }
    
        [Authorize(Policy = "PERMISSION.USER.VIEW")]
        public async Task<IActionResult> Details(string id){
            var user_details = new UserDetails();
            user_details.PhiaSpaceUser = await _userManager.FindByIdAsync(id);
            user_details.Roles = await _userManager.GetRolesAsync(user_details.PhiaSpaceUser);
            user_details.PhiaUserProject = await _projectService.GetUserProjects(id);
            user_details.Permission = _permissionService.GetUserPermissions(id).Result.Select(m => new PermissionModel{Permission=m}).ToList();

            var permissions = _permissionService.GetPermissions().Result.Select(m => new PermissionModel{Permission=m.Permission, Client = m.PermissionClient});
            

            ViewData["ActiveProjects"] = new SelectList(_projectService.GetActiveProjects().Result.ToList(), "Id", "ProjectName");
            ViewData["Roles"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            ViewBag.Permissions = permissions;
            return View(user_details);
        }

        public JsonResult GetUsers(GridTableModel grid)
        {
            List<PhiaSpaceUser> records;
            int total;
            var query = _userManager.Users.Where(m => m.Deleted == 0);
            if(!string.IsNullOrWhiteSpace(grid.search)){
                query = query.Where(q => q.UserName.Contains(grid.search) || q.FullName.Contains(grid.search) || q.Email.Contains(grid.search));
            }      
            if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
            {
                var propertyName = grid.order.Trim();
                var parameter = Expression.Parameter(typeof(PhiaSpaceUser)); 
                var property = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda<Func<PhiaSpaceUser, object>>(Expression.Convert(property, typeof(object)), parameter);

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
            total = query.Count();
            int start = (grid.page != null ? grid.page.Value - 1 : 0) * grid.limit;
            records = query.Skip(grid.offset).Take(grid.limit).ToList();
            return this.Json(new { records, total });
        }
    
        [Authorize(Policy = "PERMISSION.USER.EDIT")]
        public IActionResult AddUserProject(PhiaUserProject phiaUserProject){
            _projectService.AddUserProject(phiaUserProject);
            return RedirectToAction("Details", new {id=phiaUserProject.UserId});
        }

        [Authorize(Policy = "PERMISSION.USER.EDIT")]
        public IActionResult AddUserRole(string RoleId, string UserId){
            var user = _userManager.FindByIdAsync(UserId).Result;
            _userManager.AddToRoleAsync(user, RoleId).Wait();
                        
            return RedirectToAction("Details", new {id=UserId});
        }

        [Authorize(Policy = "PERMISSION.USER.EDIT")]
        public IActionResult AddUserPermission(List<string> Permission, string UserId){
            _permissionService.AddUserPermission(Permission, UserId);            
            return RedirectToAction("Details", new {id=UserId});
        }
    }
}