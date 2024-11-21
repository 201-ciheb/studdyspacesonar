using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblTrainingFacilitator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacilitatorId { get; set; }
        public string IdentificationCode { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string RegisteredBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int TrainingId { get; set; }
        public int? Deleted { get; set; }

        public virtual TblPersons Person { get; set; }
        public virtual TblTraining Training { get; set; }
    }
}
