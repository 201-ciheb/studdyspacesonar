using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Setup;

namespace PHIASPACE.CORE.Controllers{
    public class SetUpController : Controller{

        private readonly UserManager<PhiaSpaceUser> _userManager;   
        private readonly ISetUpService _setUpService;
        private readonly ConfigurationDbContext _dbcontext;

        public SetUpController(UserManager<PhiaSpaceUser> userManager, ISetUpService setUpService, ConfigurationDbContext dbcontext)
        {
            _userManager=userManager;
            _setUpService=setUpService;
            _dbcontext=dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartSetup(string dbname){
            dbname="PHIASpace";
            //TODO: Implement this with signal R
            //TODO: Implement adding the core to PHIAApps table

            //do migration
            _setUpService.DoMigrations(_dbcontext);
            //create users
            _setUpService.CreateAdminandUsers(_userManager);
            //SqlManager sqlManager = new SqlManager(dbname);
            //sqlManager.createDatabase();
            // string sql = @"USE " + dbname  + @"   
            //         Alter Table dbo.Clients Alter Column PairWiseSubjectSalt nvarchar(200) null; 
            //         Alter Table dbo.Clients Alter Column Description nvarchar(1000) null;
            //         Alter Table dbo.Clients Alter Column BackChannelLogoutUri nvarchar(1000) null;
            //         Alter Table dbo.Clients Alter Column AllowedIdentityTokenSigningAlgorithms nvarchar(1000) null;";
                    
            // sqlManager.UpdateTable(sql);
            
            //?when done
            return RedirectToAction("Index", "Home");
        }
    }
}