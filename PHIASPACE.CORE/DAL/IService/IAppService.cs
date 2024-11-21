using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.DAL.Model.Enum;

namespace PHIASPACE.CORE.DAL.IService{
    public interface IAppService
    {
        Task<int> AddPhiaApp(PhiaApp App);
        Task<int> UpdatePhiaApp(PhiaApp app);
        Task<List<PhiaApp>> GetActiveApps();
        IQueryable<PhiaApp> GetApps();
        Task<PhiaApp> GetApp(int id);
        Task<List<PhiaApp>> GetProjectApps(string project_abbrev);
        int ToggleAppStatus(int id, AppStatus status);
    }
}