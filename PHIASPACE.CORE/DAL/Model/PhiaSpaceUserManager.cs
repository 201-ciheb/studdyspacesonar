using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PHIASPACE.CORE.DAL.Model{
    public class PhiaSpaceUserManager : UserManager<PhiaSpaceUser>
    {
        public PhiaSpaceUserManager(IUserStore<PhiaSpaceUser> store, IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<PhiaSpaceUser> passwordHasher, IEnumerable<IUserValidator<PhiaSpaceUser>> userValidators, 
        IEnumerable<IPasswordValidator<PhiaSpaceUser>> passwordValidators, ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<PhiaSpaceUser>> logger) 
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public async Task<string> GetAddressAsync(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.Address;
        }

        public async Task<string> GetCityAsync(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.City;
        }

        public async Task<string> GetAddressLevel1Async(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.AddressLevel1;
        }
        
        public async Task<string> GetAddressLevel2Async(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.AddressLevel2;
        }

        public async Task<string> GetFullNameAsync(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.FullName;
        }

        public async Task<string> GetQualificationAsync(ClaimsPrincipal principal)
        {
            var user = await GetUserAsync(principal);
            return user.Qualification;
        }

        public async Task<string> GetGenderAsync(ClaimsPrincipal principal){
            var user = await GetUserAsync(principal);
            return user.Gender;
        }

        public async Task<IdentityResult> SetUserVariablesAsync(PhiaSpaceUser user,
            string address = null, string fullname = null, string address_level_1 = null, string city = null,
            string qualification = null, string gender=null)
        {
            user.Address = address != null ? address : user.Address;
            user.FullName = fullname != null ? fullname : user.FullName;
            user.AddressLevel1 = address_level_1 != null ? address_level_1 : user.AddressLevel1;
            user.City = city != null ? city : user.City;
            user.Qualification = qualification != null ? qualification : user.Qualification;
            user.Gender = gender != null ? gender:user.Gender;

            IdentityResult result = await UpdateAsync(user);
            
            return result;
        }

        public virtual Task<PhiaSpaceUser> FindByPhoneNumberAsync(string PhoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Users.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber, cancellationToken);
        }
    }
}