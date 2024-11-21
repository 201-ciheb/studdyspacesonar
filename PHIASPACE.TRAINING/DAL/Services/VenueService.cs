using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class VenueService : IVenueService
    {
        private readonly PhiaSpaceTrainingContext _context;

        public VenueService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblTrainingVenue AddVenue(TblTrainingVenue venue)
        {
            _context.Add(venue);
            _context.SaveChanges();
            return venue;
        }

        public TblTrainingVenue GetVenue(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblTrainingVenue> GetVenueList(List<int> venueIds)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblTrainingVenue> GetVenues()
        {
            return _context.Tbl_Venue.AsQueryable();
        }

        public async Task<List<TblTrainingVenue>> GetActiveVenues()
        {
            return await _context.Tbl_Venue.Where(m => m.Deleted != 1).ToListAsync();
        }

        public void Remove(TblTrainingVenue trainingVenue)
        {
            throw new NotImplementedException();
        }

        public void Update(TblTrainingVenue venue)
        {
            throw new NotImplementedException();
        }
    }
}
