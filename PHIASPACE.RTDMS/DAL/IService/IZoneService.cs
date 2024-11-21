using PHIASPACE.RTDMS.DAL.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHIASPACE.RTDMS.DAL.IService
{
    public interface IZoneService
    {
        int GetUserZone(string username);
        IQueryable<AimsZone> GetZones();
        Task<List<AimsCounty>> GetCountiesAsync();
        Task<List<RtdmsIssueType>> GetIssueTypes();
        Task<List<RtdmsIssueStatus>> GetIssueStatuses();
        // List<AimsLnkFormAge> GetFormLink();
        List<string> GetPersonRoles(string email);
        IQueryable<AimsLookup> GetLookupOptions(string category);
        // IQueryable<aims_tree_data> GetTreeData();
        // IQueryable<AimsEa> GetEa();
        IQueryable<AimsValueSet> GetValueSets();
        AimsPerson GetTeamLead(int cluster);
        Task<List<LbLab>> GetLabsAsync();
        IQueryable<LbLab> GetLabs();
    }
}
