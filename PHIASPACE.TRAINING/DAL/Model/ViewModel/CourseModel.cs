using System.ComponentModel.DataAnnotations;

namespace PHIASPACE.TRAINING.DAL.Model.ViewModel
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        //[Required]
        [StringLength(60)]
        [Display(Name = "Course description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Course certificate type")]
        public int? CertificateType { get; set; }
        //[Required]
        [Display(Name = "Course CME units")]
        public int? CmeUnits { get; set; }
        
        [Required]
        [Range(1, 120, ErrorMessage = "Enter number between 0 to 120")]
        [Display(Name = "Validity Period")]
        public int? ValidityPeriod { get; set; }
        
        //[Required]
        [Display(Name = "Course Thematic Area")]
        public int? ThematicAreaId { get; set; }
        
        [Required]
        [Range(1, 365, ErrorMessage = "Enter number between 0 to 365")]
        [Display(Name = "Course Duration Required")]
        public int? DurationDays { get; set; }
        
        [Range(1.0, 100.0, ErrorMessage = "Enter number between 0 to 100")]
        [Display(Name = "Pass Score")]
        public double? PassScore { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? Deleted { get; set; }
    }
}
