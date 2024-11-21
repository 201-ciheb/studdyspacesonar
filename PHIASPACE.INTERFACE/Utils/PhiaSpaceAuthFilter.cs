using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;

namespace PHIASPACE.INTERFACE.Utils;

public class PhiaSpaceAuthFilter : IDashboardAuthorizationFilter
{
    //private readonly IAuthorizationService _authorizationService;

    // public PhiaSpaceAuthFilter(IAuthorizationService authorizationService)
    // {
    //     _authorizationService = authorizationService;
    // }

    public bool Authorize(DashboardContext context)
    {
        var user = context.GetHttpContext().User;

        if(user.Identity.IsAuthenticated){
            //var result = _authorizationService.AuthorizeAsync(user, "YourHangfirePolicy").Result;
            //return result.Succeeded;
            return true;
        }
        else{
            return false;
        }
    }
}