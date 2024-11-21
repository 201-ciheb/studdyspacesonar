using Microsoft.AspNetCore.Mvc;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.AspNetCore.Authorization;
using PHIASPACE.TRAINING.DAL.Model.ViewModel;
using PHIASPACE.TRAINING.Models;

namespace PHIASPACE.TRAINING.Controllers{
    [Authorize]
    public class ThematicAreaController:Controller{
        private readonly IThematicAreaService _thematicAreaService;

        public ThematicAreaController(IThematicAreaService thematicAreaService) {
            _thematicAreaService = thematicAreaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(ThematicAreaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var thema_area = new TblThematicArea{
                        CreatedBy = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value,
                        Name=model.Name,
                        DateCreated=DateTime.Now,
                        Deleted=0
                    };
                    _thematicAreaService.AddThematicArea(thema_area);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Error occcured while saving");

                    return View(model);
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int Id)
        {
            if(_thematicAreaService.DeleteThematicArea(Id)){
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var thema_area = _thematicAreaService.GetThematicArea(id);
            var model = new ThematicAreaModel{
                Name=thema_area.Name,
                Id=thema_area.Id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ThematicAreaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var thema_area = new TblThematicArea{
                        UpdatedBy = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value,
                        Name=model.Name,
                        DateUpdated=DateTime.Now,
                        Deleted=0,
                        Id = model.Id
                    };
                    _thematicAreaService.UpdateThematicArea(thema_area);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Error occcured while saving");

                    return View(model);
                }
            }
            return View(model);
        }

        public JsonResult GetActiveThematicAreas(GridTableModel grid)
        {
            List<TblThematicArea> records;
            int total;
            var query = _thematicAreaService.GetActiveThematicAreas();
            if (!string.IsNullOrWhiteSpace(grid.search))
            {
                query = query.Where(q => q.Name.Contains(grid.search)).ToList();
            }        
            if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
            {
                if (grid.direction.Trim().ToLower() == "asc")
                {
                    switch (grid.order.Trim().ToLower())
                    {
                        case "Name":
                            query = query.OrderBy(q => q.Name).ToList();
                            break;
                        case "DateCreated":
                            query = query.OrderBy(q => q.DateCreated).ToList();
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
                        case "DateCreated":
                            query = query.OrderByDescending(q => q.DateCreated).ToList();
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