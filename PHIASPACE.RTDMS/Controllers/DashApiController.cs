using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;
using PHIASPACE.RTDMS.DAL.MssqlDbService;
using PHIASPACE.RTDMS.DAL.Service;
using PHIASPACE.RTDMS.Helpers;
using PHIASPACE.RTDMS.Models;
using PHIASPACE.RTDMS.Utility;

namespace PHIASPACE.RTDMS.Controllers
{
	public class DashApiController : Controller
    {
        ICAPIService _cAPIService;
        ITeamService _teamService;
        IIssueService _issueService;
        IAIssueService _aissueService;
        private readonly UserHelper _userHelper;
        private readonly TreeHelper _treeHelper;
        private readonly IHouseholdService _householdService;
        private readonly ITeamService _teamServices;
        private readonly IZoneService _zoneService;
        static readonly AimsMssqlDbContext _dbcontext = new AimsMssqlDbContext();
        private readonly IMonitoringService _monitoringService;


        public DashApiController(ICAPIService cAPIService, ITeamService teamService,
        IIssueService issueService, IAIssueService aissueService,
        UserHelper userHelper, TreeHelper treeHelper, IHouseholdService householdService,
        ITeamService teamServices, IZoneService zoneService,
        IMonitoringService monitoringService)
        {
            _teamService = teamService;
            _cAPIService = cAPIService;
            _issueService = issueService;
            _aissueService = aissueService;
            _userHelper = userHelper;
            _treeHelper = treeHelper;
            _householdService = householdService;
            _teamServices = teamServices;
            _zoneService = zoneService;
            _monitoringService = monitoringService;
        }

        public async Task<JObject> GetTree()
        {
            return await _treeHelper.GetTreeAsync();
        }

        public async Task<DataTable> get_form(string id)
        {
            var formId = id.Split('_')[0];
            var lineNumber = id.Split('_')[1];

            using (var command = _dbcontext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_get_participant_form";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@form_id", formId));
                command.Parameters.Add(new SqlParameter("@line_number", lineNumber));

                await _dbcontext.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }

