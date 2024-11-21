using System;
using System.Collections.Generic;

namespace PHIASPACE.INTERFACE.DAL.Model.Data
{
    public partial class SatLabAssessment
    {
        public int FormId { get; set; }
        public string? FormUuid { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Interviewer { get; set; }
        public string? Team { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public string? Country { get; set; }
        public string? SubCounty { get; set; }
        public string? Facility { get; set; }
        public string? Kmfl { get; set; }
        public double? Geocord1 { get; set; }
        public double? Geocord2 { get; set; }
        public double? Score { get; set; }
    }
}
