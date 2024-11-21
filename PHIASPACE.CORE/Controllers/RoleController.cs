using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.Models;

namespace PHIASPACE.CORE.Controllers{

    [Authorize(Roles = "Super Admin")]
    public class RoleController :Controller{
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // [Authorize(Roles = "Super Admin")]
        public IActionResult Index(){           
            return View();
        }

        [HttpGet]
        // [Authorize(Roles = "Super Admin")]
        public IActionResult Add(){
            return View();
        }

        [HttpGet]
        // [Authorize(Roles = "Super Admin")]
        public IActionResult Edit(int id){
            return Ok("This page is still under construction");
        }

        [HttpPost]
        // [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Add(Role role){
            var roleExists = await _roleManager.RoleExistsAsync(role.Name);
            if (!roleExists)
            {
                var id_role = new IdentityRole(role.Name);

                var result = await _roleManager.CreateAsync(id_role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Role already exist");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            
            var role = await _roleManager.FindByIdAsync(id);
            
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);                
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Role already exist");
            }
            return View();
        }

        public JsonResult GetRoles(GridTableModel grid)
        {
            List<IdentityRole> records;
            int total;
            var query = _roleManager.Roles.ToList();
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
                            query = query.OrderBy(q => q.Name).ToList();
                            break;
                    }
                }
                else
                {
                    switch (grid.order.Trim().ToLower())
                    {
                        case "Name":
                            query = query.OrderByDescending(q => q.Name).ToList();
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