        [AllowAnonymous]
        public async Task<DataSet> GetCompletion()
        {
            using (var command = _dbcontext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_get_completion";
                command.CommandType = CommandType.StoredProcedure;

                await _dbcontext.Database.OpenConnectionAsync();

                using (var adapter = new SqlDataAdapter((SqlCommand)command))
                {
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }


        public async Task<DataSet> GetPtSummary()
        {
            var dataSet = new DataSet();

            using (var command = _dbcontext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_get_participant_summary";
                command.CommandType = CommandType.StoredProcedure;

                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = (SqlCommand)command;

                    // Fill the DataSet
                    adapter.Fill(dataSet);
                }
            }

            return dataSet;
        }

        [HttpPost]
        public void post_clients(HttpRequestMessage response)
        {
            var value = response.Content.ReadAsStringAsync().Result;
            var array = JArray.Parse(value);
            var result = new JArray();

            for (var i = 0; i < array.Count; i++)
            {
                var obj = JObject.Parse(array[i].ToString());
                // if (_cAPIService.GetHousehold(Convert.ToInt32(obj["ea_id"]), Convert.ToInt32(obj["hh_id"])) == null)
                //     continue;

                // result.Add(ProcessHHData(Convert.ToInt32(obj["ea_id"]), Convert.ToInt32(obj["hh_id"])));
            }

        }


        // JObject ProcessHHData(int ea_id,int hh_id)
        // {
        //     var hh = _cAPIService.GetHousehold(ea_id, hh_id);

        //     var obj = new JObject();
        //     obj.Add("team", TeamUtil.GetTeam(hh.A1QCLUST).ToString("D2"));
        //     obj.Add("ea", hh.A1QCLUST.ToString("D4"));
        //     obj.Add("hh", hh.A1QNUMBER.ToString("D2"));
        //     obj.Add("date_created", hh.created.Value.ToString("yyyy-MM-dd h: mm tt"));
        //     //obj.Add("inds", HHUtil.GetHHIndividual(ea_id, hh_id).Count());
        //     //obj.Add("qstn", HHUtil.GetQuestionaires(ea_id, hh_id));
        //     //obj.Add("consnts", HHUtil.GetConsents(ea_id, hh_id));

        //     return obj;
        // }

        // public JsonResult GetEas()
        // {
        //     var eas = Utils.GetEa();
        //     var n_eas = new List<EA>();
        //     foreach (var ea in eas)
        //     {
        //         n_eas.Add(new EA(ea));
        //     }

        //     return Json(n_eas);
        // }

        public async Task<IActionResult> GetTeamEaSummary(int id)
        {
            var read_only = false;//_userHelper.UserInRole(new[] { "RDA" });

            var tb_report = "";

            var hh_stats_table = await _monitoringService.GetTeamEaStat(id);
            if (hh_stats_table != null && hh_stats_table.Rows.Count > 0)
            {
                foreach (DataRow row in hh_stats_table.Rows)
                {
                    if (row[15] == null || row[15].ToString() == "")
                    {
                        tb_report += "<tr>";
                        tb_report += read_only ? "<td></td>" : string.Format("<td><a href='/DMDashboard/HHData?ea_id={0}&hh_id={1}' target='_blank' class='btn border-warning-400 text-warning-400 btn-flat btn-rounded btn-icon btn-xs'><i class='icon-menu-open2'></i></a></td>", id, row[0]);

                        tb_report += $"<td>{Convert.ToInt32(row[0]):D2} - {row[1]}</td>";
                        for (int i = 0; i < 13; i++)
                        {
                            tb_report += "<td></td>";
                        }
                        tb_report += "</tr>";
                    }
                    else
                    {
                        tb_report += "<tr>";
                        tb_report += read_only ? "<td></td>" : string.Format("<td><a href='/DMDashboard/HHData?ea_id={0}&hh_id={1}' target='_blank' class='btn border-warning-400 text-warning-400 btn-flat btn-rounded btn-icon btn-xs'><i class='icon-menu-open2'></i></a></td>", id, row[0]);

                        tb_report += $"<td>{Convert.ToInt32(row[0]):D2} - {row[1]}</td>";
                        tb_report += $"<td>{row[2]}</td>";
                        tb_report += $"<td>{row[3]}</td>";
                        tb_report += $"<td>{row[4]}</td>";
                        tb_report += $"<td>{row[5]}</td>";

                        tb_report += Utils.ConvertObjectToInt(row[4]) + Utils.ConvertObjectToInt(row[5]) != Utils.ConvertObjectToInt(row[3])
                            ? $"<td style='background-color:yellow'>{row[6]}</td>"
                            : $"<td>{row[6]}</td>";

                        tb_report += Utils.ConvertObjectToInt(row[6]) != Utils.ConvertObjectToInt(row[7])
                            ? $"<td style='background-color:lightsalmon'>{row[7]}</td>"
                            : $"<td>{row[7]}</td>";

                        tb_report += $"<td>{row[8]}</td>";
                        tb_report += Utils.ConvertObjectToInt(row[8]) != Utils.ConvertObjectToInt(row[9])
                            ? $"<td style='background-color:lightsalmon'>{row[9]}</td>"
                            : $"<td>{row[9]}</td>";

                        tb_report += $"<td>{row[10]}</td>";
                        tb_report += $"<td>{row[11]}</td>";
                        tb_report += $"<td>{row[12]}</td>";

                        tb_report += Utils.ConvertObjectToInt(row[12]) != Utils.ConvertObjectToInt(row[13])
                            ? $"<td style='background-color:lightsalmon'>{row[13]}</td>"
                            : $"<td>{row[13]}</td>";

                        tb_report += $"<td>{row[14]}</td>";
                        tb_report += $"<td class='m_date'>{row[15]}</td>";
                        tb_report += "</tr>";
                    }
                }
            }

            var ea_team = await _monitoringService.GetTeamEaMembers(id);
            var tm_teport = "";

            if (ea_team != null)
            {
                foreach (DataRow person in ea_team.Rows)
                {
                    tm_teport += "<tr>";
                    tm_teport += $"<td>{person[0]}</td>";
                    tm_teport += $"<td>{person[1]}</td>";
                    tm_teport += $"<td><a href ='mailto:{person[2]}'>{person[2]}</a></td>";
                    tm_teport += $"<td><a href='tel:{person[3]}'>{person[3]}</a></td>";
                    tm_teport += "</tr>";
                }
            }

            return Ok( new {
                report = tb_report,
                team = tm_teport
            });
        }


        public async Task<IActionResult> GetHHSummary(string id)
        {
            var vals = id.Split('_');
            var ea_id = Convert.ToInt32(vals[0]);
            var hh_id = Convert.ToInt32(vals[1]);

            var read_only = _userHelper.UserInRole(new[] { "RDA" });

            var dataSet = new DataSet();

            // Using the DbContext to execute the stored procedure
            var ds = await _householdService.GetHouseholdSummaryAsync(ea_id, hh_id);

            Console.WriteLine(ds);

            // Log the DataSet contents
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                Console.WriteLine($"Table {i} has {ds.Tables[i].Rows.Count} rows");
                foreach (DataRow row in ds.Tables[i].Rows)
                {
                    Console.WriteLine($"Table {i}, Row {row.Table.Rows.IndexOf(row)}: {string.Join(", ", row.ItemArray)}");
                }
            }


            var table1 = ds.Tables[0];
            var header = $"{ea_id:D4}-{hh_id:D4}";
            if (table1 != null && table1.Rows.Count > 0)
            {
                var values = table1.Rows[0].ItemArray;
                header += $"<span> | {values[6]}</span>";
            }

            // var person = _householdService.GetTeamLead(ea_id);
            var person = new AimsPerson();// _householdService.GetTeamLead(ea_id);
            var person_header = "";
            if (person != null && !read_only)
            {
                person_header += $"<li><a href=''>{person.LastName} {person.FirstName}</a></li>";
                person_header += $"<li><a href='tel:{person.Phone}'><i class='icon-phone'></i></a></li>";
                person_header += $"<li><a href='mailto:{person.Email}'><i class='icon-mail5'></i>  | </a></li>";
                person_header += $"<li><a href='#' onclick='mark_as_completed({ea_id},{hh_id})' style='color:green'><i class='icon-checkbox-checked'></i></a></li>";
            }

            var hh_qs = "";
            var table2 = ds.Tables[1];
            if (table1 != null && table1.Rows.Count > 0)
            {
                var rw = table1.Rows[0].ItemArray;
                Console.WriteLine($"rw[3]: {rw[3]}, Type: {rw[3].GetType()}");
                if (rw[3].ToString() == "Completed")
                {
                    hh_qs += $"<a class='btn btn-success' style='margin: 3px' href='/DMDashboard/PData?form_id={1}&cluster_no={ea_id}&hh={hh_id}&line={0}'> A1 - Household Question</a>";
                    hh_qs += "<a class='btn btn-success' style='margin: 3px' href='#'> B1 - Consent for Household</a>";
                }
                else if (rw[3].ToString() == "Refused")
                {
                    hh_qs += "<a class='btn btn-danger' style='margin:3px' href='#'> C1 - Refusal withdrawal form</a>";
                }

                hh_qs += $"<a style='margin:3px' href='#'><i class='icon-quill4'></i><span class='badge bg-slate-400'>{/*HHUtil.GetSignatures(ea_id, hh_id, 1)*/0}</span></a>";

                hh_qs += "<div class='pull-right' style='margin-top:10px; margin-right:10px'>";
                Console.WriteLine($"rw[6]: {rw[6]}, Type: {rw[6].GetType()}");
                if (Convert.ToInt32(rw[6]) == 1)
                {
                    hh_qs += "<i class='icon-page-break' style='color:green'></i>";
                }
                if (Convert.ToInt32(rw[5]) == 1)
                {
                    hh_qs += "<i class='icon-person' style='color:green'></i>";
                }
                hh_qs += "</div>";
            }

            var table3 = ds.Tables[2];
            var table_header = "<tr><th>Individual forms</th>";
            if (table3 != null && table2 != null)
            {
                foreach (DataRow row in table2.Rows)
                {
                    var style = "yellow";
                    var itm = row.ItemArray;
                    try
                    {
                        //var completed = HHUtil.ParticipantCompleted(Convert.ToInt32(itm[5]), Convert.ToInt32(itm[4]), Convert.ToInt32(itm[7]), Convert.ToInt32(itm[1]));
                        //style = completed switch
                        //{
                        //    -1 => "gray",
                        //    1 => "lightgreen",
                        //    _ => style
                        //};
                    }
                    catch (Exception) { }

                    table_header += $"<th style='background-color:{style}'>{itm[0]} {itm[1]}</th>";
                }
                table_header += "</tr><tr><th><strong>Interview status</strong></th>";
                foreach (DataRow row in table2.Rows)
                {
                    var itm = row.ItemArray;
                    table_header += $"<th><code>{itm[9]}</code> - {itm[10]}";
                    table_header += $"<a style='margin:3px' href='#'><i class='icon-quill4'></i><span class='badge bg-slate-400'>{/*HHUtil.GetSignatures(ea_id, hh_id, Convert.ToInt32(itm[7]))*/0}</span></a></th>";
                }
            }
            table_header += "</tr>";

            var form_links = ""; //Utils.GetFormLink();
            var table_content = "";
            foreach (DataRow row in table3.Rows)
            {
                var cell = row.ItemArray;
                table_content += "<tr><td><strong>" + cell[1].ToString() + "</strong></td>";
                foreach (DataRow rw in table2.Rows)
                {
                    var itm = rw.ItemArray;
                    var form_id = Convert.ToInt32(cell[0]);

                    try
                    {
                        var age = Convert.ToInt32(itm[1]);
                        // var el = form_links.Where(e => e.FormId == form_id && e.Age.AgeLower <= age && e.Age.AgeUpper >= age);
                        var el = new List<dynamic>(); // form_links.Where(e => e.FormId == form_id && e.Age.AgeLower <= age && e.Age.AgeUpper >= age);
                        if (el.Any())
                        {
                            table_content += "<td>";
                            try
                            {
                                //if (HHUtil.UserHasForm(form_id, Convert.ToInt32(itm[5]), Convert.ToInt32(itm[4]), Convert.ToInt32(itm[7])))
                                //{
                                //    if (el.FirstOrDefault().Form.FormType == "form")
                                //    {
                                //        table_content += $"<a class='btn btn-xs btn-success' target='_blank' style='display:block' href='/DMDash/PData?form_id={form_id}&cluster_no={ea_id}&hh={hh_id}&line={itm[7]}' onclick='load_form({form_id},{itm[0]},'{cell[1].ToString()}')'><i class='icon-check2'></i></a>";
                                //    }
                                //    else
                                //    {
                                //        table_content += "<a class='btn btn-xs btn-success' style='display:block' href='#'><i class='icon-check2'></i></a>";
                                //    }
                                //}
                                //else
                                //{
                                //    table_content += "<a class='btn btn-xs btn-danger' style='display:block' href='#'><i class='icon-cancel-circle2'></i></a>";
                                //}
                            }
                            catch (Exception) { }

                            table_content += "</td>";
                        }
                        else
                        {
                            table_content += "<td style='background-color:#cccccc;border-color:#cccccc'></td>";
                        }
                    }
                    catch (Exception)
                    {
                        table_content += "<td style='background-color:yellow;border-color:#cccccc'></td>";
                    }
                }
                table_content += "</tr>";
            }

            var result = new JObject
            {
                { "header", header },
                { "person_header", person_header },
                { "hh_qs", hh_qs },
                { "table_header", table_header },
                { "table_content", table_content }
            };

            //var issues = _aissueService.GetIssues().Where(e => e.HouseholdId == hh_id && e.EaId == ea_id);
            var issues = await _aissueService.GetIssue(ea_id, hh_id);
            var pm_issues = issues.Select(issue => new Issue(issue)).ToList();

            result.Add("issues", issues.Any() ? JsonConvert.SerializeObject(pm_issues) : "[]");
            // Log the result before returning it
            var jsonResult = JsonConvert.SerializeObject(result);
            Console.WriteLine("Result Data: " + jsonResult);

            return Content(jsonResult, "application/json");
        }


