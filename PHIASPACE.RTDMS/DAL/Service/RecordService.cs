using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;

namespace PHIASPACE.RTDMS.DAL.Service;

class RecordService : IRecordService{
    private readonly IAimsDbProvider _dbProvider;

    public RecordService(IAimsDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<List<AimsRecordSet>> GetAimsRecordSetAsync(){
        var aimsRecordSets = await _dbProvider.GetAllAsync<AimsRecordSet>();
        return aimsRecordSets;
    }
    
    public async Task<List<AimsValueSet>> GetAimsValueSetAsync(){
        var aimsValueSet = await _dbProvider.GetAllAsync<AimsValueSet>();
        return aimsValueSet;
    }
}