using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class ApiScopeClaim
    {
        public int Id { get; set; }
        public int ScopeId { get; set; }
        public string Type { get; set; } = null!;

        public virtual ApiScope Scope { get; set; } = null!;
    }
}