        // public List<int> GetActiveEas()
        // {
        //     return Utils.GetEa().Where(e => e.Active == 1).Select(e => e.Id).ToList();
        // }

        //[HttpPost]
        //public void Post(HttpRequestMessage respone)
        //{
        //    var value = respone.Content.ReadAsStringAsync().Result;
        //    //var message = JObject.Parse(value)["message"].ToString();
        //    var message = value.Split('=')[1];
        //    if (message.StartsWith("OEA"))
        //    {
        //        Utils.ActivateCluster(Convert.ToInt32(message.Split('*')[1]), 1);
        //    }
        //    else if (message.StartsWith("CEA"))
        //    {
        //        Utils.ActivateCluster(Convert.ToInt32(message.Split('*')[1]), 2);
        //    }
        //    else if (message.StartsWith("OHH"))
        //    {
        //        Utils.ChangeHHStatus(Convert.ToInt32(message.Split('*')[1]), Convert.ToInt32(message.Split('*')[2]), 1);
        //    }
        //    else if (message.StartsWith("CHH"))
        //    {
        //        Utils.ChangeHHStatus(Convert.ToInt32(message.Split('*')[1]), Convert.ToInt32(message.Split('*')[2]), 2);
        //    }
        //}

        // [HttpPost]
        // public DataSet PostIndAnal(HttpRequestMessage respone)
        // {
        //    return HHUtil.generate_query("AHRESULT", "CAPI_AHSECOVER", "[A1QCLUST]");
        // }

