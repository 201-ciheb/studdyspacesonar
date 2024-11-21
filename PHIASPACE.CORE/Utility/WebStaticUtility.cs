using System.Security.Claims;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.Models;

namespace PHIASPACE.CORE.Utility{
    public static class WebStaticUtility
    {
        public static string GetFullNameInitials(this PhiaSpaceUser user)
        {
            //check if user has more than one name
            var names = user.FullName.Split(" ");
            if (names.Length > 1)
            {
                //user has more than 1 name
                return names[0][0].ToString() + names[1][0].ToString();
            }
            else if(user.FullName.Length > 1)
            {
                return user.FullName[0].ToString() + "" + user.FullName[1].ToString();
            }
            else
            {
                return user.FullName[0].ToString();
            }
        }

        public static string GetFullNameInitials(this ClaimsPrincipal user)
        {
            if(user == null)
                return "__";
            //check if user has more than one name
            string name = user.FindFirstValue("name");
            var names = name.Split(" ");
            if (names.Length > 1)
            {
                //user has more than 1 name
                return names[0][0].ToString() + names[1][0].ToString();
            }
            else if(name.Length > 1)
            {
                return name[0].ToString() + "" + name[1].ToString();
            }
            else
            {
                return name[0].ToString();
            }
        }

        public static UiModel LoadUiModel(this UiModel uiModel, DbUtils dbUtils, string user_id = null){       
            uiModel.projects = new List<ProjectModel>();
            if(string.IsNullOrEmpty(user_id)){
                foreach (var item in dbUtils.GetPhiaProjects())
                {
                    uiModel.projects.Add(new ProjectModel{
                        Id = item.Id,
                        Name = item.ProjectName,
                        Abbreviation = item.ProjectAbbrev,
                        Description = item.ProjectDescription,
                        StartDate = item.ProjectStartDate.Value.ToString("ddd MMM, yyyy")
                    });
                }
            }
            else{
                foreach (var item in dbUtils.GetProjects(user_id))
                {
                    uiModel.projects.Add(new ProjectModel{
                        Id = item.Id,
                        Name = item.ProjectName,
                        Abbreviation = item.ProjectAbbrev,
                        Description = item.ProjectDescription,
                        StartDate = item.ProjectStartDate != null ? item.ProjectStartDate.Value.ToString("ddd MMM, yyyy") : null
                    });
                }
            }
            return uiModel;
        }
        
        public static bool HasPermission(this ClaimsPrincipal user, string permission)
        {
            bool exists = false;
            using (var _context = new PhiaSpaceContext())
            {
                exists = _context.PhiaUserPermissions.Any(m => m.Permission == permission);
            }            
            return exists;
        }
    }
}