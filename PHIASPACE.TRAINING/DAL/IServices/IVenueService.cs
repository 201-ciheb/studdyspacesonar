using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IVenueService
    {
        TblTrainingVenue AddVenue(TblTrainingVenue venue);
        TblTrainingVenue GetVenue(int id);
        IQueryable<TblTrainingVenue> GetVenues();
        Task<List<TblTrainingVenue>> GetActiveVenues();
        void Remove(TblTrainingVenue trainingVenue);
        IQueryable<TblTrainingVenue> GetVenueList(List<int> venueIds);
        void Update(TblTrainingVenue venue);
    }
}
