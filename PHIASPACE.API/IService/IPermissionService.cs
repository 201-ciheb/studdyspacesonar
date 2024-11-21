using PHIASPACE.API.Model;

namespace PHIASPACE.API.IService;

public interface IPermissionService{
    Task<TokenModel> RequestIdentityTokenAsync();
    Task<HttpResponseMessage> GetPermissions(string authToken);
}