using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class TraininingService : ITrainingService
    {
        private readonly PhiaSpaceTrainingContext _context;
        public TraininingService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public void AddFacilitator(List<TblTrainingFacilitator> facilitators)
        {
            throw new NotImplementedException();
        }

        public TblTrainingParticipant AddParticipant(TblTrainingParticipant participant)
        {
            _context.Tbl_TrainingParticipant.Add(participant);
            _context.SaveChanges();
            return participant;
        }

        public void AddParticipants(List<TblTrainingParticipant> participants)
        {
            throw new NotImplementedException();
        }

        public TblTraining AddTraining(TblTraining training)
        {
            //_context.Tbl_Training.Add(training);
           //_context.SaveChanges();
            return training;
        }

        public void DeleteFacilitator(int trainingid, string personid)
        {
            throw new NotImplementedException();
        }

        public void DeleteParticipant(int trainingid, string personid)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblTraining> GetAllTraining()
        {
            return _context.Tbl_Training.AsQueryable();
        }

        public TblTrainingFacilitator GetFacilitator(int trainingid, string personid)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblTrainingFacilitator> GetFacilitators(int trainingid)
        {
            throw new NotImplementedException();
        }

        public TblTrainingParticipant GetParticipant(int trainingid, string personid)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblTrainingParticipant> GetParticipants(int trainingid)
        {
            throw new NotImplementedException();
        }

        public TblTraining GetTraining(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TblTraining training)
        {
            throw new NotImplementedException();
        }

        public void Update(TblTraining training)
        {
            throw new NotImplementedException();
        }
    }
}
