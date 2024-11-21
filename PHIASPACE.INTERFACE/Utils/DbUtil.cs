using System.Data;
using System.Data.SqlClient;
using PHIASPACE.INTERFACE.DAL.Model.Data;

namespace PHIASPACE.INTERFACE.Utils;

public abstract class DbUtil{
    public static async Task DoBulkInsertUpdate<T>(List<T> items, string procedureName, string connectionString)
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
                cmd.Parameters.AddWithValue("@SatLabAssessmentTable", table);
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
    public static async Task<List<T>> ExecuteSpAsync<T>(string procedureName, string connectionString)
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
                dynamicObjs = new List<T>();
            }

            return dynamicObjs;
        }
    }

    public static async Task<List<T>> ExecuteQueryAsync<T>(string sqlQuery, string connectionString)
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

    public static async Task<T> ExecuteSingleSpAsync<T>(string procedureName, string connectionString)
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

    public static async Task<T> ExecuteSingleQueryAsync<T>(string sqlQuery, string connectionString)
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

    public static async Task ExecuteSingleQueryAsync(string sqlQuery, string connectionString)
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
}