using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class PhiaUserPermission
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Permission { get; set; }
        public int? PermissionId { get; set; }

        public virtual PhiaPermission? PermissionNavigation { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
