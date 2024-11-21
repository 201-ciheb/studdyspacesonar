using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.RTDMS.DAL.Model
{
    [Table("aims_issue")]
    public partial class AimsIssue
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("issue_type_id")]
        public int IssueTypeId { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("title")]
        public string Title { get; set; } = null!;

        [Column("issue_content")]
        public string IssueContent { get; set; } = null!;

        [Column("household_id")]
        public int? HouseholdId { get; set; }

        [Column("team_id")]
        public int? TeamId { get; set; }

        [Column("county_id")]
        public int? CountyId { get; set; }

        [Column("lab_id")]
        public Guid? LabId { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("opened_by")]
        public string OpenedBy { get; set; } = null!;

        [Column("ea_id")]
        public int? EaId { get; set; }

        [Column("impact")]
        public string? Impact { get; set; }

        [Column("resolution")]
        public string? Resolution { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties

        public virtual ICollection<AimsDiscussion> AimsDiscussions { get; set; } = new List<AimsDiscussion>();

        public virtual ICollection<AimsLnkIssuePerson> AimsLnkIssuePeople { get; set; } = new List<AimsLnkIssuePerson>();

        [ForeignKey("EaId")]
        public virtual AimsEa? Ea { get; set; }

        [ForeignKey("IssueTypeId")]
        public virtual RtdmsIssueType IssueType { get; set; } = null!;

        [ForeignKey("LabId")]
        public virtual LbLab? Lab { get; set; }

        [ForeignKey("Priority")]
        public virtual AimsLookup PriorityNavigation { get; set; } = null!;

        [ForeignKey("CountyId")]
        public virtual AimsCounty? County { get; set; }

        [ForeignKey("StatusId")]
        public virtual RtdmsIssueStatus Status { get; set; } = null!;
    }
}