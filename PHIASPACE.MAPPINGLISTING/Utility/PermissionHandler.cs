using Microsoft.AspNetCore.Authorization;
using PHIASPACE.MAPPINGLISTING.Models;

namespace PHIASPACE.TRAINING.Utility{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.HasClaim("permission", requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}