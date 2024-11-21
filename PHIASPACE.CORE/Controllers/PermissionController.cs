using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.Models;

namespace PHIASPACE.CORE.Controllers{
    [Authorize(Roles = "Super Admin")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService=permissionService;
        }

        // [Authorize(Roles = "Super Admin")]
        public IActionResult Index(){            
            return View(_permissionService.GetPermissions().Result);
        }

        [HttpGet]
        // [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Add(){
            var clients = await _permissionService.GetOauthClients();
            ViewData["Clients"] = new SelectList(clients,"ClientId","ClientId");
            //ViewBag.Clients = new SelectList((await _permissionService.GetOauthClients()).ToList().Select(m => new SelectListItem { Value = m, Text = m }));
            return View();
        }

        [HttpGet]
        // [Authorize(Roles = "Super Admin")]
        public IActionResult Edit(int id){
            return Ok("This page is still under construction");
        }

        [HttpPost]
        // [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Add(PhiaPermission permission){
            var db_per = await _permissionService.CheckPermisionExist(permission.Permission);
            if(db_per == null){
                var pem = await _permissionService.AddPermission(new PhiaPermission{
                    PermissionModule = permission.Permission, PermissionClient = permission.PermissionClient,
                    Permission = permission.Permission
                });
                if(pem > 0){
                    return RedirectToAction(nameof(Index)); 
                }
                ModelState.AddModelError("", "There was an error adding this permission");
            }
            else{
                ModelState.AddModelError("", "Permission already exist");
            }
                
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var pemission = await _permissionService.GetPermision(id);
            
            if (pemission != null)
            {
                await _permissionService.RemovePhiaPermission(pemission);                
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Role already exist");
            }
            return View();
        }

        public JsonResult GetPermissions(GridTableModel grid)
        {
            List<PhiaPermission> records;
            int total;
            var query = _permissionService.GetPermissions().Result;
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
                        case "Module":
                            query = query.OrderBy(q => q.PermissionModule).ToList();
                            break;
                    }
                }
                else
                {
                    switch (grid.order.Trim().ToLower())
                    {
                        case "Module":
                            query = query.OrderByDescending(q => q.PermissionModule).ToList();
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
}