using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.TRAINING.DAL.IServices;

namespace PHIASPACE.TRAINING.Controllers
{
    [Authorize]
    public class FacilityController : Controller
    {
        private readonly IFacilityService _faservice;

        public FacilityController(IFacilityService faservice) {
            _faservice = faservice;
        }

        public IActionResult Index()
        {
            var facilities = _faservice.GetFacilities();
            return View(facilities);
        }
    }
}
