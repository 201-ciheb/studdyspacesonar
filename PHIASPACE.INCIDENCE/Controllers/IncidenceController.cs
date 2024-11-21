using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHIASPACE.INCIDENCE.DAL.IService;
using PHIASPACE.INCIDENCE.Models;

namespace PHIASPACE.INCIDENCE.Controllers
{
    public class IncidenceController : Controller
    {
        private readonly IZoneService _zoneService;

        public IncidenceController(IZoneService zoneService) {
            _zoneService =  zoneService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateAsync()
        {
            var counties  = await _zoneService.GetCountiesAsync();
            var model = new IncidentModel();
            model.Counties = counties.Select(c => new SelectListItem
            {
                Value = c.CountyCode,
                Text = c.CountyName
            }).ToList();
         
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(IncidentModel model)
        {
            var counties = await _zoneService.GetCountiesAsync();
            model.Counties = counties.Select(c => new SelectListItem
            {
                Value = c.CountyCode,
                Text = c.CountyName
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public JsonResult GetIncidents()
        {
            var incidents = new List<IncidentModel>();

            return Json(new { data = incidents });
        }
    }
}
