using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class ProfessionService : IProfessionService
    {
        private readonly PhiaSpaceTrainingContext _context;
        public ProfessionService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblProfession AddProffession(TblProfession proffession)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblProfession> GetAllProffessions()
        {
            throw new NotImplementedException();
        }

        public TblProfession GetProffession(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TblProfession profession)
        {
            throw new NotImplementedException();
        }

        public IList<TblProfession> SearchProfessions(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(TblProfession proffession)
        {
            throw new NotImplementedException();
        }
    }
}
