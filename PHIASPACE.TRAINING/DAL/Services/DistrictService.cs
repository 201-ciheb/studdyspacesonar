using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly PhiaSpaceTrainingContext _context;
        public DistrictService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblDistrict AddDistrict(TblDistrict district)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblDistrict>> GetActiveDistricts()
        {
            return await _context.Tbl_District.Where(m => m.Deleted != 1).ToListAsync();
        }

        public void RemoveDistrict(TblDistrict district)
        {
            throw new NotImplementedException();
        }

        public IList<TblDistrict> SearchDistricts(string term, int provinceId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDistrict(TblDistrict province)
        {
            throw new NotImplementedException();
        }

        public async Task<TblDistrict> GetDistrict(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblDistrict>> GetDistricts()
        {
            return await _context.Tbl_District.ToListAsync();
        }
    }
}
