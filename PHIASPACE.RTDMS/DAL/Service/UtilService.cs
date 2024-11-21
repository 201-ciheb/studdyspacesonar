using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model.Custom;

namespace PHIASPACE.RTDMS.DAL.Service;

public class UtilService : IUtilService {
    private readonly IAimsDbProvider _dbProvider;
    private readonly IConfiguration _configuration;

    public UtilService(IAimsDbProvider dbProvider, IConfiguration configuration){
        _dbProvider = dbProvider;
        _configuration = configuration;
    }

    public async Task<List<DuplicatePtId>> ListDuplicatePtidsEA(int ea_id){
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            $"Exec [monitor].[sp_get_duplicate_ptid] @cluster_number = {ea_id}" :
            $"CALL sp_get_duplicate_ptid({ea_id})";
        var duplicate_ptid = await _dbProvider.ExecuteQueryAsync<DuplicatePtId>(sqlQuery, _dbProvider.GetConnectionString());
        return duplicate_ptid;
    }
}
