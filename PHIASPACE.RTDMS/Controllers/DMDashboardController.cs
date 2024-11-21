using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.Helpers;
using PHIASPACE.RTDMS.DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using PHIASPACE.RTDMS.DAL.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Drawing.Printing;
using PHIASPACE.RTDMS.Utility;

namespace PHIASPACE.RTDMS.Controllers
{
    public class DMDashboardController : RtdmsBaseController
    {
        private readonly IHouseholdService _householdService;
        private readonly ITeamService _teamService;
        private readonly ILogger<DMDashboardController> _logger;
        private readonly TreeHelper _treeHelper;
        private readonly IZoneService _zoneService;
        private readonly IFormDataService _formDataService;
        private readonly IMonitoringService _monitoringService;
        private readonly IUtilService _utilService;

        public DMDashboardController(
            IHouseholdService householdService,
            ITeamService teamService,
            ILogger<DMDashboardController> logger,
            TreeHelper treeHelper,
            IFormDataService formDataService,
            IZoneService zoneService,
            IMonitoringService monitoringService,
            IUtilService utilService)
        {
            _householdService = householdService;
            _teamService = teamService;
            _logger = logger;
            _treeHelper = treeHelper;
            _formDataService = formDataService;
            _zoneService = zoneService;
            _monitoringService = monitoringService;
            _utilService = utilService;
        }

