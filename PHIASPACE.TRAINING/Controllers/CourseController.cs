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
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUtilService _utilService;
        private readonly IThematicAreaService _thematicAreaService;

        public CourseController(ICourseService courseService, IUtilService utilService, IThematicAreaService thematicAreaService)
        {
            _courseService = courseService;
            _utilService=utilService;
            _thematicAreaService=thematicAreaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _courseService.GetCourse(id.Value);
            ViewData["CertificateTypes"] = new SelectList(_utilService.GetOptions("cert_type").Result.ToList(), "LookupId", "LookupName");
            ViewData["ThematicArea"] = new SelectList(_thematicAreaService.GetActiveThematicAreas().ToList(), "Id", "Name");

            if (course == null)
                return NotFound();

            var courseModel = new CourseModel { 
                Id = course.CourseId,
                CourseName = course.CourseName,
                CertificateType = course.CertificateType,
                Description = course.Description,
                DurationDays = course.DurationDays,
                ValidityPeriod = course.ValidityPeriodMonths,
                CmeUnits = course.CmeUnits,
                PassScore = course.PassScore,
                ThematicAreaId = course.ThematicAreaId,
            };

            //ViewData["Certificates"] = new SelectList(_certificateService.GetCertificates(), "Id", "Name");

            return View(courseModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, CourseModel courseToEdit)
        {
            var course = new TblCourse();

            if (id != courseToEdit.Id)
                return NotFound();
            
            ViewData["CertificateTypes"] = new SelectList(_utilService.GetOptions("cert_type").Result.ToList(), "LookupId", "LookupName");
            ViewData["ThematicArea"] = new SelectList(_thematicAreaService.GetActiveThematicAreas().ToList(), "Id", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    course = new TblCourse { 
                        CourseId = courseToEdit.Id,
                        CourseName = courseToEdit.CourseName,
                        Description = courseToEdit.Description,
                        CmeUnits = courseToEdit.CmeUnits,
                        CertificateType = courseToEdit.CertificateType,
                        DurationDays = courseToEdit.DurationDays,
                        PassScore = courseToEdit.PassScore,
                        ThematicAreaId = courseToEdit.ThematicAreaId,
                        ValidityPeriodMonths = courseToEdit.ValidityPeriod,
                    };

                    course.UpdatedBy = User.Identity.Name;
                    course.DateUpdated = DateTime.Now;
                    course.Deleted = 0;

                    _courseService.UpdateCourse(course);

                    TempData["message"] = $"{course.CourseName} training course has been successfully updated";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception /*e*/)
                {
                    ModelState.AddModelError(string.Empty, "Error occured while updating");
                    return View(courseToEdit);
                }
            }

            return View(courseToEdit);
        }

        [HttpGet]
        public IActionResult Create()
        {            
            ViewData["CertificateTypes"] = new SelectList(_utilService.GetOptions("cert_type").Result.ToList(), "LookupId", "LookupName");
            ViewData["ThematicArea"] = new SelectList(_thematicAreaService.GetActiveThematicAreas().ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseModel courseToAdd)
        {
            //var tt = User.Identity.Name;
            var course = new TblCourse();
            ViewData["CertificateTypes"] = new SelectList(_utilService.GetOptions("cert_type").Result.ToList(), "LookupId", "LookupName");
            ViewData["ThematicArea"] = new SelectList(_thematicAreaService.GetActiveThematicAreas().ToList(), "Id", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    course = new TblCourse { 
                        CourseId = courseToAdd.Id,
                        CourseName = courseToAdd.CourseName,
                        Description = courseToAdd.Description,
                        ThematicAreaId = courseToAdd.ThematicAreaId,
                        ValidityPeriodMonths = courseToAdd.ValidityPeriod,
                        CertificateType = courseToAdd.CertificateType,
                        DurationDays = courseToAdd.DurationDays,
                        CmeUnits = courseToAdd.CmeUnits,
                        PassScore = courseToAdd.PassScore,
                    };

                    course.CreatedBy = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;// User.Identity.Name;
                    course.DateCreated = DateTime.Now;
                    course.Deleted = 0;

                    _courseService.AddCourse(course);

                    TempData["message"] = $"{courseToAdd.CourseName} training course has been successfully added";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Error occcured while saving");

                    return View(courseToAdd);
                }
            }

            return View(courseToAdd);
        }
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetCourse(id.Value);

            if (course == null)
            {
                return NotFound();
            }
            else {
                try {
                    _courseService.RemoveCourse(course);
                    return Ok();
                }catch(Exception){
                    return NotFound();
                }
            }

            //return View(course);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetCourse(id);

            if (course == null)
                return NotFound();

            try
            {
                _courseService.RemoveCourse(course);

                TempData["message"] = $"{course.CourseName} course has been successfully deleted";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception /*e*/)
            {
                ModelState.AddModelError(string.Empty, "Error occcured while deleting");

                return View(course);
            }
        }

        public IActionResult Deactivate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetCourse(id.Value);

            if (course == null)
            {
                return NotFound();
            }
            ViewBag.ID = course.CourseId;
            return View(course);
        }
        [HttpPost]
        public IActionResult Deactivate(int Id)
        {
            var course = _courseService.GetCourse(Id);

            if (course == null)
                return NotFound();

            course.Deleted = 1;
            course.UpdatedBy = User.Identity.Name;
            course.DateUpdated = DateTime.Now;

            try
            {
                _courseService.UpdateCourse(course);

                TempData["message"] = $"{course.CourseName} course has been successfully deactivated";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception /*e*/)
            {
                ModelState.AddModelError(string.Empty, "Error occcured while deactivating");

                return View(course);
            }
        }

        public IList<TblCourse> SearchCourses(string term, int cid)
        {
            return _courseService.SearchCourses(term, cid);
        }

        public JsonResult GetActiveCourses(GridTableModel grid)
        {
            List<TblCourse> records;
            int total;
            var query = _courseService.GetActiveCourses().Result;
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
                        case "CourseName":
                            query = query.OrderBy(q => q.CourseName).ToList();
                            break;
                    }
                }
                else
                {
                    switch (grid.order.Trim().ToLower())
                    {
                        case "Name":
                            query = query.OrderByDescending(q => q.CourseName).ToList();
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.CourseId).ToList();
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