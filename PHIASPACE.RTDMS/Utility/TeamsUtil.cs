using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PHIASPACE.RTDMS.Utility
{
    public static class TeamUtil
    {

        // public static void Initialize(RtdmsDbContext entity)
        // {
        //     _entity = entity;
        // }

        // public static int GetTeam(int ea_id)
        // {

        //     try
        //     {
        //         var lnk_ea = _entity.AimsLnkTeamEas.FirstOrDefault(e => e.EaId == ea_id);
        //         return lnk_ea.TeamId;
        //     }
        //     catch (Exception)
        //     {
        //         return 0;
        //     }

        // }

        // public static AimsEa GetActiveEa(int team_id)
        // {
        //     var lnk_ea = _entity.AimsLnkTeamEas.FirstOrDefault(e => e.TeamId == team_id && e.Active == 1);
        //     if (lnk_ea != null)
        //         return lnk_ea.Ea;
        //     else
        //         return null;
        // }

        public static async Task<int> GetDuplicatePtids(int team_id)
        {
            var result = 0;

            // Using the DbContext to execute the query
            // using (var command = _entity.Database.GetDbConnection().CreateCommand())
            // {
            //     command.CommandText = "SELECT sum([Count]) FROM [dbo].[VW_multiple_ptids] where team=@team_id";
            //     command.CommandType = CommandType.Text;
            //     command.Parameters.Add(new SqlParameter("@team_id", team_id));

            //     try
            //     {
            //         using (var reader = await command.ExecuteReaderAsync())
            //         {
            //             if (reader.HasRows)
            //             {
            //                 await reader.ReadAsync();
            //                 result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            //             }
            //         }
            //     }
            //     catch (Exception)
            //     {
            //     }
            // }

            return result;
        }


        // public static AimsTeam? GetAssignedTeam(int ea_id)
        // {
        //     var assignedTeam = _entity.AimsLnkTeamEas.FirstOrDefault(e => e.EaId == ea_id);
        //     return assignedTeam?.Team; 
        // }

        public static DateTime GetEaStartDate(int ea_id)
        {
            return DateTime.Now;
        }

        public static DateTime GetEaExpCompDate(int ea_id)
        {
            return DateTime.Now.AddDays(10);
        }

        // public static float GetStartHH(int ea_id)
        // {
        //     return _entity.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id);
        // }

        // public static float GetCompHH(int ea_id)
        // {
        //     return _entity.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id && e.AHRESULT == 1);
        // }

        // public static float GetRefusedHH(int ea_id)
        // {
        //     return _entity.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id && e.AHRESULT == 5);
        // }


        // public static float GetUploadedHH(int ea_id)
        // {
        //     return _entity.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id);
        // }

        public static DateTime GetLastContact(int team_id)
        {
            return DateTime.Now;
        }

        public static int GetAlerts(int team_id)
        {
            return new Random().Next();
        }

        // public static string GetHHUploadTime(int hh_id, int ea_id)
        // {
        //     var hh_info = _entity.CAPI_AHSECOVER.FirstOrDefault(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id);
        //     if (hh_info != null) return hh_info.created.Value.ToString("yyyy-MM-dd h: mm tt");
        //     return "";

        // }

        // public static int GetPersonTeam(int person_id)
        // {
        //     return _entity.AimsLnkTeamPersons.FirstOrDefault(m => m.PersonId == person_id).TeamId;
        // }
    }
}

