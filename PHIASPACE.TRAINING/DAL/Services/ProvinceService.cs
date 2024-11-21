using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly PhiaSpaceTrainingContext _context;
        public ProvinceService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblProvince AddProvince(TblProvince province)
        {
            throw new NotImplementedException();
        }

        public async Task<TblProvince> GetProvince(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblProvince>> GetProvinces()
        {
            var provinces = await _context.Tbl_Province.ToListAsync();
            return provinces;
        }

        public async Task<List<TblProvince>> GetActiveProvinces()
        {
            var provinces = await _context.Tbl_Province.Where(m => m.Deleted != 1).ToListAsync();
            return provinces;
        }

        public void RemoveProvince(TblProvince province)
        {
            throw new NotImplementedException();
        }

        public void UpdateProvince(TblProvince province)
        {
            throw new NotImplementedException();
        }
    }
}
