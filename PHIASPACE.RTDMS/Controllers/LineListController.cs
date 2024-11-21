using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.Service;

namespace PHIASPACE.RTDMS.Controllers;

public class LineListController : RtdmsBaseController
{

    private readonly ZoneService _zoneService;

    // Inject ZoneService via constructor
    public LineListController(ZoneService zoneService)
    {
        _zoneService = zoneService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.IssueTypes = await _zoneService.GetIssueTypes();
        ViewBag.States = await _zoneService.GetCountiesAsync();
        ViewBag.IssueStatuses = await _zoneService.GetIssueStatuses();

        return View();
    }
}
