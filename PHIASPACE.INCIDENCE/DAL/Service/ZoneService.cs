using Microsoft.EntityFrameworkCore;
using PHIASPACE.INCIDENCE.DAL.IService;
using PHIASPACE.INCIDENCE.DAL.Model;
using PHIASPACE.INCIDENCE.DAL.MysqlDbService;

namespace PHIASPACE.INCIDENCE.DAL.Service
{
    public class ZoneService : IZoneService
    {
        private readonly AimsMysqlDbContext _dbContext;

        public ZoneService(AimsMysqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<AimsCounty>> GetCountiesAsync()
        {
            return await _dbContext.AimsCounties.ToListAsync();
        }
    }
}
