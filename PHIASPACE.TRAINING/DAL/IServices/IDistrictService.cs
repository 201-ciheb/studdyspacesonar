using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IDistrictService
    {
        Task<List<TblDistrict>> GetDistricts();
        Task<List<TblDistrict>> GetActiveDistricts();
        TblDistrict AddDistrict(TblDistrict district);
        Task<TblDistrict> GetDistrict(int id);
        void RemoveDistrict(TblDistrict district);
        void UpdateDistrict(TblDistrict province);
        IList<TblDistrict> SearchDistricts(string term, int provinceId);
    }
}
