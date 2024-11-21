using System.ComponentModel.DataAnnotations;

namespace PHIASPACE.TRAINING.DAL.Model.ViewModel
{
    public class FacilitatorModel
    {
        [Required(ErrorMessage = "Please select facilitator")]
        [Display(Name = "Facilitator")]
        public string IdentificationCode { get; set; }
        [Required(ErrorMessage = "Please select training")]
        [Display(Name = "Training")]
        public int TrainingId { get; set; }
    }
}
