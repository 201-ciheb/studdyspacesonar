using Microsoft.EntityFrameworkCore;
using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;


namespace PHIASPACE.TRAINING.DAL.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly PhiaSpaceTrainingContext _context;

        public FacilityService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblFacility AddFacility(TblFacility facility)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblFacility> GetFacilities()
        {
            return _context.Tbl_Facility
                 .Include(p => p.Province)
                 .Include(d => d.District)
                 .Include(t => t.Type)
                 .Include(l => l.Level)
                 .AsQueryable();
        }

        public IQueryable<TblFacility> GetFacilitiesByRole(string role, int provinceid, int districtid, int facilityid)
        {
            throw new NotImplementedException();
        }

        public TblFacility GetFacility(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveFacility(TblFacility facility)
        {
            throw new NotImplementedException();
        }

        public IList<TblFacility> SearchFacilities(string term, int districtid)
        {
            throw new NotImplementedException();
        }

        public IList<TblFacility> SearchInstitutions(string term)
        {
            throw new NotImplementedException();
        }

        public void UpdateFacility(TblFacility facility)
        {
            throw new NotImplementedException();
        }
    }
}
