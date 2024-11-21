using System.ComponentModel.DataAnnotations;

namespace PHIASPACE.TRAINING.DAL.Model.ViewModel
{
    public class ParticipantModel
    {
        [Required(ErrorMessage = "Please select participant")]
        [Display(Name = "Participant")]
        public string IdentificationCode { get; set; }
        [Required(ErrorMessage = "Please select training")]
        [Display(Name = "Training")]
        public int TrainingId { get; set; }
        [Required(ErrorMessage = "Please enter pre-test score")]
        [Display(Name = "Pre-test score")]
        public double? PreTestScore { get; set; }
    }
}
