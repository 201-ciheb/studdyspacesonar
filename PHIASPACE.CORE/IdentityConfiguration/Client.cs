using IdentityServer4;
using IdentityServer4.Models;

namespace PHIASPACE.CORE.IdentityConfiguration{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                // new Client
                // {
                //     ClientId = "PHIASAPCE.API",
                //     ClientName = "PHIA API Application",
                //     AllowedGrantTypes = GrantTypes.ClientCredentials,
                //     ClientSecrets = new List<Secret> {new Secret("g7yFePKZ6R6GwQeu".Sha256())},
                //     AllowedScopes = new List<string> {"PHIASAPCE.API.Read"}
                // },
                // new Client
                // {
                //     ClientId = "PHIASAPCE.TRAINING",
                //     ClientName = "PHIA Training Application",
                //     ClientSecrets = new List<Secret> {new Secret("g7yFePKZ6R6GwQeu".Sha256())},
        
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RedirectUris = new List<string> {"https://localhost:7056/Home/Index"},
                //     PostLogoutRedirectUris = { "https://localhost:7056/Home/Index" },
                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         IdentityServerConstants.StandardScopes.Email,
                //         "PHIASAPCE.TRAINING.Read",
                //         "PHIASAPCE.TRAINING.Write"
                //     },
                //     AlwaysIncludeUserClaimsInIdToken = true
                // },
                // new Client
                // {
                //     ClientId = "PHIASAPCE.TICKETING",
                //     ClientName = "PHIA Ticketing Application",
                //     ClientSecrets = new List<Secret> {new Secret("g7yFePKZ6R6GwQeu".Sha256())},
        
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RedirectUris = new List<string> {"https://localhost:7191/signin-oidc"},
                //     PostLogoutRedirectUris = { "https://localhost:7191/Home/Index" },
                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         IdentityServerConstants.StandardScopes.Email,
                //         "PHIASAPCE.TICKETING.Read",
                //         "PHIASAPCE.TICKETING.Write"
                //     },
                //     AlwaysIncludeUserClaimsInIdToken = true
                // },
                // new Client
                // {
                //     ClientId = "PHIASPACE.MAPPINGLISTING",
                //     ClientName = "Mapping and listing application",
                //     ClientSecrets = new List<Secret> {new Secret("U0c7ul3n16GgTOZze5x".Sha256())},
        
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RedirectUris = new List<string> {"https://localhost:7285/signin-oidc"},
                //     PostLogoutRedirectUris = { "https://localhost:7285/Home/Index" },
                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         IdentityServerConstants.StandardScopes.Email
                //     },
                //     AlwaysIncludeUserClaimsInIdToken = true
                // },
                // new Client
                // {
                //     ClientId = "DNAS",
                //     ClientName = "Diagnostic Network Adaptive System",
                //     ClientSecrets = new List<Secret> {new Secret("U0c7ul3n16GgTOZze5x".Sha256())},
        
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RedirectUris = new List<string> {"https://localhost:7001/signin-oidc"},
                //     PostLogoutRedirectUris = { "https://localhost:7001/Home/Index" },
                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         IdentityServerConstants.StandardScopes.Email
                //     },
                //     AlwaysIncludeUserClaimsInIdToken = true
                // }
                // new Client {
                //     ClientId = "PHIASPACE.RTDMS",
                //     ClientName = "RTDMS for PHIA",
                //     AllowedGrantTypes = GrantTypes.Code,
                //     RequireClientSecret = false,
                //     RedirectUris = { "http://localhost:44413/index.html" },
                //     PostLogoutRedirectUris = { "http://localhost:44413/index.html" },
                //     AllowedScopes = { "openid", "profile", "api1" },
                //     AllowAccessTokensViaBrowser = true,
                //     RequirePkce = true,
                //     RequireConsent = false
                // }
                new Client {
                     ClientId = "PHIASPACE.RTDMSS",
                     ClientName = "RTDMS for PHIA",
                     AllowedGrantTypes = GrantTypes.Code,
                     RequireClientSecret = false,
                     RedirectUris = { "http://localhost:4200/index.html" },
                     PostLogoutRedirectUris = { "http://localhost:4200/index.html" },
                     AllowedScopes = { "openid", "profile", "phiaapi" },
                     AllowAccessTokensViaBrowser = true,
                     RequirePkce = true,
                     RequireConsent = false
                 }
            };
        }
    }
}