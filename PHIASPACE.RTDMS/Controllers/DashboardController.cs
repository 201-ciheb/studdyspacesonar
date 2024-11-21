using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using PHIASPACE.RTDMS.Utility;
using PHIASPACE.RTDMS.DAL.IService;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.Models;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using PHIASPACE.RTDMS.DAL.Model;

namespace PHIASPACE.RTDMS.Controllers;

public class DashboardController: RtdmsBaseController{
    private readonly IDashboardService _dashboardService;
    private readonly IRecordService _recordService;

    public DashboardController(IDashboardService dashboardService, IRecordService recordService)
    {
        _dashboardService = dashboardService;
        _recordService = recordService;
    }

    public async Task<IActionResult> Index(){
        return View();
    }

    public async Task<IActionResult> Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> MonitorEA(){
        var dt = await _dashboardService.GetEAMonitorAsync();
        ViewBag.table = dt;
        return View();
    }

    public async Task<IActionResult> TeamSummary()
    {
        var dt = await _dashboardService.GetTeamSummaryDataSetAsync();
        ViewBag.ds = dt;
        return View();
    }

    public async Task<ActionResult> ResponseRate()
{
    var dt = await _dashboardService.GetHhResponseRate();

    // Add columns to DataTable with simplified and consistent names
    AddResponseRateColumns(dt);

    foreach (DataRow row in dt.Rows)
    {
        // Helper function for safe conversion
        double SafeConvertToDouble(object value) => value == DBNull.Value ? 0 : Convert.ToDouble(value);

        // Helper to calculate percentage safely
        double CalculatePercentage(double numerator, double denominator) => 
            Math.Abs(denominator) < double.Epsilon ? 0 : (numerator * 100) / denominator;

        // Populate calculated columns
        PopulateHabitableColumns(row, SafeConvertToDouble);
        PopulateResponseRates(row, SafeConvertToDouble, CalculatePercentage);
    }

    ViewBag.dt = dt;
    return View();
}

private static void AddResponseRateColumns(DataTable dt)
{
    var columns = new[]
    {
        "Habitable", "NotHabitable", "RefusalRate", "AcceptanceRate", "PHIAResponseRate",
        "SampleMethodResponseRate", "PartialOrWithdrawn", "IncompleteBloodDraw",
        "InterviewResponseRate", "DrawResponseRate", "TotalAdultResponseRate"
    };

    foreach (var column in columns)
    {
        dt.Columns.Add(column, typeof(double));
    }
}

private static void PopulateHabitableColumns(DataRow row, Func<object, double> safeConvert)
{
    row["Habitable"] = safeConvert(row["Households_Consented"]) +
                       safeConvert(row["Households_Refused"]) +
                       safeConvert(row["Households_Not_At_Home"]) +
                       safeConvert(row["Households_Absent_For_Long"]) +
                       safeConvert(row["Households_Postponed"]) +
                       safeConvert(row["Households_Not_Found"]) +
                       safeConvert(row["Households_Others"]);

    row["NotHabitable"] = safeConvert(row["Households_Destroyed"]) +
                          safeConvert(row["Households_Vacant"]);
}

private static void PopulateResponseRates(DataRow row, Func<object, double> safeConvert, Func<double, double, double> calculatePercentage)
{
    double householdsVisited = safeConvert(row["Households_Visited"]);
    double eligible15Above = safeConvert(row["Eligible_15_Above"]);
    double completedIntv15Above = safeConvert(row["Completed_Intv_15_Above"]);

    row["RefusalRate"] = calculatePercentage(safeConvert(row["Households_Refused"]), householdsVisited);
    row["AcceptanceRate"] = calculatePercentage(safeConvert(row["Households_Consented"]) + safeConvert(row["Households_Refused"]), householdsVisited);
    row["PHIAResponseRate"] = calculatePercentage(safeConvert(row["Habitable"]), householdsVisited);
    row["SampleMethodResponseRate"] = calculatePercentage(safeConvert(row["Households_Consented"]), householdsVisited);

    row["PartialOrWithdrawn"] = safeConvert(row["Consented_15_Above"]) - completedIntv15Above;
    row["IncompleteBloodDraw"] = safeConvert(row["Consented_For_Draw_15_Above"]) - safeConvert(row["Draws_15_Above"]);

    row["InterviewResponseRate"] = calculatePercentage(completedIntv15Above, eligible15Above);
    row["DrawResponseRate"] = calculatePercentage(safeConvert(row["Draws_15_Above"]), completedIntv15Above);

    row["TotalAdultResponseRate"] =
        (eligible15Above <= 0 || completedIntv15Above <= 0) ? 0 :
        calculatePercentage(completedIntv15Above, eligible15Above) *
        calculatePercentage(safeConvert(row["Draws_15_Above"]), completedIntv15Above) / 100;
}

