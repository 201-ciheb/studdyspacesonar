using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.Utility;
using System.Data;
using System.Data.SqlClient;

namespace PHIASPACE.RTDMS.DAL.Utils
{
    public class TeamUtil
    {

        // public static AimsEa GetActiveEa(int team_id)
        // {
        //     var lnk_ea = _dbcontext.AimsLnkTeamEas.FirstOrDefault(e => e.TeamId == team_id && e.Active == 1);
        //     if (lnk_ea != null)
        //         return lnk_ea.Ea;
        //     else
        //         return null;
        // }
        // public static int GetDuplicatePtids(int team_id)
        // {
        //     try
        //     {
        //         var sql = "SELECT SUM([Count]) FROM [dbo].[VW_multiple_ptids] WHERE [team] = @team_id";
        //         var result = _dbcontext.Database.SqlQueryRaw<int?>(sql, new SqlParameter("@team_id", team_id)).FirstOrDefault();

        //         return result ?? 0;
        //     }
        //     catch (Exception ex)
        //     {
        //         return 0;
        //     }
        // }


        // public static float GetStartHH(int ea_id)
        // {
        //     return _dbcontext.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id);
        // }
        // public static float GetCompHH(int ea_id)
        // {
        //     return _dbcontext.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id && e.AHRESULT == 1);
        // }

        // public static float GetRefusedHH(int ea_id)
        // {
        //     return _dbcontext.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id && e.AHRESULT == 5);
        // }

    }
}
