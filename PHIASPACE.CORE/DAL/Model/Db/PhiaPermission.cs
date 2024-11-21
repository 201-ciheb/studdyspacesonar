using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class PhiaPermission
    {
        public PhiaPermission()
        {
            PhiaUserPermissions = new HashSet<PhiaUserPermission>();
        }

        public int Id { get; set; }
        public string PermissionModule { get; set; } = null!;
        public string? Permission { get; set; }
        public string? PermissionType { get; set; }
        public string? PermissionClient { get; set; }

        public virtual ICollection<PhiaUserPermission> PhiaUserPermissions { get; set; }
    }
}
