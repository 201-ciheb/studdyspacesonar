using IdentityModel.Client;
using Newtonsoft.Json;
using PHIASPACE.API.IService;
using PHIASPACE.API.Model;

namespace PHIASPACE.API.Service;

public class PermissionService :IPermissionService{

    private readonly HttpClient _client;
    public PermissionService(HttpClient client)
    {
        _client = client;
    }

    public async Task<TokenModel> RequestIdentityTokenAsync()
    {
        var tokenEndpoint = "/connect/token"; 
    
        var tokenRequest = new Dictionary<string, string>
        {
            { "client_id", "PHIASAPCE.API" },  
            { "client_secret", "g7yFePKZ6R6GwQeu" }, 
            { "grant_type", "client_credentials" },
            { "scope", "PHIASAPCE.API.Read" } 
        };

        var tokenResponse = await _client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(tokenRequest));

        if (tokenResponse.IsSuccessStatusCode)
        {
            var responseContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResponseDTO = JsonConvert.DeserializeObject<TokenModel>(responseContent);
            return tokenResponseDTO;
        }

        throw new Exception("Token request failed.");
    }

    public async Task<HttpResponseMessage> GetPermissions(string authToken)
    {
        _client.SetBearerToken(authToken);
        var response = await _client.GetAsync("/permission_endpoint");

        return response;
    }
}
