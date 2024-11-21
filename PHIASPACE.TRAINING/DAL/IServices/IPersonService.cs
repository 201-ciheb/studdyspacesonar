using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IPersonService
    {
        TblPersons AddPerson(TblPersons person);
        TblPersons GetPerson(string personId);
        IQueryable<TblPersons> GetAllPersons();
        IQueryable<TblPersons> SearchPerson(string? term);
        void Update(TblPersons person, string old);
        IQueryable<TblPersons> GetPersonsByRole(string role, int province, int district, int facility);
        //IQueryable<TblPersons> GetPersonsList(List<int> personIds);
        IQueryable<TblPersons> SearchByPersonID(String key);
        IQueryable<TblPersons> FilterPersons(int provinceid, string districtid, int facilityid, int? professionid);
        void Remove(TblPersons person);
        IQueryable<TblFacilityTransfers> GetTransfers(string personid);
    }
}
