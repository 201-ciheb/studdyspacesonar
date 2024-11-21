using System.ComponentModel.DataAnnotations;

namespace PHIASPACE.TRAINING.DAL.Model.ViewModel
{
    public class TrainingModel
    {
        public int? TrainingId { get; set; }
        
        [Required(ErrorMessage = "Please enter a valid training name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select course")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please select venue")]
        [Display(Name = "Venue")]
        public int? VenueId { get; set; }

        [Required(ErrorMessage = "Please choose start date")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        
        [Required(ErrorMessage = "Please choose end date")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Funding Type")]
        public string FundingType { get; set; }
    }
}
