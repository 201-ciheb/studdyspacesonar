//using PHIASPACE.RTDMS.DAL.Model.Data;
//using PHIASPACE.RTDMS.Utility;
//using System.Data.SqlClient;

//namespace PHIASPACE.RTDMS.DAL.Utils
//{
//    public class TeamUtil
//    {
//        readonly static RtdmsDbContext _dbcontext = new RtdmsDbContext();

//        public static AimsEa GetActiveEa(int team_id)
//        {
//            var lnk_ea = _dbcontext.AimsLnkTeamEas.FirstOrDefault(e => e.TeamId == team_id && e.Active == 1);
//            if (lnk_ea != null)
//                return lnk_ea.Ea;
//            else
//                return null;
//        }
//        public static int GetDuplicatePtids(int team_id)
//        {
//            var result = 0;
//            var sql = "SELECT  sum([Count]) FROM [dbo].[VW_multiple_ptids] where team=@team_id";
//            var cmd = new SqlCommand();
//            cmd.CommandText = sql;
//            cmd.Parameters.AddWithValue("@team_id", team_id);
//            cmd.CommandType = System.Data.CommandType.Text;
//            var dt = Options.GetDatable(cmd);
//            try
//            {
//                if (dt != null && dt.Rows.Count > 0)
//                {
//                    result = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
//                }
//            }
//            catch (Exception)
//            {

//            }


//            return result;
//        }


//        public static float GetStartHH(int ea_id)
//        {
//            return _dbcontext.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id);
//        }
//        public static float GetCompHH(int ea_id)
//        {
//            return _dbcontext.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id && e.AHRESULT == 1);
//        }

//        public static float GetRefusedHH(int ea_id)
//        {
//            return _dbcontext.CAPI_AHSECOVER.Count(e => e.A1QCLUST == ea_id && e.AHRESULT == 5);
//        }

//    }
//}
