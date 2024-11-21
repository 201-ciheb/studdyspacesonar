using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHIASPACE.TRAINING.DAL.IServices;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.Model.ViewModel;
using PHIASPACE.TRAINING.Models;

namespace PHIASPACE.TRAINING.Controllers
{
    [Authorize]
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly IUtilService _utilService;
        private readonly IDistrictService _districtService;
        private readonly IProvinceService _provinceService;

        public VenueController(IVenueService venueService, IUtilService utilService, IDistrictService districtService, IProvinceService provinceService)
        {
            _venueService = venueService;
            _utilService=utilService;
            _districtService=districtService;
            _provinceService=provinceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // var profile = (AspNetUsers)HttpContext.Session.GetObjectFromJson<AspNetUsers>("Profile");

            // var venues = new List<TblTrainingVenue>();
            // if (User.IsInRole("facility_admin") || User.IsInRole("district_admin"))
            // {
            //     venues = _venueService.GetVenues().Where(v => v.Deleted == 0 && v.DistrictId == profile.District)
            //         .ToList();
            // }
            // else if (User.IsInRole("provincial_admin"))
            // {
            //     venues = _venueService.GetVenues().Where(v => v.Deleted == 0 && v.ProvinceId == profile.Province)
            //         .ToList();
            // }
            // else {
            //     venues = _venueService.GetVenues().Where(v => v.Deleted == 0).ToList();
            // }

            return View();
        }
        // public IList<TblTrainingVenue> GetVenuesApi(string term)
        // {
        //     var profile = (AspNetUsers)HttpContext.Session.GetObjectFromJson<AspNetUsers>("Profile");
        //     var venues = new List<TblTrainingVenue>();

        //     if (User.IsInRole("facility_admin") || User.IsInRole("district_admin"))
        //     {
        //         venues = _venueService.GetVenues().Where(v => v.Deleted == 0 && v.DistrictId == profile.District)
        //             .ToList();
        //     }
        //     else if (User.IsInRole("provincial_admin"))
        //     {
        //         venues = _venueService.GetVenues().Where(v => v.Deleted == 0 && v.ProvinceId == profile.Province)
        //             .ToList();
        //     }
        //     else
        //     {
        //         venues = _venueService.GetVenues().Where(v => v.Deleted == 0).ToList();
        //     }

        //     if (term == "" || term == null) {
        //         return venues;
        //     }

        //     return venues.Where(v => v.Deleted == 0 && v.VenueName.Contains(term)).ToList();
        // }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = _venueService.GetVenue(id.Value);
            if (venue == null)
                return NotFound();

            var venueModel = new VenueModel
            {
                Id = venue.VenueId,
                VenueName = venue.VenueName,
                ProvinceId = venue.ProvinceId,
                DistrictId = venue.DistrictId,
                Address = venue.Address,
                Latitude = venue.Latitude,
                Longitude = venue.Longitude
            };

            ViewData["Provinces"] = new SelectList(_provinceService.GetProvinces().Result.ToList(), "ProvinceId", "ProvinceName");
            ViewData["Districts"] = new SelectList(_districtService.GetDistricts().Result.ToList(), "DistrictId", "DistrictName");

            return View(venueModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, VenueModel venueToEdit)
        {
            if (id != venueToEdit.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var venue = new TblTrainingVenue {
                    VenueId = venueToEdit.Id,
                    VenueName = venueToEdit.VenueName,
                    DistrictId = venueToEdit.DistrictId,
                    ProvinceId = venueToEdit.ProvinceId,
                    Latitude = venueToEdit.Latitude,
                    Longitude = venueToEdit.Longitude,
                    Address = venueToEdit.Address
                };
                try
                {
                    //venue.UpdatedBy = User.Identity.Name;
                    venue.UpdatedAt = DateTime.Now;
                    venue.Deleted = 0;

                    _venueService.Update(venue);

                    TempData["message"] = $"{venueToEdit.VenueName} venue has been successfully updated";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception /*e*/)
                {
                    ModelState.AddModelError(string.Empty, "Error occured while updating");
                }
            }

            return View(venueToEdit);
        }

