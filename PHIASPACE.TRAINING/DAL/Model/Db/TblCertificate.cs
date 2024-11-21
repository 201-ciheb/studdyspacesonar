using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblCertificate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CertificateId { get; set; }
        public int CourseId { get; set; }
        public TblCourse Course { get; set; }
        public int TrainingId { get; set; }
        public TblTraining Training { get; set; }
        public string IdentificationCode { get; set; }
        public TblPersons Person { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Deleted { get; set; }
        public string CertStatus { get; set; }
    }
}
