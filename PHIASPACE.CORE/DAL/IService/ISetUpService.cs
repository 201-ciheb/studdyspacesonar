using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using PHIASPACE.CORE.DAL.Model;

namespace PHIASPACE.CORE.DAL.IService{
    public interface ISetUpService{
        bool CheckDB();
        bool CreateAdminandUsers(UserManager<PhiaSpaceUser> userMgr);
        bool DoMigrations(ConfigurationDbContext _dbcontext);
    }
}