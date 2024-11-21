using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;

namespace PHIASPACE.CORE.Utility{
    public class DbUtils{
        private readonly IProjectService _projectService;

        public DbUtils(IProjectService projectService)
        {
            _projectService=projectService;
        }

        public List<PhiaProject> GetPhiaProjects(){
            return _projectService.GetActiveProjects().Result;
        }

        public List<PhiaProject> GetProjects(string user_id){
            var project = _projectService.GetProjects(user_id).Result;       
            return project;
        }
        
    }
}