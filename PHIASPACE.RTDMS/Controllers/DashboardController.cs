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
        dt.Columns.Add("Habitable", typeof(Double));
        dt.Columns.Add("NotHabitable", typeof(Double));
        dt.Columns.Add("RefusalRate", typeof(Double));
        dt.Columns.Add("AcceptanceRate", typeof(Double));
        dt.Columns.Add("PHIAResponseRate", typeof(Double));
        dt.Columns.Add("SampleMethodResponseRate", typeof(Double));
        dt.Columns.Add("PartialOrWithdrawn", typeof(Double));
        dt.Columns.Add("IncompleteBloodDraw", typeof(Double));
        dt.Columns.Add("InterviewResponseRate", typeof(Double));  // Target 87.9%
        dt.Columns.Add("DrawResponseRate", typeof(Double));  // Target 77.3%
        dt.Columns.Add("TotalAdultResponseRate", typeof(Double));  // Target 63%

        foreach (DataRow row in dt.Rows)
        {
            // Helper function for safe conversion
            double SafeConvertToDouble(object value) => value == DBNull.Value ? 0 : Convert.ToDouble(value);

            // Calculate values for each row and assign directly
            row["Habitable"] = SafeConvertToDouble(row["Households_Consented"]) +
                               SafeConvertToDouble(row["Households_Refused"]) +
                               SafeConvertToDouble(row["Households_Not_At_Home"]) +
                               SafeConvertToDouble(row["Households_Absent_For_Long"]) +
                               SafeConvertToDouble(row["Households_Postponed"]) +
                               SafeConvertToDouble(row["Households_Not_Found"]) +
                               SafeConvertToDouble(row["Households_Others"]);

            row["NotHabitable"] = SafeConvertToDouble(row["Households_Destroyed"]) +
                                  SafeConvertToDouble(row["Households_Vacant"]);

            double householdsVisited = SafeConvertToDouble(row["Households_Visited"]);
            row["RefusalRate"] = householdsVisited == 0 ? 0 : SafeConvertToDouble(row["Households_Refused"]) * 100 / householdsVisited;
            row["AcceptanceRate"] = householdsVisited == 0 ? 0 : (SafeConvertToDouble(row["Households_Consented"]) + SafeConvertToDouble(row["Households_Refused"])) * 100 / householdsVisited;
            row["PHIAResponseRate"] = householdsVisited == 0 ? 0 : SafeConvertToDouble(row["Habitable"]) * 100 / householdsVisited;
            row["SampleMethodResponseRate"] = householdsVisited == 0 ? 0 : SafeConvertToDouble(row["Households_Consented"]) * 100 / householdsVisited;

            row["PartialOrWithdrawn"] = SafeConvertToDouble(row["Consented_15_Above"]) - SafeConvertToDouble(row["Completed_Intv_15_Above"]);
            row["IncompleteBloodDraw"] = SafeConvertToDouble(row["Consented_For_Draw_15_Above"]) - SafeConvertToDouble(row["Draws_15_Above"]);

            double eligible15Above = SafeConvertToDouble(row["Eligible_15_Above"]);
            row["InterviewResponseRate"] = eligible15Above == 0 ? 0 : SafeConvertToDouble(row["Completed_Intv_15_Above"]) * 100 / eligible15Above;

            double completedIntv15Above = SafeConvertToDouble(row["Completed_Intv_15_Above"]);
            row["DrawResponseRate"] = completedIntv15Above == 0 ? 0 : SafeConvertToDouble(row["Draws_15_Above"]) * 100 / completedIntv15Above;

            row["TotalAdultResponseRate"] =
                eligible15Above == 0 || completedIntv15Above == 0
                    ? 0
                    : (completedIntv15Above / eligible15Above) * (SafeConvertToDouble(row["Draws_15_Above"]) / completedIntv15Above) * 100;
        }

        ViewBag.dt = dt;
        return View();
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
        var result = new List<Trend>();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            var states = ds.Tables[0].DefaultView.ToTable(true, "State");
            foreach (DataRow st in states.Rows)
            {
                var dict = new Dictionary<string, List<double[]>>();
                var rs = new List<double[]>();
                foreach (DataRow row in ds.Tables[0].Select(string.Format("State = '{0}'", st.ItemArray[0])))
                {
                    //result.Add(new double[] { training.start_date.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, training.tdb_training_attendance.Count() });
                    var item = row.ItemArray;
                    rs.Add(new double[] { Convert.ToDateTime(item[1]).Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, Convert.ToDouble(item[0]) });
                }
                //dict.Add(st.ItemArray[0].ToString(), rs);
                result.Add(new Trend { name = st.ItemArray[0].ToString(), data = rs });
            }

        }

        return result;
    }

    public async Task<IActionResult> IndAnalytic(int id = 1)
    {
        var recordSet = await _recordService.GetAimsRecordSetAsync();
        ViewBag.recordSet = recordSet;

        var valueSets = await _recordService.GetAimsValueSetAsync();
        var default_record = recordSet.First(m => m.Id == id);
        ViewBag.values = valueSets.Where(m => m.TableName == default_record.Name.ToLower()).ToList();
        return View();
    }

    public async Task<string> GetIndAnalyticDynamic(int id)
    {
        return await _dashboardService.GetIndAnalyticDynamic(id);
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
