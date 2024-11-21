using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IProvinceService
    {
        Task<List<TblProvince>> GetProvinces();
        Task<List<TblProvince>> GetActiveProvinces();
        TblProvince AddProvince(TblProvince province);
        Task<TblProvince> GetProvince(int id);
        void UpdateProvince(TblProvince province);
        void RemoveProvince(TblProvince province);
    }
}
