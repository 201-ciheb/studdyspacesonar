using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PHIASPACE.CORE.DAL.Service{
    public class UserProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<PhiaSpaceUser> _claimsFactory;
        private readonly UserManager<PhiaSpaceUser> _userManager;
        private readonly IPermissionService _permissionService;
        private readonly IProjectService _projectService;

        public UserProfileService(
            IUserClaimsPrincipalFactory<PhiaSpaceUser> claimsFactory,
            UserManager<PhiaSpaceUser> userManager,IPermissionService permissionService, IProjectService projectService)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
            _permissionService=permissionService;
            _projectService = projectService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var clientId = context.Client.ClientId;
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = await _claimsFactory.CreateAsync(user);

            context.IssuedClaims.AddRange(claims.Claims);
            //get the permissions
            var userPermissions = await _permissionService.GetUserPermissionsForClient(user.Id, clientId);

            if (userPermissions.Count() > 0)
            {
                foreach (var permission in userPermissions)
                {
                    context.IssuedClaims.Add(new Claim("permission", permission));
                }
            }

            //get the projects
            var userProjects = await _projectService.GetUserProjects(user.Id);
            if (userProjects.Count() > 0)
            {
                foreach (var project in userProjects)
                {
                    context.IssuedClaims.Add(new Claim("projects", project.Project.ProjectAbbrev));
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null);
        }
    }
}