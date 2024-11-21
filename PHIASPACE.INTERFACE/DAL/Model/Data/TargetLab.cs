using System;
using System.Collections.Generic;

namespace PHIASPACE.INTERFACE.DAL.Model.Data
{
    public partial class TargetLab
    {
        public string Kmfl { get; set; } = null!;
        public string? LabName { get; set; }
        public string? County { get; set; }
        public string? Region { get; set; }
        public string? Team { get; set; }
        public int? AssessmentStatus { get; set; }
        public string? GeoLat { get; set; }
        public string? GeoLon { get; set; }
    }
}
