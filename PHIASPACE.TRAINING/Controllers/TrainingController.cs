using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.Model.ViewModel;
using PHIASPACE.TRAINING.DAL.IServices;
using PHIASPACE.TRAINING.Utility;
using Microsoft.AspNetCore.Authorization;

namespace PHIASPACE.TRAINING.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trservice;
        private readonly ICourseService _coservice;
        private readonly IVenueService _veservice;
        private readonly IPersonService _psservice;
        private readonly IUtilService _utilService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public TrainingController(ITrainingService trservice, ICourseService coservice, IVenueService veservice, IPersonService psservice, IUtilService utilService)
        {
            _trservice = trservice;
            _coservice = coservice;
            _veservice = veservice;
            _psservice = psservice;
            _utilService = utilService;
        }

        [Authorize(Policy = "PERMISSION.APP.VIEW")]
        public IActionResult Index()
        {
            var trainings = _trservice.GetAllTraining();
            return View(trainings);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var c_training = (TrainingModel)HttpContext.Session.GetObjectFromJson<TrainingModel>("c_training");
            var courses = new SelectList(_coservice.GetCourses().Result.ToList(), "CourseId", "CourseName", null);
            var venues = new SelectList(_veservice.GetVenues(), "VenueId", "VenueName", null); 
            ViewBag.FundingType = new SelectList(_utilService.GetOptions("funding_type").Result.ToList(), "LookupName", "LookupName");

            ViewBag.courses = courses;
            ViewBag.venues = venues;

            if (c_training != null)
            {
                return View(c_training);
            }
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(TrainingModel training)
        {
            if (!ModelState.IsValid)
            {
                var courses = new SelectList(_coservice.GetCourses().Result.ToList(), "CourseId", "CourseName", null);
                var venues = new SelectList(_veservice.GetVenues(), "VenueId", "VenueName", null);
                ViewBag.FundingType = new SelectList(_utilService.GetOptions("funding_type").Result.ToList(), "LookupName", "LookupName");

                ViewBag.courses = courses;
                ViewBag.venues = venues;
                return View(training);
            }

            var c_training = (TrainingModel)HttpContext.Session.GetObjectFromJson<TrainingModel>("c_training");

            if (c_training != null)
            {
                HttpContext.Session.Remove("c_training");
            }

          /*  var _training = new TblTraining
            {
                CourseId = training.CourseId,
                VenueId = training.VenueId,
                StartDate = training.StartDate,
                EndDate = training.EndDate
            };*/

            HttpContext.Session.SetObjectAsJson("c_training", training);

            // var newtraining = _trservice.AddTraining(_training);
            var c_participants = (List<ParticipantModel>)HttpContext.Session.GetObjectFromJson<List<ParticipantModel>>("c_participants");
            if (c_participants == null) {
                c_participants = new List<ParticipantModel>();
            }
            ViewBag.Participants = c_participants;
            return View("AddParticipant", new ParticipantModel());
        }

        [HttpGet]
        public IActionResult AddParticipant()
        {
            var c_participants = (List<ParticipantModel>)HttpContext.Session.GetObjectFromJson<List<ParticipantModel>>("c_participants");
            if (c_participants == null)
            {
                c_participants = new List<ParticipantModel>();
            }
            ViewBag.Participants = c_participants;

            return View();
        }

        [HttpPost]
        public IActionResult AddParticipant([FromBody] ParticipantModel participantModel)
        {
            var person = _psservice.GetPerson(participantModel.IdentificationCode);
            return Ok(person);
        }
        [HttpGet]
        public IActionResult AddFacilitator()
        {
            var c_facilitators = (List<FacilitatorModel>)HttpContext.Session.GetObjectFromJson<List<FacilitatorModel>>("c_facilitators");
            if (c_facilitators == null)
            {
                c_facilitators = new List<FacilitatorModel>();
            }
            ViewBag.Facilitators = c_facilitators;
            return View();
        }

        [HttpPost]
        public IActionResult AddFacilitor(FacilitatorModel facilitatorModel)
        {
            return View();
        }
    }
}
