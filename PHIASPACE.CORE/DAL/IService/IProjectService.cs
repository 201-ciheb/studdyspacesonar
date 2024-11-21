using PHIASPACE.CORE.DAL.Model.DB;

namespace PHIASPACE.CORE.DAL.IService{
    public interface IProjectService
    {
        Task AddPhiaProject(PhiaProject project);
        Task<List<PhiaProject>> GetActiveProjects();
        Task<List<PhiaProject>> GetProjects();
        Task<List<PhiaUserProject>> GetUserProjects(string user_id);
        Task<PhiaProject> GetProject(int id);
        void AddUserProject(PhiaUserProject userProject);
        Task<List<PhiaProject>> GetProjects(string user_id);
        Task EditPhiaProject(PhiaProject project);
        Task<PhiaProject> GetProject(string abbrev);
    }
}