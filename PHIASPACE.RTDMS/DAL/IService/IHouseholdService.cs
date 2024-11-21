using System.Data;

namespace PHIASPACE.RTDMS.DAL.IService
{
    public interface IHouseholdService
    {
        Task<DataSet> GetHouseholdDetailsAsync(int clusterNo, int hhId);
        Task<DataSet> GetAllHouseholdDataAsync();
        Task<DataSet> GetHouseholdDataAsync(int page_id, int pageSize, int? level, int? locationId);
        Task<DataSet> GetHouseholdSummaryAsync(int clusterNo, int hhId);
        // AimsPerson GetTeamLead(int cluster);
    }
}


