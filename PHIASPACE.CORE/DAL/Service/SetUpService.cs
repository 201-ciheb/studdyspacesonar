using System.Security.Claims;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.IdentityConfiguration;

namespace PHIASPACE.CORE.DAL.Service{
    public class SetUpService: ISetUpService{

        private readonly PhiaSpaceContext _context;

        public SetUpService(PhiaSpaceContext context)
        {
            _context=context;
        }

        //private readonly PhiaSpaceContext _context = new PhiaSpaceContext();
        
        public bool CheckDB(){
            var rr = _context.Database.GetService<IRelationalDatabaseCreator>().Exists();
            return rr;
        }


        public bool CreateAdminandUsers(UserManager<PhiaSpaceUser> userMgr){
            _context.Database.EnsureCreated();
            var superadmin = userMgr.FindByNameAsync("superadmin").Result;
            if (superadmin == null)
            {
                superadmin = new PhiaSpaceUser { Email = "superadmin@phiaspace.org", UserName = "superadmin", Address="", FullName="Super Admin", Gender="", Deleted=0};
                
                var result = userMgr.CreateAsync(superadmin, "P@ssw0rd").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                result = userMgr.AddClaimsAsync(superadmin, new Claim[]{
                    new Claim(JwtClaimTypes.Name, "Super Admin"),
                    new Claim(JwtClaimTypes.GivenName, "Super"),
                    new Claim(JwtClaimTypes.FamilyName, "Admin")
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                return true;
            }
            return true;
        }

        public bool DoMigrations(ConfigurationDbContext _dbcontext){
            _dbcontext.Database.Migrate();
            _context.Database.Migrate();
            if (!_dbcontext.Clients.Any())
            {
                foreach (var client in Clients.Get())
                {
                    _dbcontext.Clients.Add(client.ToEntity());
                }
                _dbcontext.SaveChanges();
            }

            if (!_dbcontext.IdentityResources.Any())
            {
                foreach (var resource in Resources.GetIdentityResources())
                {
                    _dbcontext.IdentityResources.Add(resource.ToEntity());
                }
                _dbcontext.SaveChanges();
            }

            if (!_dbcontext.ApiScopes.Any())
            {
                foreach (var resource in Scopes.GetApiScopes())
                {
                    _dbcontext.ApiScopes.Add(resource.ToEntity());
                }
                _dbcontext.SaveChanges();
            }
            return true;
        }
    }
}