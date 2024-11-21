using IdentityServer4.Models;

namespace PHIASPACE.CORE.IdentityConfiguration{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                // new ApiScope("PHIASAPCE.API.Read", "Read Access to API Application"),
                // new ApiScope("PHIASAPCE.API.Write", "Write Access to API Application"),
                // new ApiScope("PHIASAPCE.TARINING.Read", "Read Access to API Application"),
                // new ApiScope("PHIASAPCE.TARINING.Write", "Write Access to API Application"),
                new ApiScope("phiaapi", "PHIASPACE API")
            };
        }
    }
}