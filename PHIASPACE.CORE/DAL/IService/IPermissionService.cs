using PHIASPACE.CORE.DAL.Model.DB;

namespace PHIASPACE.CORE.DAL.IService{
    public interface IPermissionService
    {
        Task<int> AddPermission(PhiaPermission permission);
        Task<int> UpdatePhiaPermission(PhiaPermission permission);
        Task<List<PhiaPermission>> GetPermissions();
        Task<PhiaPermission> GetPermision(int id);
        Task<List<string>> GetUserPermissions(string user_id);
        void AddUserPermission(List<string> permission, string userId);
        bool CheckUserPermission(string user_id, string permission);
        Task<PhiaPermission> CheckPermisionExist(string permissionModule);
        Task RemovePhiaPermission(PhiaPermission permission);
        Task<List<string>> GetUserPermissionsForClient(string user_id, string clientId);
        Task<List<Client>> GetOauthClients();
    }
}