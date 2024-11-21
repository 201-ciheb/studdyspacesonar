using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int? CertificateType { get; set; }
        public int? CmeUnits { get; set; }
        public int? ValidityPeriodMonths { get; set; }
        public int? ThematicAreaId { get; set; }
        public TblThematicArea ThematicArea { get; set; }
        public int? DurationDays { get; set; }
        public double? PassScore { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? Deleted { get; set; }
    }
}
