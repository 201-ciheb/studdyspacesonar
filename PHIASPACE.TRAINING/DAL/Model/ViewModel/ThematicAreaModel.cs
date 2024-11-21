using System.ComponentModel.DataAnnotations;

namespace PHIASPACE.TRAINING.DAL.Model.ViewModel
{
    public class ThematicAreaModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Thanatic Area Name")]
        public string Name { get; set; }
    }
}