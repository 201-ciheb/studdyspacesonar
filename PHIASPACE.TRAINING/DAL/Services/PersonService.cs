using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class PersonService : IPersonService
    {
        private readonly PhiaSpaceTrainingContext _context;

        public PersonService(PhiaSpaceTrainingContext context) {
            _context = context;
        }

        public TblPersons AddPerson(TblPersons person)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblPersons> FilterPersons(int provinceid, string districtid, int facilityid, int? professionid)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblPersons> GetAllPersons()
        {
            return _context.Tbl_Persons.AsQueryable();
        }

        public TblPersons GetPerson(string personId)
        {
            return _context.Tbl_Persons.Find(personId);
        }

        public IEnumerable<TblPersons> GetPersons()
        {
            return _context.Tbl_Persons.ToList();
        }

        public IQueryable<TblPersons> GetPersonsByRole(string role, int province, int district, int facility)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblFacilityTransfers> GetTransfers(string personid)
        {
            throw new NotImplementedException();
        }

        public void Remove(TblPersons person)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblPersons> SearchByPersonID(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblPersons> SearchPerson(string? term)
        {
            if (term == null)
            {
                return _context.Tbl_Persons.AsQueryable();
            }
            else {
                return _context.Tbl_Persons
                    .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term) || p.IdentificationCode.Contains(term) || p.OtherName.Contains(term))
                    .AsQueryable();
            }
        }

        public void Update(TblPersons person, string old)
        {
            throw new NotImplementedException();
        }
    }
}
