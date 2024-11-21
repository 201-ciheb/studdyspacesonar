using PHIASPACE.INCIDENCE.DAL.Model;

namespace PHIASPACE.INCIDENCE.DAL.IService
{
    public interface IZoneService
    {
        Task<List<AimsCounty>> GetCountiesAsync();
    }
}
