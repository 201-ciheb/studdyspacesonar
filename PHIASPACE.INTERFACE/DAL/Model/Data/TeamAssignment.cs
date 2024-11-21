using System;
using System.Collections.Generic;

namespace PHIASPACE.INTERFACE.DAL.Model.Data
{
    public partial class TeamAssignment
    {
        public int Id { get; set; }
        public string Team { get; set; } = null!;
        public int Number { get; set; }
        public int? Active { get; set; }
        public string? Initial { get; set; }
    }
}
