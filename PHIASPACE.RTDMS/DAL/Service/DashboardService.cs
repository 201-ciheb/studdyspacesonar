using System.Data;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.DAL.IService;

namespace PHIASPACE.RTDMS.DAL.Service;
public class DashboardService : IDashboardService
{

    private readonly IAimsDbProvider _dbProvider;
    private readonly IConfiguration _configuration;
    private readonly IRecordService _recordervice;


    public DashboardService(IAimsDbProvider dbProvider, IConfiguration configuration, IRecordService recordervice)
    {
        _dbProvider = dbProvider;
        _configuration = configuration;
        _recordervice = recordervice;
    }

    public async Task<DataTable> GetEAMonitorAsync()
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            "[report].[sp_ea_monitor]" :
            "sp_ea_monitor";
        var ea_summary = await _dbProvider.ExecuteSpAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());
        return ea_summary;
    }

    public async Task<DataSet> GetTeamSummaryDataSetAsync()
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            "[report].[sp_team_summary]" :
            "sp_team_summary";
        var team_summary = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString());
        return team_summary;
    }

    public async Task<DataTable> GetHhResponseRate()
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            "[report].[sp_get_hhrr]" :
            "sp_get_hhrr";
        var hh_response_rate_summary = await _dbProvider.ExecuteSpAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());
        return hh_response_rate_summary;
    }

    public async Task<DataSet> GetHhEaSummary()
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            "[report].[sp_get_hh_ea_summary]" :
            "sp_get_hh_ea_summary";
        var hh_response_rate_summary = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString());
        return hh_response_rate_summary;
    }


    public async Task<DataTable> GetLinkageDashboardAsync()
    {
        var sqlQuery = "sp_get_linkage_dashboard";
        return await _dbProvider.ExecuteSpAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());
    }

    public async Task<DataSet> GetRegionTrend()
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            "[report].[sp_get_state_performance_trend]" :
            "sp_get_state_performance_trend";
        var region_summary = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString());
        return region_summary;
    }

    public async Task<DataSet> GenerateDynamicQuery(string sqlQuery)
    {
        var summary = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString());
        return summary;
    }

    public async Task<DataSet> GenerateDynamicDataSetQuery(string sqlQuery)
    {
        var dyanmic_table = await _dbProvider.ExecuteQueryAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString());
        return dyanmic_table;
    }

    public async Task<string> GetIndAnalyticDynamic(int id)
    {
        // Fetch value set for the provided record ID
        var valueSet = await _recordervice.GetAimsValueSetAsync();
        var categories = valueSet.Where(m => m.RecordId == id);
        var sqlArguments = new List<string>();

        // Construct SQL arguments from value set categories
        foreach (var item in categories.OrderBy(m => m.Id))
        {
            sqlArguments.Add(string.Format("count(case when {0}.{1} = {2} then {3} else null end) as `{4}`",
                item.TableName, item.ItemName, item.ValueId, item.CountParameter, item.Value));
        }

        // Determine database type and construct SQL query accordingly
        var dbType = _configuration["DatabaseType:DbType"];
        string sqlQuery;

        if (dbType == "MSSQL")
        {
            sqlQuery = categories.First().SpName;
        }
        else if (dbType == "MySQL")
        {
            sqlQuery = categories.First().SpName;
        }
        else
        {
            throw new InvalidOperationException("Unsupported database type.");
        }

        // Prepare parameters for the stored procedure
        var parameters = new[]
        {
        _dbProvider.CreateParameter("@selectVariables", string.Join(",", sqlArguments), DbType.String)
    };

        // Execute the query with parameters
        var resultTable = await _dbProvider.ExecuteSpAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString(), parameters);

        // Convert the result to JSON if there are rows in the result
        if (resultTable.Rows.Count > 0)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(resultTable);
        }

        return string.Empty;
    }


}