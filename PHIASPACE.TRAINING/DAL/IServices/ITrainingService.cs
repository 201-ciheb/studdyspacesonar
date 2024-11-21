using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface ITrainingService
    {
        //Trainings
        TblTraining AddTraining(TblTraining training);
        TblTraining GetTraining(int id);
        IQueryable<TblTraining> GetAllTraining();
        void Update(TblTraining training);
        void Remove(TblTraining training);


        //Participants
        void AddParticipants(List<TblTrainingParticipant> participants);
        TblTrainingParticipant AddParticipant(TblTrainingParticipant participants);
        void DeleteParticipant(int trainingid, string personid);
        IQueryable<TblTrainingParticipant> GetParticipants(int trainingid);
        TblTrainingParticipant GetParticipant(int trainingid, string personid);

        //Facilitators
        void AddFacilitator(List<TblTrainingFacilitator> facilitators);
        void DeleteFacilitator(int trainingid, string personid);
        IQueryable<TblTrainingFacilitator> GetFacilitators(int trainingid);
        TblTrainingFacilitator GetFacilitator(int trainingid, string personid);
    }
}
