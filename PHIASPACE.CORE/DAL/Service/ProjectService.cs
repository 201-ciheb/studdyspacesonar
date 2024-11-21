using Microsoft.EntityFrameworkCore;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;

namespace PHIASPACE.CORE.DAL.Service
{
    public class ProjectService :IProjectService {
        private readonly PhiaSpaceContext _context;

        public ProjectService(PhiaSpaceContext context)
        {
            _context=context;
        }

        public async Task AddPhiaProject(PhiaProject project){
            _context.PhiaProjects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task EditPhiaProject(PhiaProject project){
            var exising = _context.PhiaProjects.FirstOrDefault(m => m.Id == project.Id);
            if(exising != null){
                project.Id = exising.Id;
                project.ProjectStartDate = exising.ProjectStartDate;
                _context.Entry(exising).CurrentValues.SetValues(project);
                _context.Entry(exising).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<List<PhiaProject>> GetActiveProjects(){
            return await _context.PhiaProjects.Where(m => m.Active == 1).ToListAsync();
        }

        public async Task<List<PhiaProject>> GetProjects(){
            return await _context.PhiaProjects.ToListAsync();
        }

        public async Task<PhiaProject> GetProject(int id){
            return await _context.PhiaProjects.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PhiaProject> GetProject(string abbrev){
            return await _context.PhiaProjects.FirstOrDefaultAsync(m => m.ProjectAbbrev == abbrev);
        }

        public async Task<List<PhiaUserProject>> GetUserProjects(string user_id){
            return await _context.PhiaUserProjects
                .Include(m => m.Project)
                //.Include(m => m.User)
                .Where(m => m.UserId == user_id && m.Project.Active == 1).ToListAsync();            
        }

        public async Task<List<PhiaProject>> GetProjects(string user_id){
            return await _context.PhiaUserProjects
                .Include(m => m.Project)
                .Include(m => m.User)
                .Where(m => m.UserId == user_id && m.Project.Active == 1).Select(m => m.Project).ToListAsync();            
        }

        public void AddUserProject(PhiaUserProject userProject){
            var existing = _context.PhiaUserProjects.FirstOrDefault(m => m.ProjectId == userProject.ProjectId && m.UserId == userProject.UserId);
            if(existing == null){
                _context.Add(userProject);
                _context.SaveChanges();
            }
        }
    }
}