using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.TRAINING.DAL.Model.Db
{
    public class TblTraining
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainingId { get; set; }
        public int? CourseId { get; set; }
        public TblCourse Course { get; set; }
        public int? VenueId { get; set; }
        public TblTrainingVenue Venue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? ExpectedParticipants { get; set; }
        public int? Deleted { get; set; }
        public string DeletedBy { get; set; }
        public string Trainingstatus { get; set; }
        public string Statusmessage { get; set; }
        public virtual ICollection<TblTrainingFacilitator> TrainingFacilitators { get; set; }
        public virtual ICollection<TblTrainingParticipant> TrainingParticipants { get; set; }
    }
}