        // public DataSet GetIndAnal(string id)
        // {
        //     var val = Convert.ToInt32(id.Split('_')[0]);
        //     var valueset = _zoneService.GetValueSets().FirstOrDefault(e => e.Id == val);
        //     return HHUtil.generate_query(valueset.ItemName, "CAPI_"+valueset.Label, id.Split('_')[1]);
        // }

        [HttpPost]
        public void PostHHStatus(HttpRequestMessage request)
        {
            var value = request.Content.ReadAsStringAsync().Result;
        }


        [HttpPost]
        public int MarkCompleted([FromBody] MarkCompletedRequest request)
        {
            try
            {
                // Ensure the values are properly parsed
                if (request == null || request.ea_id == 0 || request.hh_id == 0)
                {
                    throw new ArgumentException("Invalid parameters");
                }

                // Call the service method to mark as completed
                // _cAPIService.MarkCompleted(request.ea_id, request.hh_id, User.Identity.Name);
                return 1;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return 0;
            }
        }

        // Define a class to represent the incoming request
        public class MarkCompletedRequest
        {
            public int ea_id { get; set; }
            public int hh_id { get; set; }
        }


        public List<Trend> GetStateTrend()
        {
            var result = new List<Trend>();

            try
            {
                var sql = "sp_get_state_performance_trend";

                using (var command = _dbcontext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;

                    // No need to explicitly open the connection, EF will handle it
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var stateTrends = new DataTable();
                            stateTrends.Load(reader);

                            var states = stateTrends.DefaultView.ToTable(true, "State");
                            foreach (DataRow st in states.Rows)
                            {
                                var rs = new List<double[]>();
                                foreach (DataRow row in stateTrends.Select($"State = '{st["State"]}'"))
                                {
                                    rs.Add(new double[]
                                    {
                                Convert.ToDateTime(row["Date"]).Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                                Convert.ToDouble(row["Value"])
                                    });
                                }
                                result.Add(new Trend { name = st["State"].ToString(), data = rs });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., logging the error
            }

            return result;
        }



        // [HttpPost]
        // public List<string> run_hh_qc(HttpRequestMessage respone)
        // {
        //     var value = respone.Content.ReadAsStringAsync().Result;
        //     var message = Newtonsoft.Json.Linq.JObject.Parse(value);
            
        //     return HHUtil.RunHHQC(Convert.ToInt32(message["ea_id"]), Convert.ToInt32(message["hh_id"]));
        // }

        public async Task<List<string[]>> GetValues(string id)
        {
            string[] item_names = "AZONE,ASTATE,AVRESULT,AINTM,LHIVF,LSMONTH,AHVRESULT,M703Y,M703M,AHINTCD,AHINTC".Split(',');
            var ex_values = "Continue".Split(',');
            var result = new List<string[]>();
            var all_db_values = _zoneService.GetValueSets();
            var values = all_db_values.Where(e => e.Label == id && !item_names.Contains(e.ItemName) && !ex_values.Contains(e.Value) && e.ValuesetType=="normal" ).ToList();
            var distinct_values = values.Select(e => e.Label).Distinct();
            foreach (var value in distinct_values)
            {
                var value_set = values.FirstOrDefault(e => e.Label == value);
                var st = new string[] { value_set.Id.ToString(), value };
                result.Add(st);
            }

            return result;
        }

// #if !DEBUG
//         [PermissionFilter(role = new[] { "DMA" })]
// #endif
        [HttpPost]
        public int FieldClosed(HttpRequestMessage response)
        {
            // var value = response.Content.ReadAsStringAsync().Result;
            // var json = JObject.Parse(value);

            // var ea_id = Convert.ToInt32(json["ea_id"]);
            // var date = Convert.ToDateTime(json["date"]);

            // try
            // {
            //     var ea = Utils.GetEa().FirstOrDefault(e => e.id == ea_id);
            //     //var date_closed = Convert.ToDateTime(date);
            //     ea.date_closed = date;

            //     _eaService.UpdateEa(ea);
            //     return 1;
            // }
            // catch (Exception ex)
            // {
            //     return 0;
            // }
            return 0;
        }

// #if !DEBUG
//         [PermissionFilter(role = new[] { "DMA" })]
// #endif
        [HttpPost]
        public int DmValidated(HttpRequestMessage response)
        {
            // var value = response.Content.ReadAsStringAsync().Result;
            // var json = JObject.Parse(value);

            // var ea_id = Convert.ToInt32(json["ea_id"]);

            // try
            // {
            //     var ea = Utils.GetEa().FirstOrDefault(e => e.id == ea_id);
            //      ea.active = 1;

            //     _eaService.UpdateEa(ea);
            //     return 1;
            // }
            // catch (Exception)
            // {
            //     return 0;
            // }
            return 0;
        }
    }
}
