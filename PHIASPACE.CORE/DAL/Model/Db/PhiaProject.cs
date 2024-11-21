using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class PhiaProject
    {
        public PhiaProject()
        {
            PhiaApps = new HashSet<PhiaApp>();
            PhiaUserProjects = new HashSet<PhiaUserProject>();
        }

        public int Id { get; set; }
        public string ProjectAbbrev { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public string? ProjectDescription { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public int Active { get; set; }
        public string? ProjectLogoPath { get; set; }

        public virtual ICollection<PhiaApp> PhiaApps { get; set; }
        public virtual ICollection<PhiaUserProject> PhiaUserProjects { get; set; }
    }
}
