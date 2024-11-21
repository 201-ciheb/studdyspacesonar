using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices{
    public interface IUtilService{
        Task<List<TblLookup>> GetOptions(string category);
    }
}