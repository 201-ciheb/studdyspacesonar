using System.Data;

namespace PHIASPACE.RTDMS.DAL.IService;

public interface IDashboardService{
    Task<DataTable> GetEAMonitorAsync();
    Task<DataSet> GetTeamSummaryDataSetAsync();
    Task<DataTable> GetHhResponseRate();
    Task<DataSet> GetHhEaSummary();
    Task<DataTable> GetLinkageDashboardAsync();
    Task<DataSet> GetRegionTrend();
    Task<DataSet> GenerateDynamicQuery(string sqlQuery);
    Task<DataSet> GenerateDynamicDataSetQuery(string sqlQuery);
    Task<string> GetIndAnalyticDynamic(int id);

}