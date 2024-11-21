using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IProfessionService
    {
        TblProfession AddProffession(TblProfession proffession);
        TblProfession GetProffession(int id);
        IQueryable<TblProfession> GetAllProffessions();
        IList<TblProfession> SearchProfessions(string term);
        void Update(TblProfession proffession);
        void Remove(TblProfession profession);
    }
}
