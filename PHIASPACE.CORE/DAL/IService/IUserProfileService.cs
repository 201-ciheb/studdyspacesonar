using IdentityServer4.Models;

namespace PHIASPACE.CORE.DAL.IService{
    public interface IUserProfileService{
        // Task GetProfileDataAsync(ProfileDataRequestContext context);
        // Task IsActiveAsync(IsActiveContext context);
        Task GetProfileDataAsync(ProfileDataRequestContext context);
        Task IsActiveAsync(IsActiveContext context);
    }
}