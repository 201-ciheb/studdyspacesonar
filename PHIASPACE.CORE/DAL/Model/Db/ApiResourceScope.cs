using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class ApiResourceScope
    {
        public int Id { get; set; }
        public string Scope { get; set; } = null!;
        public int ApiResourceId { get; set; }

        public virtual ApiResource ApiResource { get; set; } = null!;
    }
}
