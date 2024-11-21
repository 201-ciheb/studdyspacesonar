using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace PHIASPACE.CORE.DAL.Setup
{
    class SqlManager
    {
        private string connectionStringMaster;
        private SqlConnection connection;
        private string DBName;

        public SqlManager(string dbname)
        {            
            DBName = dbname;
            IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("appsettings.json")
                    .Build();
            connectionStringMaster = config.GetConnectionString("PHIASPACEMasterDbConnection");
            //var targetDBStartString = connectionString.Substring(connectionString.IndexOf("Initial Catalog="));
            // if(!string.IsNullOrEmpty(dbname))
            //     connectionStringMaster = connectionStringMaster.Replace("default_parser_db", DBName);
        }

        public Boolean connectMaster()
        {
            connection = new SqlConnection(connectionStringMaster);
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean close()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void createDatabase()
        {
            if (connectMaster())
            {                
                string sql = @"USE master                                
                   IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '" + DBName + @"')
                   BEGIN
                       CREATE DATABASE " + DBName + @";
                   END;";
                
                //String sql = string.Format("CREATE DATABASE {0}", databaseName);
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine(string.Format("Database {0} Created", DBName));
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Error creating database {0}", DBName));
                    Console.WriteLine("error: " + e.ToString());
                }
                close();
            }
            else
            {
                Console.WriteLine("failed to connect to database");
            }
        }

        public void UpdateTable(string updateSts)
        {
            if (connectMaster())
            {                
                SqlCommand command = new SqlCommand(updateSts, connection);
                try
                {                    
                    command.ExecuteNonQuery();
                    Console.WriteLine("Query Success: " + updateSts);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e.ToString());
                }
                close();
            }
            else
            {
                Console.WriteLine("failed to connect to database");
            }
        }
    }
}
