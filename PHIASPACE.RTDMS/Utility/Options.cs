//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data.SqlClient;
//using System.Data;
//using System.IO;
//using System.Net;
//using System.Net.Mail;
//using System.Net.Mime;
////using DataAccess.Models;
//using PHIASPACE.RTDMS.DAL.Model.Data;

//namespace PHIASPACE.RTDMS.Utility
//{
//    public static class Options
//    {
//        //private static readonly aim_naiis_ds_entities entity = new aim_naiis_ds_entities();
//        // private readonly IConfiguration _configuration;

//        public static DataTable GetDatable(SqlCommand command)
//        {
//            var connstring = StaticConfig.GetConnectionString("RtdmsDbConnection");

//            using (var conn = new SqlConnection(connstring))
//            {
//                command.Connection = conn;
//                conn.Open();
//                using (command)
//                {
//                    using (SqlDataAdapter a = new SqlDataAdapter(command))
//                    {

//                        DataTable t = new DataTable();
//                        a.Fill(t);
//                        return t;

//                    }
//                }
//            }
//        }

//        public static DataSet GetDataSet(SqlCommand cmd)
//        {
//            var connstring = StaticConfig.GetConnectionString("RtdmsDbConnection");
//            using (var conn = new SqlConnection(connstring))
//            {
//                conn.Open();
//                cmd.Connection = conn;
//                using (cmd)
//                {
//                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
//                    {

//                        DataSet t = new DataSet();
//                        a.Fill(t);
//                        return t;

//                    }
//                }
//            }
//        }
//    }
//}