using System;
using System.Data;
using System.Threading.Tasks;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;

namespace PHIASPACE.RTDMS.DAL.Service
{
    public class HouseholdService : IHouseholdService
    {
        private readonly IAimsDbProvider _dbProvider;

        public HouseholdService(IAimsDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<DataSet> GetHouseholdDetailsAsync(int clusterNo, int hhId)
        {
            var sqlQuery = "sp_get_hh_details";
            var parameters = new[]
            {
                _dbProvider.CreateParameter("@cluster", clusterNo, DbType.Int32),
                _dbProvider.CreateParameter("@hh", hhId, DbType.Int32)
            };

            var ds = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString(), parameters);
            return ds;
        }

        public async Task<DataSet> GetAllHouseholdDataAsync()
        {
            var sqlQuery = "sp_get_hh_data";
            var householdData = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString());
            return householdData;
        }

       public async Task<DataSet> GetHouseholdDataAsync(int page_id, int pageSize, int? level, int? locationId)
{
    var sqlQuery = "sp_get_hh_line";
    var parameters = new[]
    {
        // Conditionally set the parameter value to DBNull if null
        _dbProvider.CreateParameter("@level", level.HasValue ? (object)level.Value : DBNull.Value, DbType.Int32),
        _dbProvider.CreateParameter("@location_id", locationId.HasValue ? (object)locationId.Value : DBNull.Value, DbType.Int32)
    };

    var ds = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString(), parameters);
    return ds;
}


        private void LogDataSet(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine($"Table: {table.TableName}");
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        Console.Write($"{column.ColumnName}: {row[column]} \t");
                    }
                    Console.WriteLine(); // New line for each row
                }
            }
        }

        public async Task<DataSet> GetHouseholdSummaryAsync(int clusterNo, int hhId)
        {
            var sqlQuery = "sp_get_hh_get_summary";
            var parameters = new[]
            {
                _dbProvider.CreateParameter("@cluster", clusterNo, DbType.Int32),
                _dbProvider.CreateParameter("@hh", hhId, DbType.Int32)
            };

            var ds = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString(), parameters);
            return ds;
        }

        // Uncomment and implement if needed
        // public AimsPerson GetTeamLead(int cluster)
        // {
        //     var sqlQuery = "SELECT ... FROM ... WHERE EaId = @cluster"; // Define your SQL query here
        //     var parameter = _dbProvider.CreateParameter("@cluster", cluster, DbType.Int32);
        //     var teamLead = _dbProvider.ExecuteQuerySingleOrDefaultAsync<AimsPerson>(sqlQuery, new[] { parameter });
        //     return teamLead;
        // }
    }
}