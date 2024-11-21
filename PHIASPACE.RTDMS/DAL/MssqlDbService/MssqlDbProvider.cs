using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;

namespace PHIASPACE.RTDMS.DAL.MssqlDbService;

public class MssqlDbProvider : IAimsDbProvider
{
    private readonly AimsMssqlDbContext _context;
    private readonly ILogger<AimsMssqlDbContext> _logger;
    public MssqlDbProvider(AimsMssqlDbContext context, ILogger<AimsMssqlDbContext> logger)
    {
        _context = context;
        _logger = logger;
    }

    // public MssqlDbProvider()
    // {
    // }

    public string GetConnectionString()
    {
        return _context.Database.GetConnectionString();
    }

    public async Task DoBulkInsertUpdate<T>(List<T> items, string procedureName, string connectionString)
    {
        using (var con = new SqlConnection(connectionString))
        {
            var table = new DataTable();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                table.Columns.Add(property.Name, propertyType);
            }
            foreach (var item in items)
            {
                var row = table.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                table.Rows.Add(row);
            }
            using (SqlCommand cmd = new SqlCommand(procedureName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                // cmd.Parameters.AddWithValue("@SatLabAssessmentTable", table);
                con.Open();
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    //handle error and update accordingly
                }
                con.Close();
            }
        }
    }
    /**
        This function assumes the class and the stored procedure select query variables match
    **/
    public async Task<List<T>> ExecuteSpAsync<T>(string procedureName, string connectionString)
    {
        var dynamicObjs = new List<T>();

        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(procedureName, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var propertyNames = typeof(T).GetProperties().Select(p => p.Name).ToList();

                    while (await reader.ReadAsync())
                    {
                        var dynamicObj = Activator.CreateInstance<T>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            if (propertyNames.Contains(columnName))
                            {
                                var property = typeof(T).GetProperty(columnName);
                                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                property.SetValue(dynamicObj, value);
                            }
                           
                        }
                        dynamicObjs.Add(dynamicObj);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error executing stored procedure {ProcedureName}", procedureName);
                dynamicObjs = new List<T>();
            }

            return dynamicObjs;
        }
    }

    public async Task<List<T>> ExecuteQueryAsync<T>(string sqlQuery, string connectionString)
    {
        var dynamicObjs = new List<T>();

        try
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlQuery, con))
            {
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var propertyNames = typeof(T).GetProperties().Select(p => p.Name).ToList();

                    while (await reader.ReadAsync())
                    {
                        var dynamicObj = Activator.CreateInstance<T>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            if (propertyNames.Contains(columnName))
                            {
                                var property = typeof(T).GetProperty(columnName);
                                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                property.SetValue(dynamicObj, value);
                            }
                        }
                        dynamicObjs.Add(dynamicObj);
                    }
                }
            }
        }
        catch (Exception e)
        {
            dynamicObjs = new List<T>();
        }

        return dynamicObjs;
    }

    public async Task<T> ExecuteSingleSpAsync<T>(string procedureName, string connectionString)
    {
        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(procedureName, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            var dynamicObj = Activator.CreateInstance<T>();

            try
            {                
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var propertyNames = typeof(T).GetProperties().Select(p => p.Name).ToList();

                    while (await reader.ReadAsync())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            if (propertyNames.Contains(columnName))
                            {
                                var property = typeof(T).GetProperty(columnName);
                                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                property.SetValue(dynamicObj, value);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                dynamicObj = Activator.CreateInstance<T>();
            }

            return dynamicObj;
        }
    }

    public async Task<T> ExecuteSingleQueryAsync<T>(string sqlQuery, string connectionString)
    {
        var dynamicObj = Activator.CreateInstance<T>();

        try
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlQuery, con))
            {
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var propertyNames = typeof(T).GetProperties().Select(p => p.Name).ToList();

                    while (await reader.ReadAsync())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            if (propertyNames.Contains(columnName))
                            {
                                var property = typeof(T).GetProperty(columnName);
                                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                property.SetValue(dynamicObj, value);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            dynamicObj = Activator.CreateInstance<T>();
        }

        return dynamicObj;
    }

    public async Task ExecuteSingleQueryAsync(string sqlQuery, string connectionString)
    {
        try
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlQuery, con))
            {
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }    
    }

    public async Task<DataTable> ExecuteSpAsyncReturnDataTable(string procedureName, string connectionString)
    {
        var dataTable = new DataTable();

        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(procedureName, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception e)
            {
                dataTable = new DataTable();
            }

            return dataTable;
        }
    }

    public async Task<DataSet> ExecuteSpAsyncReturnDataSet(string procedureName, string connectionString)
    {
        var dataSet = new DataSet();

        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(procedureName, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                await con.OpenAsync();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    await Task.Run(() => adapter.Fill(dataSet));
                }
            }
            catch (Exception e)
            {
                dataSet = new DataSet();
            }

            return dataSet;
        }
    }

    public async Task<DataTable> ExecuteQueryAsyncReturnDataTable(string sqlQuery, string connectionString)
    {
        var dataTable = new DataTable();

        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(sqlQuery, con))
        {
            try
            {
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception e)
            {
                dataTable = new DataTable();
            }

            return dataTable;
        }
    }

    public async Task<DataSet> ExecuteQueryAsyncReturnDataSet(string procedureName, string connectionString)
    {
        var dataSet = new DataSet();

        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(procedureName, con))
        {
            try
            {
                await con.OpenAsync();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    await Task.Run(() => adapter.Fill(dataSet));
                }
            }
            catch (Exception e)
            {
                dataSet = new DataSet();
            }

            return dataSet;
        }
    }
    public async Task<DataTable> ExecuteSpAsyncReturnDataTable(string procedureName, string connectionString, params IDbDataParameter[] parameters)
    {
        var dataTable = new DataTable();

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(procedureName, connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            // Add parameters to the command, if any
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            try
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing stored procedure {ProcedureName} with parameters {Parameters}", procedureName, parameters);
                dataTable.Clear(); // Clear the DataTable to ensure it's empty in case of an error
            }
        }

        return dataTable;
    }

    public async Task<DataSet> ExecuteSpAsyncReturnDataSet(string procedureName, string connectionString, params IDbDataParameter[] parameters)
    {
        var dataSet = new DataSet();

        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(procedureName, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            try
            {
                await con.OpenAsync();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataSet);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error executing stored procedure {ProcedureName}", procedureName);
            }
        }

        return dataSet;
    }

    public async Task<T> ExecuteScalarAsync<T>(string sqlQuery, string connectionString, params IDbDataParameter[] parameters)
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(sqlQuery, connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();

            // Handle the case where result might be null
            if (result == null || result == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(result, typeof(T));
        }
    }

    public async Task<int> ExecuteNonQueryAsync(string sqlQuery, string connectionString, params IDbDataParameter[] parameters)
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(sqlQuery, connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
    }


    public async Task<DataTable> ExecuteQueryAsyncReturnDataTable(string sqlQuery, string connectionString, params IDbDataParameter[] parameters)
    {
        var dataTable = new DataTable();
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
            }
        }
        return dataTable;
    }

    public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
    {
        return new SqlParameter
        {
            ParameterName = name,
            Value = value ?? DBNull.Value,
            DbType = dbType
        };
    }

    public async Task<List<T>> GetAllAsync<T>() where T : class
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<bool> AddAsync<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
