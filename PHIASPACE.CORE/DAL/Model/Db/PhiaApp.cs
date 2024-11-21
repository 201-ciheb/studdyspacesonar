using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class PhiaApp
    {
        public int Id { get; set; }
        public string AppName { get; set; } = null!;
        public string? AppDescription { get; set; }
        public string AppUrl { get; set; } = null!;
        public string AppStatus { get; set; } = null!;
        public string? AppIconPath { get; set; }
        public int? AppOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int ProjectId { get; set; }
        public int Deleted { get; set; }

        public virtual PhiaProject Project { get; set; } = null!;
    }
}