        public async Task<IActionResult> Index(int? hh_id, int? cluster_no)
        {
            try
            {
                // Fetch Issue Types, States, and Issue Statuses
                ViewBag.IssueTypes = new SelectList(await _zoneService.GetIssueTypes(), "Id", "Name");
                ViewBag.States = new SelectList(await _zoneService.GetCountiesAsync(), "Id", "Name");
                ViewBag.IssueStatuses = new SelectList(await _zoneService.GetIssueStatuses(), "Id", "Name");

                // Fetch priorities from the database
                var priorities = await _zoneService.GetLookupOptions("priority").ToListAsync();
                ViewBag.PriorityOptions = priorities != null && priorities.Any()
                    ? new SelectList(priorities, "Id", "LookupName")
                    : null;

                // Fetch form links
                var formLinks = "";//_zoneService.GetFormLink();
                ViewBag.FormLinks = formLinks;

                // Generate the tree data
                ViewBag.tree_data = _treeHelper.GetTree1Async();

                // Fetch household data if hh_id and cluster_no are provided
                if (hh_id.HasValue && cluster_no.HasValue)
                {
                    var householdData = await _householdService.GetHouseholdDetailsAsync(cluster_no.Value, hh_id.Value);
                    ViewBag.hh_data = householdData;

                    // Fetch the team lead
                    // ViewBag.person = _teamService.GetTeamLeadEa(cluster_no.Value);
                    ViewBag.person = null;// _teamService.GetTeamLeadEa(cluster_no.Value);
                }
                else
                {
                    // Fetch all household data
                    var allHouseholdData = await _householdService.GetAllHouseholdDataAsync();
                    ViewBag.hh_data = allHouseholdData;
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Index");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        public async Task<IActionResult> HHData(int? hh_id, int? ea_id)
        {
            ViewBag.ea_id = ea_id;
            ViewBag.hh_id = hh_id;

            // Fetch issue types and statuses
            ViewBag.IssueTypes = new SelectList(await _zoneService.GetIssueTypes(), "Id", "Name");
            ViewBag.IssueStatuses = new SelectList(await _zoneService.GetIssueStatuses(), "Id", "Name");

            return View();
        }


        public async Task<IActionResult> Linelisting(int? page, int? pageSize, string location_id = null)
        {
            try
            {
                int size = pageSize ?? 10;
                var page_id = page ?? 1;

                // Fetch data as lists for ViewBag items
                var statuses = await _zoneService.GetIssueStatuses();
                ViewBag.Statuses = new SelectList(statuses, "Id", "Name");

                var counties = await _zoneService.GetCountiesAsync();
                ViewBag.States = counties != null && counties.Any()
                    ? new SelectList(counties, "Id", "CountyName")
                    : new SelectList(Enumerable.Empty<SelectListItem>());

                var issueTypes = await _zoneService.GetIssueTypes();
                ViewBag.IssueTypes = new SelectList(issueTypes, "Id", "Name");

                var labs = await _zoneService.GetLabsAsync();
                ViewBag.Labs = new SelectList(labs, "Id", "LabName");

                var priorities = _zoneService.GetLookupOptions("priority").ToList();
                ViewBag.PriorityOptions = new SelectList(priorities, "Id", "LookupName");

                // Initialize level and locationId as nullable integers
                int? level = null;
                int? locationIdParam = null;

                // Parse level and location ID if provided in location_id
                if (!string.IsNullOrEmpty(location_id))
                {
                    var values = location_id.Split('_');
                    if (values.Length > 0 && int.TryParse(values[0], out int parsedLevel))
                    {
                        level = parsedLevel;
                    }
                    if (values.Length > 1 && int.TryParse(values[1], out int parsedLocationId))
                    {
                        locationIdParam = parsedLocationId;
                    }
                }

                // Fetch household data with nullable level and locationId parameters
                var householdData = await _householdService.GetHouseholdDataAsync(page_id, size, level, locationIdParam);
                var totalRecords = householdData.Tables[0].Rows.Count;
                int totalPages = (int)Math.Ceiling((double)totalRecords / size);

                // Paginate household data
                var paginatedData = householdData.Tables[0].AsEnumerable()
                    .Skip((page_id - 1) * size)
                    .Take(size)
                    .CopyToDataTable();

                // Assign data to ViewBag for use in the view
                ViewBag.hh_data = paginatedData;
                ViewBag.page = page_id;
                ViewBag.pages = totalPages;
                ViewBag.pageSize = size;
                ViewBag.location_id = location_id;

                ViewBag.tree_data = await _treeHelper.GetTreeAsync(location_id);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing Linelisting");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        public async Task<IActionResult> GetEaSummary(int? ea_id)
        {
            if (ea_id.HasValue)
            {
                ViewBag.data_table = await _teamService.GetEaConfirmationSummaryAsync(ea_id.Value);
            }

            return View();
        }

        public IActionResult TeamMonitor()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MonitorProcess(int? page, int? team_id, string team_state = "")
        {
            try
            {
                //this method is basically using the logged in user to get the zon and the teams in that zone
                //the disagregation of teams by zone here is for the purpose of the data monitors assignment to zone
                var zone = _zoneService.GetUserZone(User.Identity.Name);
                // var tms = _teamService.GetTeams().Where(e => e.Status == 1);
                var tms = await _monitoringService.GetTeamEaDetailsAsync(0, team_state);

                if (zone != 0)
                {
                    var zone_code = _zoneService.GetZones().FirstOrDefault(e => e.Id == zone)?.Code;
                    var states = await _zoneService.GetCountiesAsync();
                    var state_ids = states.Where(e => e.GeoPolicticalRegion == zone_code).Select(e => e.Id).ToList();
                    // tms = tms.Where(e => e.AimsLnkTeamStates.Any(x => state_ids.Contains(x.StateId)));
                }

                // int pageSize = 30;
                // int pageIndex = page.HasValue ? page.Value : 1;

                // if (team_id.HasValue)
                // {
                //     var active_team = TeamUtil.GetActiveEa(team_id.Value);
                //     ViewBag.tb = _teamService.GetTeamEaStatAsync(active_team.Id);
                // }

                // var teams = tms.OrderBy(e => e.Id).ToList().ToPagedList(pageIndex, pageSize);
                var teams = tms.OrderBy(e => e.Team).ToList();

                if (string.IsNullOrEmpty(team_state))
                {
                    // var team_ids = tms
                    //     .Where(e => e.AimsLnkTeamStates.Any(x => x.StateId == team_state.Value))
                    //     .Select(e => e.Id)
                    //     .ToArray();

                    // return View(tms.Where(e => team_ids.Contains(e.Id)).OrderBy(e => e.Id).ToList().ToPagedList(pageIndex, pageSize));
                }

                ViewBag.hhutil = new HHUtil(_utilService);

                return View(teams);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "Null reference encountered in MonitorProcess");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in MonitorProcess");
                return View();
            }
        }

        public async Task<ActionResult> PData(int? form_id, int? cluster_no, int? hh, int? line)
        {
            // Call the service to get the form data
            var ds = await _formDataService.GetFormDataAsync(form_id, cluster_no, hh, line);

            // Assign the DataSet to ViewBag
            ViewBag.hh_data = ds;
            ViewBag.form_id = form_id;

            // Return the view
            return View();
        }

        public ActionResult GetReport(int ea_id)
        {
    
            return View();
        }
    }
}
