using PHIASPACE.RTDMS.DAL.Model;

namespace PHIASPACE.RTDMS.DAL.IService;

public interface IRecordService{
    Task<List<AimsRecordSet>> GetAimsRecordSetAsync();
    Task<List<AimsValueSet>> GetAimsValueSetAsync();
}