        public IActionResult Create()
        {
            ViewData["Provinces"] = new SelectList(_provinceService.GetProvinces().Result.ToList(), "ProvinceId", "ProvinceName");
            ViewData["Districts"] = new SelectList(_districtService.GetDistricts().Result.ToList(), "DistrictId", "DistrictName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(VenueModel venueToAdd)
        {
            ViewData["Provinces"] = new SelectList(_provinceService.GetProvinces().Result.ToList(), "ProvinceId", "ProvinceName");
            ViewData["Districts"] = new SelectList(_districtService.GetDistricts().Result.ToList(), "DistrictId", "DistrictName");
            if (ModelState.IsValid)
            {
                var venue = new TblTrainingVenue
                {
                    VenueId = venueToAdd.Id,
                    VenueName = venueToAdd.VenueName,
                    DistrictId = venueToAdd.DistrictId,
                    ProvinceId = venueToAdd.ProvinceId,
                    Latitude = venueToAdd.Latitude,
                    Longitude = venueToAdd.Longitude,
                    Address = venueToAdd.Address
                };

                try
                {
                    venue.CreatedBy = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                    venue.CreatedAt = DateTime.Now;
                    venue.Deleted = 0;

                    _venueService.AddVenue(venue);

                    TempData["message"] = $"{venueToAdd.VenueName} venue has been successfully added";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception /*e*/)
                {
                    ModelState.AddModelError(string.Empty, "error occured while saving");

                    return View(venueToAdd);
                }
            }

            return View(venueToAdd);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var venue = _venueService.GetVenue(id.Value);

            if (venue == null)
            {
                return NotFound();
            }

            try
            {
                _venueService.Remove(venue);
                return Ok();
            }
            catch (Exception e) { 
            }
            return Ok();
            //return View(venue);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var venueToDelete = _venueService.GetVenue(id);

            if (venueToDelete == null)
                return NotFound();

            try
            {
                _venueService.Remove(venueToDelete);

                TempData["message"] = $"{venueToDelete.VenueName} venue has been successfully deleted";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception /*e*/)
            {
                ModelState.AddModelError(string.Empty, "error occured while deleting");

                return View(venueToDelete);
            }
        }

        public IActionResult Deactivate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = _venueService.GetVenue(id.Value);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost]
        public IActionResult Deactivate(int id)
        {
            var venueToDeactivate = _venueService.GetVenue(id);

            if (venueToDeactivate == null)
                return NotFound();

            try
            {
                venueToDeactivate.Deleted = 1;
                //venueToDeactivate.UpdatedBy = User.Identity.Name;
                venueToDeactivate.UpdatedAt = DateTime.Now;

                _venueService.Update(venueToDeactivate);

                TempData["message"] = $"{venueToDeactivate.VenueName} venue has been successfully deactivated";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception /*e*/)
            {
                ModelState.AddModelError(string.Empty, "error occured while deactivating");

                return View(venueToDeactivate);
            }
        }

        public JsonResult GetActiveVenues(GridTableModel grid)
        {
            List<TblTrainingVenue> records;
            int total;
            var query = _venueService.GetActiveVenues().Result;     
            if (!string.IsNullOrEmpty(grid.order) && !string.IsNullOrEmpty(grid.direction))
            {
                if (grid.direction.Trim().ToLower() == "asc")
                {
                    switch (grid.order.Trim().ToLower())
                    {
                        case "VenueName":
                            query = query.OrderBy(q => q.VenueName).ToList();
                            break;
                    }
                }
                else
                {
                    switch (grid.order.Trim().ToLower())
                    {
                        case "VenueName":
                            query = query.OrderByDescending(q => q.VenueName).ToList();
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.VenueId).ToList();
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