using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.Models;

namespace PHIASPACE.CORE.Utility{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        public PermissionHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            bool hasPermission = _permissionService.CheckUserPermission(context.User.FindFirstValue("sub"), requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            // hasPermission = _permissionService.CheckPermissionForRole(context.User, requirement.Permission);
            // if (hasPermission)
            // {
            //     context.Succeed(requirement);
            //     return Task.CompletedTask;
            // }

            return Task.CompletedTask;
        }
    }
}