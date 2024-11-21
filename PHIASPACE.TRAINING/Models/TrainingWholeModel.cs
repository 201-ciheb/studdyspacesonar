using PHIASPACE.TRAINING.DAL.Model.ViewModel;

namespace PHIASPACE.TRAINING.Models{
    public class TrainingWholeModel{
        public TrainingModel TrainingDetail { get; set; }
        public List<FacilitatorModel> Facilitators { get; set; }
        public List<ParticipantModel> Participants { get; set; }
        public List<VenueModel> Venues { get; set; }
    }
}