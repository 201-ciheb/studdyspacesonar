using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Model.DB;

namespace PHIASPACE.CORE.Models{
    public class UserDetails{
        public PhiaSpaceUser PhiaSpaceUser { get; set; }
        public List<PhiaUserProject> PhiaUserProject { get; set; }
        public IList<string> Roles { get; set; }
        public List<PermissionModel> Permission { get; set; }
    }
}