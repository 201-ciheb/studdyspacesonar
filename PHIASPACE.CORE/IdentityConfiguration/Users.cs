using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace PHIASPACE.CORE.IdentityConfiguration{
    public class Users
    {
        public static List<TestUser> Get() => new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "56892347",
                    Username = "procoder",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "support@procodeguide.com"),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "https://procodeguide.com")
                    }
                }
            };
    }
}