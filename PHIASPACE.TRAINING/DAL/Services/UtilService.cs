using Microsoft.EntityFrameworkCore;
using PHIASPACE.TRAINING.DAL.IServices;
using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.Services{
    public class UtilService : IUtilService{

        private readonly PhiaSpaceTrainingContext _context;

        public UtilService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public async Task<List<TblLookup>> GetOptions(string category)
        {
            var results = await _context.Tbl_Lookup.Where(e => e.Category == category).OrderBy(e => e.OrderId).ToListAsync();
            return results;
        }
    }
}