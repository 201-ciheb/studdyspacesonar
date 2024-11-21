using Microsoft.EntityFrameworkCore;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.IdentityConfiguration;

namespace PHIASPACE.CORE.DAL.Service
{
    public class PermissionService :IPermissionService {
        private readonly PhiaSpaceContext _context;

        public PermissionService(PhiaSpaceContext context)
        {
            _context=context;
        }

        public async Task<int> AddPermission(PhiaPermission permission)
        {
            _context.PhiaPermissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission.Id;
        }

        public async Task<PhiaPermission> GetPermision(int id)
        {
            var permission = await _context.PhiaPermissions.FirstOrDefaultAsync(m => m.Id == id);
            return permission;
        }

        public async Task<List<PhiaPermission>> GetPermissions()
        {
            return await _context.PhiaPermissions.ToListAsync();
        }

        public async Task<int> UpdatePhiaPermission(PhiaPermission permission)
        {
            var existing = await _context.PhiaPermissions.FirstOrDefaultAsync(m => m.Id == permission.Id);
            if(existing != null){
                _context.Entry(existing).CurrentValues.SetValues(permission);
                _context.Entry(existing).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return permission.Id;
        }

        public async Task<List<string>> GetUserPermissions(string user_id){
            var permissions = await _context.PhiaUserPermissions.Where(m => m.UserId == user_id).ToListAsync();
            if(permissions.Any())
            {
                return permissions.Select(m => m.Permission).ToList();
            }
            return new List<string>();
        }

        public async Task<List<string>> GetUserPermissionsForClient(string user_id, string clientId){
            var permissions = await _context.PhiaUserPermissions.Include(m => m.PermissionNavigation)
            .Where(m => m.UserId == user_id && m.PermissionNavigation.PermissionClient == clientId).ToListAsync();
            if(permissions.Any())
            {
                return permissions.Select(m => m.Permission).ToList();
            }
            return new List<string>();
        }

        public void AddUserPermission(List<string> permission, string userId){
            var existing = _context.PhiaUserPermissions.Where(m => m.UserId == userId);
            _context.RemoveRange(existing);
            _context.SaveChanges();
            //get the permission id with the module
            if(permission.Count() > 0) {
                
                var db_permission = _context.PhiaPermissions.ToList();
                var new_permissions = new List<PhiaUserPermission>();
                foreach (var m in permission)
                {
                    var permissionModule = m.Replace("PERMISSION.","").Replace(".CREATE", "").Replace(".EDIT","").Replace(".VIEW", "").Replace(".DELETE","");
                    var db_perm = db_permission.FirstOrDefault(m => m.Permission == permissionModule);
                    new_permissions.Add(new PhiaUserPermission
                    {
                        UserId = userId, Permission=m,
                        PermissionId = db_perm.Id
                    });
                }
                if(new_permissions.Count() > 0){
                    _context.AddRange(new_permissions);
                    _context.SaveChanges();
                } 
            }                           
        }

        public bool CheckUserPermission(string user_id, string permission)
        {
            return _context.PhiaUserPermissions.Any(m => m.UserId == user_id && m.Permission == permission);
        }

        public async Task<PhiaPermission> CheckPermisionExist(string permissionModule)
        {
            var permission = await _context.PhiaPermissions.FirstOrDefaultAsync(m => m.PermissionModule == permissionModule);
            return permission;
        }

        public async Task RemovePhiaPermission(PhiaPermission permission)
        {
            var existing = await _context.PhiaPermissions.FirstOrDefaultAsync(m => m.Id == permission.Id);
            if(existing != null){
                _context.Entry(existing).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Client>> GetOauthClients(){
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }
    }
}