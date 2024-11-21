using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class PhiaUserProject
    {
        public string UserId { get; set; } = null!;
        public int ProjectId { get; set; }
        public int Id { get; set; }
        public int? Default { get; set; }

        public virtual PhiaProject Project { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
