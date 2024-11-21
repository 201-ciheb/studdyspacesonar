using System.Data;
using PHIASPACE.RTDMS.DAL.Model.Custom;

namespace PHIASPACE.RTDMS.DAL.IService
{
    public interface IMonitoringService
    {
        Task<List<UserTeamEaDetails>> GetTeamEaDetailsAsync(int team_id, string region);
        Task<DataTable> GetTeamEaStat(int ea_number);
        Task<DataTable> GetTeamEaMembers(int ea_number);
    }
}
