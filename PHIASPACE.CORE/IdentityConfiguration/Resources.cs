using IdentityServer4.Models;

namespace PHIASPACE.CORE.IdentityConfiguration{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                // new ApiResource
                // {
                //     Name = "PHIASAPCE.API",
                //     DisplayName = "PHIASAPCE API",
                //     Description = "Allow the application to access PHIA API on your behalf",
                //     Scopes = new List<string> { "PHIASAPCE.API.Read", "PHIASAPCE.API.Write"},
                //     ApiSecrets = new List<Secret> {new Secret("g7yFePKZ6R6GwQeu".Sha256())},
                //     UserClaims = new List<string> {"role"}
                // }
                new ApiResource("phiaapi", "PHIASPACE API")
                {
                    Scopes = new List<string> { "phiaapi" }
                }
            
            };
        }
    }
}