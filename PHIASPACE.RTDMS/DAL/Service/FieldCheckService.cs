using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;

namespace PHIASPACE.RTDMS.DAL.Service;

public class FieldCheckService : IFieldCheckService{
    private readonly IAimsDbProvider _aimsDbProvider;
    private readonly IConfiguration _configuration;

    public FieldCheckService(IAimsDbProvider aimsDbProvider, IConfiguration configuration)
    {
        _aimsDbProvider = aimsDbProvider;
        _configuration = configuration;
    }

    public async Task SaveFieldCheckData(FieldCheck fieldCheck){
        await _aimsDbProvider.AddAsync(fieldCheck);
    }
}
