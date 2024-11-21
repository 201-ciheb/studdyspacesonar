using Microsoft.EntityFrameworkCore;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.DAL.Model.Enum;

namespace PHIASPACE.CORE.DAL.Service
{
    public class AppService :IAppService {
        private readonly PhiaSpaceContext _context;

        public AppService(PhiaSpaceContext context)
        {
            _context=context;
        }

        public async Task<int> AddPhiaApp(PhiaApp app){
            _context.PhiaApps.Add(app);
            await _context.SaveChangesAsync();
            return app.Id;
        }

        public async Task<int> UpdatePhiaApp(PhiaApp app){
            var existing = await _context.PhiaApps.FirstOrDefaultAsync(m => m.Id == app.Id);
            if(existing != null){
                app.CreateDate = existing.CreateDate;
                app.UpdateDate = DateTime.Now;
                if(string.IsNullOrEmpty(app.AppIconPath)){
                    app.AppIconPath = existing.AppIconPath;
                }
                _context.Entry(existing).CurrentValues.SetValues(app);
                _context.Entry(existing).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return app.Id;
        }
        
        public async Task<List<PhiaApp>> GetActiveApps(){
            return await _context.PhiaApps.Where(m => m.AppStatus == AppStatus.Active.ToString()).ToListAsync();
        }

        public IQueryable<PhiaApp> GetApps(){
            return _context.PhiaApps.AsQueryable();
        }

        public async Task<PhiaApp> GetApp(int id){
            var app = await _context.PhiaApps.FirstOrDefaultAsync(m => m.Id == id);
            return app;
        }

        public async Task<List<PhiaApp>> GetProjectApps(string project_abbrev){
            var project = await _context.PhiaProjects.FirstOrDefaultAsync(m => m.ProjectAbbrev == project_abbrev);
            if(project != null)
                return await _context.PhiaApps.Where(m => m.ProjectId == project.Id && m.AppStatus == "Active").ToListAsync();
            return new List<PhiaApp>();
        }

        public int ToggleAppStatus(int id, AppStatus status){
            var existing = _context.PhiaApps.FirstOrDefault(m => m.Id == id);
            if(existing != null){
                existing.AppStatus = status.ToString();
                _context.Entry(existing).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return id;
        }   

        // public async Task<List<PhiaApp>> GetUserAppsForProject(string project_abbrev, string user_id){
        //     var project = await _context.PhiaProjects.FirstOrDefaultAsync(m => m.ProjectAbbrev == project_abbrev);
        //     if(project != null)
        //         return await _context.PhiaApps.Where(m => m.ProjectId == project.Id && m.AppStatus == "Active").ToListAsync();
        //     return new List<PhiaApp>();
        // }
    
    }
}