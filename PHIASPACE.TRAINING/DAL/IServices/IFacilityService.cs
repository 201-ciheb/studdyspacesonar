using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IFacilityService
    {
        IQueryable<TblFacility> GetFacilities();
        IList<TblFacility> SearchFacilities(string term, int districtid);
        public IList<TblFacility> SearchInstitutions(string term);
        IQueryable<TblFacility> GetFacilitiesByRole(string role, int provinceid, int districtid, int facilityid);
        TblFacility AddFacility(TblFacility facility);
        TblFacility GetFacility(int id);
        void RemoveFacility(TblFacility facility);
        void UpdateFacility(TblFacility facility);
    }
}
