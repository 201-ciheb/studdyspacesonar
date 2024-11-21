using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;
using PHIASPACE.RTDMS.Helpers;
using PHIASPACE.RTDMS.Models;

namespace PHIASPACE.RTDMS.Controllers
{
    public class AIssueController : RtdmsBaseController
    {
        private readonly IAIssueService _iAIssueService;
        private readonly IZoneService _zoneService;
        private readonly UserHelper _userHelper;

        public AIssueController(IAIssueService iAIssueService, UserHelper userHelper, IZoneService zoneService)
        {
            _iAIssueService = iAIssueService;
            _userHelper = userHelper;
            _zoneService = zoneService;
        }

        // GET: AIssue
        public async Task<IActionResult> Manage(DateTime? start_date, DateTime? end_date, int? page, int? category_id, int? search_status_id, string search = null, int? pageSize = null)
        {
            try
            {
                int currentPageSize = pageSize ?? 10;
                int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = emailClaim?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return View("Error", new ErrorViewModel { Message = "Unable to retrieve user email." });
                }

                var person = _userHelper.GetPersonDetails(email);
                if (person == null)
                {
                    return View("Error", new ErrorViewModel { Message = "Person details not found." });
                }

                var priorities = _zoneService.GetLookupOptions("priority")?.ToList() ?? new List<AimsLookup>();
                var labs = await _zoneService.GetLabsAsync();
                var issueTypes = await _zoneService.GetIssueTypes();
                var counties = await _zoneService.GetCountiesAsync();

                ViewBag.States = counties != null && counties.Any()
                    ? new SelectList(counties, "Id", "CountyName")
                    : new SelectList(Enumerable.Empty<SelectListItem>());

                var statuses = await _zoneService.GetIssueStatuses();
                ViewBag.Statuses = new SelectList(statuses, "Id", "Name");
                ViewBag.Priorities = new SelectList(priorities, "Id", "LookupName");
                ViewBag.Labs = new SelectList(labs, "Id", "LabName");
                ViewBag.IssueTypes = new SelectList(issueTypes, "Id", "Name");
                ViewBag.pageSize = currentPageSize;

                // Fetch issues with pagination and await the result
                var issues = await _iAIssueService.GetFilteredIssues(search, start_date, end_date, category_id, search_status_id, pageIndex, currentPageSize);

                return View(issues);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View("Error", new ErrorViewModel { Message = "An error occurred while fetching issues." });
            }
        }


        [HttpPost]
        public ActionResult Post(AimsIssue issue)
        {
            var collection = Request.Form;

            issue.OpenedBy = User.Identity.Name;

            if (Convert.ToInt32(collection["is_new"]) == 0)
            {
                _iAIssueService.UpdateIssue(issue);
            }
            else
            {
                _iAIssueService.AddIssue(issue);
            }

            var persons = collection["persons"].ToString();
            var lnks = new List<AimsLnkIssuePerson>();

            if (!string.IsNullOrEmpty(persons))
            {
                foreach (var ps in persons.Split(','))
                {
                    var lnk_person = new AimsLnkIssuePerson
                    {
                        IssueId = issue.Id,
                        PersonId = Convert.ToInt32(ps)
                    };

                    lnks.Add(lnk_person);
                }
            }

            var refererUrl = Request.Headers["Referer"].ToString();
            return Redirect(refererUrl);
        }

        [HttpPost]
        public ActionResult PostDiscussion(AimsDiscussion discussion)
        {
            discussion.AddedBy = User.Identity.Name;
            _iAIssueService.AddDiscussion(discussion);

            var refererUrl = Request.Headers["Referer"].ToString();
            return Redirect(refererUrl);
        }
    }
}