    public async Task<ActionResult> Linkage()
    {
        var dt = await _dashboardService.GetLinkageDashboardAsync();
        ViewBag.table = dt;
        return View();
    }

    public ActionResult StateTrend()
    {
        return View();
    }
    
    public async Task<IActionResult> Summary()
    {
        var dt = await _dashboardService.GetHhEaSummary();
        ViewBag.ds = dt;
        return View();
    }

    public async Task<List<Trend>> GetStateTrend()
    {
        var ds = await _dashboardService.GetRegionTrend();
        var state = "State";

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return new List<Trend>();

        // Use helper method to create the epoch DateTime
        var epoch = CreateUtcDateTime(1970, 1, 1);

        var states = ds.Tables[0].DefaultView.ToTable(true, state).AsEnumerable();

        var result = states
            .Select(st => new Trend
            {
                name = st.Field<string>(state),
                data = ds.Tables[0]
                    .AsEnumerable()
                    .Where(row => row.Field<string>("State") == st.Field<string>(state))
                    .Select(row => new[]
                    {
                        row.Field<DateTime>(1).ToUniversalTime().Subtract(epoch).TotalMilliseconds, // Ensure DateTimeKind is handled
                        Convert.ToDouble(row[0])
                    })
                    .ToList()
            })
            .ToList();

        return result;
    }

// Helper method to create a DateTime with specified DateTimeKind
    private static DateTime CreateUtcDateTime(int year, int month, int day)
    {
        return DateTime.SpecifyKind(new DateTime(year, month, day), DateTimeKind.Utc);
    }

    public async Task<IActionResult> IndAnalytic([FromQuery] int id = 1)
    {
        if (!ModelState.IsValid)
        {
            // Handle invalid model state
            return BadRequest(ModelState);
        }

        // Fetch the record set
        var recordSet = await _recordService.GetAimsRecordSetAsync();
        ViewBag.recordSet = recordSet;

        // Validate that the ID exists in the record set
        var defaultRecord = recordSet.Find(m => m.Id == id);
        if (defaultRecord == null)
        {
            // Handle case where no matching record is found
            return NotFound($"Record with ID {id} not found.");
        }

        // Fetch value sets and filter by table name
        var valueSets = await _recordService.GetAimsValueSetAsync();
        ViewBag.values = valueSets.Where(m => m.TableName == defaultRecord.Name.ToLower()).ToList();

        return View();
    }

    public async Task<IActionResult> GetIndAnalyticDynamic(int id)
    {
        if (id <= 0)
        {
            // Add a validation error to ModelState
            ModelState.AddModelError("id", "The ID must be a positive integer.");

            // Return a BadRequest with validation details
            return BadRequest(ModelState);
        }

        // If validation passes, fetch the data
        var result = await _dashboardService.GetIndAnalyticDynamic(id);
        return Ok(result); // Return the result in a proper HTTP response
    }


    public async Task<List<Step>> GetValueSets(string id)
    {
        var value_sets = _recordService.GetAimsValueSetAsync().Result
            .Where(m => m.TableName == id.ToLower()).ToList();
        var dupicate_value_sets = value_sets.Select(
            m => new Step{
                Id = (int)m.RecordId,
                Name = m.Label,
            }
        );
        return dupicate_value_sets.DistinctBy(m => m.Id).ToList();
    }
}
