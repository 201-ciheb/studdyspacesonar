using System.Data;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model.Custom;

namespace PHIASPACE.RTDMS.DAL.Service;

public class MonitoringService : IMonitoringService{
    // TODO: This class can be added to the team service implementation once it is restructured
    private readonly IAimsDbProvider _dbProvider;
    private readonly IConfiguration _configuration;

    public MonitoringService(IAimsDbProvider dbProvider, IConfiguration configuration){
        _dbProvider = dbProvider;
        _configuration = configuration;
    }

    public async Task<List<UserTeamEaDetails>> GetTeamEaDetailsAsync(int team_id, string region)
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            $"Exec [monitor].[sp_get_team_ea] @team_code = {team_id}, @region = '{region}'" :
            $"CALL sp_get_team_ea({team_id}, '{region}')";
        var team_users = await _dbProvider.ExecuteQueryAsync<UserTeamEaDetails>(sqlQuery, _dbProvider.GetConnectionString());
        return team_users;
    }

    public async Task<DataTable> GetTeamEaStat(int ea_number)
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            $"Exec [monitor].[sp_get_ea_hh_statistics] @cluster_number = {ea_number}" :
            $"CALL sp_get_ea_hh_statistics({ea_number})";
        var hh_statistics = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());
        return hh_statistics;
    }

    public async Task<DataTable> GetTeamEaMembers(int ea_number)
    {
        var dbType = _configuration["DatabaseType:DbType"];
        var sqlQuery = dbType == "MSSQL" ?
            $"Exec [monitor].[sp_get_ea_team_details] @cluster_number = {ea_number}":
            $"CALL sp_get_ea_team_details({ea_number})";
        var ea_team = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());
        return ea_team;
    }
}
