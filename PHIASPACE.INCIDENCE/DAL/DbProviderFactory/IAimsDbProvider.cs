using System.Data;

namespace PHIASPACE.INCIDENCE.DAL.DbProviderFactory;

public interface IAimsDbProvider
{
    string GetConnectionString();
    Task<List<T>> GetAllAsync<T>() where T : class;
    // Task<T> GetByIdAsync<T>(int id) where T : class;
    Task<bool> AddAsync<T>(T entity) where T : class;
    // Task<bool> UpdateAsync<T>(T entity) where T : class;
    // Task<bool> DeleteAsync<T>(int id) where T : class;
    Task DoBulkInsertUpdate<T>(List<T> items, string procedureName, string connectionString);
    Task<List<T>> ExecuteSpAsync<T>(string procedureName, string connectionString);
    Task<List<T>> ExecuteQueryAsync<T>(string sqlQuery, string connectionString);
    Task<T> ExecuteSingleSpAsync<T>(string procedureName, string connectionString);
    Task<T> ExecuteSingleQueryAsync<T>(string sqlQuery, string connectionString);
    Task ExecuteSingleQueryAsync(string sqlQuery, string connectionString);
    Task<DataTable> ExecuteSpAsyncReturnDataTable(string procedureName, string connectionString);
    Task<DataSet> ExecuteSpAsyncReturnDataSet(string procedureName, string connectionString);
    Task<DataTable> ExecuteQueryAsyncReturnDataTable(string procedureName, string connectionString);
    Task<DataSet> ExecuteQueryAsyncReturnDataSet(string procedureName, string connectionString);
}