using System.Data;
using System.Threading.Tasks;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;

namespace PHIASPACE.RTDMS.DAL.Service
{
    public class FormDataService : IFormDataService
    {
        private readonly IAimsDbProvider _dbProvider;

        public FormDataService(IAimsDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        // Method to get form data using stored procedure
        public async Task<DataSet> GetFormDataAsync(int? formId, int? clusterNo, int? hh, int? line)
        {
            var sqlQuery = "sp_get_form_data";
            var parameters = new[]
            {
                _dbProvider.CreateParameter("@cluster", clusterNo ?? (object)DBNull.Value, DbType.Int32),
                _dbProvider.CreateParameter("@hh", hh ?? (object)DBNull.Value, DbType.Int32),
                _dbProvider.CreateParameter("@form_id", formId ?? (object)DBNull.Value, DbType.Int32),
                _dbProvider.CreateParameter("@line", line ?? (object)DBNull.Value, DbType.Int32)
            };

            var ds = await _dbProvider.ExecuteSpAsyncReturnDataSet(sqlQuery, _dbProvider.GetConnectionString(), parameters);
            return ds;
        }
    }
}
