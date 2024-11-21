using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.MssqlDbService;

namespace PHIASPACE.RTDMS.Utility
{
    public class HHUtil
    {
        private readonly IUtilService _utilService;

        public HHUtil(IUtilService utilService){
            _utilService = utilService;
        }

        public async Task<int> GetDuplicatePtidsEA(int ea_id)
        {
            var result = await _utilService.ListDuplicatePtidsEA(ea_id);
            return result.Count;
        }

        public async Task<string> ListDuplicatePtidsEA(int ea_id)
        {
            var result = await _utilService.ListDuplicatePtidsEA(ea_id);
            return string.Join(",", result.Select(r => r.PtId));
        }

        //public static bool AdultIndividualCompleted(int eaId, int hhId, int line)
        //{
        //    var consent = _context.CapiIaRefus.FirstOrDefault(e => e.IACLUSTER == eaId && e.IANUMBER == hhId && e.IALINE == line);
        //    if (consent != null)
        //    {
        //        if (consent.IAC01 == 1)
        //        {
        //            var interviewInfo = _context.CapiIaCover.FirstOrDefault(e => e.IACLUSTER == eaId && e.IANUMBER == hhId && e.IALINE == line);
        //            if (interviewInfo == null || !interviewInfo.ARESULT.HasValue)
        //                return false;
        //        }
        //        if (consent.IAC02 == 1)
        //        {
        //            var bloodDraw = _context.CapiLconsent.FirstOrDefault(e => e.LCLUSTER == eaId && e.LHHNUMBER == hhId && e.LLINENUMBER == line);
        //            if (bloodDraw == null || !bloodDraw.LCB01.HasValue)
        //                return false;
        //        }
        //        if (consent.IAC04 == 1)
        //        {
        //            var counselling = _context.CapiConsent.FirstOrDefault(e => e.CIACLUSTER == eaId && e.CIANUMBER == hhId && e.CIALINE == line);
        //            if (counselling == null || !counselling.CCB0A.HasValue || counselling.CCB0A.Value == -1)
        //                return false;
        //        }
        //        return true;
        //    }
        //    return false;
        //}
        //public static bool ChildLessThan10Completed(int eaId, int hhId, int line)
        //{
        //    var consent = _context.CapiChconsent.FirstOrDefault(e => e.CHCLUSTER == eaId && e.CHHHNUMBER == hhId && e.CHCLINE == line);
        //    if (consent != null)
        //    {
        //        if (consent.CHB03M01 == 1)
        //        {
        //            var bloodDraw = _context.CapiLconsent.FirstOrDefault(e => e.LCLUSTER == eaId && e.LHHNUMBER == hhId && e.LLINENUMBER == line);
        //            if (bloodDraw == null || !bloodDraw.LCB01.HasValue)
        //                return false;
        //        }
        //        if (consent.CHB03M03 == 1)
        //        {
        //            var counselling = _context.CapiConsent.FirstOrDefault(e => e.CIACLUSTER == eaId && e.CIANUMBER == hhId && e.CIALINE == line);
        //            if (counselling == null || !counselling.CCB0A.HasValue || counselling.CCB0A.Value == -1)
        //                return false;
        //        }
        //        return true;
        //    }
        //    return false;
        //}
        //public static bool HouseholdCompleted(int ea_id, int hh_id)
        //{
        //    try
        //    {
        //        var result = true;
        //        var members = _context.CAPI_A1SEC01X.Where(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id);
        //        if (members.Any())
        //        {
        //            foreach (var member in members)
        //            {
        //                if (ParticipantCompleted(ea_id, hh_id, member.AHLINE, member.AHAGE.Value) != 1) result = false;
        //            }
        //        }
        //        else
        //        {
        //            result = false;
        //        }
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}
        //public static int ParticipantCompleted(int ea_id, int hh_id, int line, int age)
        //{
        //    int result = 0;

        //    try
        //    {
        //        //check if the user is eligible
        //        var elig_table = _context.CAPI_A1SEC01.FirstOrDefault(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id && e.A1Q001 == line);
        //        if (elig_table != null && elig_table.A1Q015.HasValue && elig_table.A1Q015.Value == 1)
        //        {
        //            if (age > 64)
        //                result = -1;
        //            //adult
        //            else if (age > 14 && age < 64)
        //            {
        //                if (AdultIndividualCompleted(ea_id, hh_id, line)) result = 1;

        //            }
        //            else if (age < 10)
        //            {
        //                //ChildLessThan10Completed
        //                if (ChildLessThan10Completed(ea_id, hh_id, line)) result = 1;
        //            }
        //        }
        //        else
        //        {
        //            result = -1;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return result;
        //}

        // public static string ListDuplicatePtidsEAs(int ea_id)
        // {
        //     var result = new List<string>();

        //     try
        //     {
        //         var sql = "SELECT DISTINCT PTID FROM [dbo].[VW_multiple_ptids] WHERE EA = @ea_id";

        //         using (var command = _context.Database.GetDbConnection().CreateCommand())
        //         {
        //             command.CommandText = sql;
        //             command.CommandType = CommandType.Text;
        //             command.Parameters.Add(new SqlParameter("@ea_id", ea_id));

        //             _context.Database.OpenConnection();
        //             using (var reader = command.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     result.Add(reader.GetString(0));
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Handle exceptions as necessary, e.g., logging the error
        //     }
        //     finally
        //     {
        //         _context.Database.CloseConnection();
        //     }

        //     return string.Join(",", result);
        // }



        // public static int GetDuplicatePtidsEAs(int ea_id)
        // {
        //     int result = 0;

        //     try
        //     {
        //         var sql = "SELECT SUM([Count]) FROM [dbo].[VW_multiple_ptids] WHERE [EA] = @ea_id";

        //         using (var command = _context.Database.GetDbConnection().CreateCommand())
        //         {
        //             command.CommandText = sql;
        //             command.CommandType = CommandType.Text;
        //             command.Parameters.Add(new SqlParameter("@ea_id", ea_id));

        //             _context.Database.OpenConnection();
        //             using (var reader = command.ExecuteReader())
        //             {
        //                 if (reader.HasRows)
        //                 {
        //                     reader.Read();
        //                     result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Handle exceptions as necessary, e.g., logging the error
        //     }
        //     finally
        //     {
        //         _context.Database.CloseConnection();
        //     }

        //     return result;
        // }


        //public static bool UserHasForm(int form_id, int ea_id, int hh_id, int line)
        //{
        //    var result = false;
        //    try
        //    {
        //        var identifier = new AimsParticipantIdentifier(); //_entity.aims_participant_identifier.FirstOrDefault(e => e.PTID == participant_id);
        //        identifier.EaId = ea_id;
        //        identifier.HhId = hh_id;
        //        identifier.Line = line;

        //        if (identifier != null)
        //        {
        //            if (form_id == 1)
        //            {
        //                //check the household questionnaire
        //                var hh = _context.CAPI_AHSECOVER.FirstOrDefault(e => e.A1QCLUST == identifier.EaId && e.A1QNUMBER == identifier.HhId);
        //                if (hh != null && hh.AHRESULT.HasValue) result = true;
        //            }
        //            else if (form_id == 2 || form_id == 3)
        //            {
        //                //HH
        //                var adult = _context.CAPI_IACOVER.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (adult != null && adult.ARESULT.HasValue && adult.ARESULT.Value == 1) result = true;
        //            }
        //            //else if (form_id == 3)
        //            //{
        //            //    //adolescent
        //            //    var adolescent = _entity.CAPI_AM01.Where(e => e.IACLUSTER == identifier.ea_id && e.IANUMBER == identifier.hh_id && e.IALINE == identifier.line);
        //            //    if (adolescent.Any()) result = true;
        //            //}
        //            else if (form_id == 4)
        //            {
        //                //household consent
        //                var hh = _context.CAPI_AHSECOVER.FirstOrDefault(e => e.A1QCLUST == identifier.EaId && e.A1QNUMBER == identifier.HhId);
        //                if (hh != null && hh.B1CONSENT.Value == 1)
        //                {
        //                    result = true;
        //                }
        //            }
        //            else if (form_id == 5)
        //            {
        //                //interview consent
        //                var consent = _context.CAPI_IAREFUS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (consent != null && consent.IAC01 == 1)
        //                {
        //                    result = true;
        //                }
        //            }
        //            else if (form_id == 6)
        //            {
        //                //parental consent for children
        //                var parent = _context.CAPI_CHCONSENT.FirstOrDefault(e => e.CHCLUSTER == identifier.EaId && e.CHHHNUMBER == identifier.HhId && e.CHCLINE == identifier.Line);
        //                if (parent != null && parent.CHB03M01.HasValue && parent.CHB03M01.Value == 1)
        //                {
        //                    return true;
        //                }

        //            }

        //            else if (form_id == 7)
        //            {
        //                //Parental permission for children 10-17
        //                var parent = _context.CAPI_B4REFAS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (parent != null && parent.AMC01.Value == 1)
        //                {
        //                    return true;
        //                }

        //            }

        //            else if (form_id == 8)
        //            {
        //                //Assent for Interview and blood draw 15-17
        //                var parent = _context.CAPI_B4REFAS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (parent != null && parent.AMC02.Value == 1)
        //                {
        //                    return true;
        //                }

        //            }
        //            else if (form_id == 9)
        //            {
        //                //Assent for Interview and blood draw 10-14
        //                var parent = _context.CAPI_B4REFAS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (parent != null && parent.AMC02.Value == 1)
        //                {
        //                    return true;
        //                }

        //            }
        //            else if (form_id == 10)
        //            {
        //                //Parental consent for contact information 0-14
        //                var parent = _context.CAPI_CHCONSENT.FirstOrDefault(e => e.CHCLUSTER == identifier.EaId && e.CHHHNUMBER == identifier.HhId && e.CHCLINE == identifier.Line);
        //                if (parent != null && parent.CHB03M03.HasValue && parent.CHB03M01.Value == 1)
        //                {
        //                    return true;
        //                }


        //            }

        //            else if (form_id == 11)
        //            {
        //                //Consent for contact information 18-64
        //                var parent = _context.CAPI_IAREFUS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (parent != null && parent.IAC04.Value == 1)
        //                {
        //                    return true;
        //                }

        //            }
        //            else if (form_id == 12)
        //            {
        //                //Parental consent for contact information 15-17
        //                var parent = _context.CAPI_B4REFAS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (parent != null && parent.AMC04.Value == 1)
        //                {
        //                    return true;
        //                }

        //            }

        //            else if (form_id == 13)
        //            {
        //                //Refusal withdrawal form


        //            }
        //            else if (form_id == 14)
        //            {
        //                //Refusal withdrawal interview


        //            }
        //            else if (form_id == 15)
        //            {
        //                //Refusal withdrawal specimen storage


        //            }
        //            else if (form_id == 16)
        //            {
        //                //Counselling information
        //                var counselling = _context.CAPI_CONSENT.FirstOrDefault(e => e.CIACLUSTER == identifier.EaId && e.CIANUMBER == identifier.HhId && e.CIALINE == identifier.Line);
        //                if (counselling != null && counselling.CCB0A.HasValue && counselling.CCB0A.Value != -1)
        //                {
        //                    return true;
        //                }

        //            }
        //            else if (form_id == 17)
        //            {
        //                //Field test results form
        //                var rs = _context.CAPI_IDENTIFY.FirstOrDefault(e => e.LCLUSTER == identifier.EaId && e.LHHNUMBER == identifier.HhId && e.LLINENUMBER == identifier.Line);
        //                if (rs != null)
        //                {
        //                    return true;
        //                }


        //            }
        //            else if (form_id == 18)
        //            {
        //                //Field test result referral form


        //            }
        //            else if (form_id == 19)
        //            {
        //                //consent for blood draw
        //                var consent = _context.CAPI_IAREFUS.FirstOrDefault(e => e.IACLUSTER == identifier.EaId && e.IANUMBER == identifier.HhId && e.IALINE == identifier.Line);
        //                if (consent != null && consent.IAC02 == 1)
        //                {
        //                    result = true;
        //                }


        //            }
        //        }


        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return result;
        //}

        //public static IQueryable<CAPI_A1SEC01X> GetHHIndividual(int ea_id, int hh_id)
        //{
        //    return _context.CAPI_A1SEC01X.Where(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id);
        //}

        //public static int GetQuestionaires(int ea_id, int hh_id)
        //{
        //    var result = 0;
        //    //get the adult forms for the household
        //    result += _context.CAPI_A1SEC01.Count(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id);

        //    return result;
        //}

        //public static int GetConsents(int ea_id, int hh_id)
        //{
        //    return _context.CAPI_CONSENT_Bio.Count(e => e.LCLUSTER == ea_id && e.LHHNUMBER == hh_id);
        //}

        // public static List<string> RunHHQC(int ea_id, int hh_id)
        // {
        //     var errors = new List<string>();

        //     try
        //     {
        //         var sql = "sp_hh_completion_script";

        //         using (var command = _context.Database.GetDbConnection().CreateCommand())
        //         {
        //             command.CommandText = sql;
        //             command.CommandType = CommandType.StoredProcedure;
        //             command.Parameters.Add(new SqlParameter("@ea_id", ea_id));
        //             command.Parameters.Add(new SqlParameter("@hh_id", hh_id));

        //             using (var adapter = new SqlDataAdapter((SqlCommand)command))
        //             {
        //                 var ds = new DataSet();
        //                 adapter.Fill(ds);

        //                 if (ds.Tables.Count > 0)
        //                 {
        //                     var hh_table = ds.Tables[0];
        //                     var individual_table = ds.Tables[1];

        //                     // Check the number of head of the household
        //                     var heads = individual_table.Select("A1Q003 = 1");
        //                     if (heads.Length > 1)
        //                     {
        //                         var ids = heads.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0029 - More than one head of households '{string.Join(",", ids)}'");
        //                     }

        //                     var parents = individual_table.Select("A1Q003 = 6");
        //                     if (heads.Length > 0)
        //                     {
        //                         if (parents.Length > 2)
        //                         {
        //                             var ids = parents.Select(row => row["A1Q002"].ToString()).ToList();
        //                             errors.Add($"0030 - The head of household should not have more than 2 parents '{string.Join(",", ids)}'");
        //                         }
        //                         else if (parents.Length == 2)
        //                         {
        //                             var genders = parents.Select(row => row["A1Q004"].ToString()).ToList();
        //                             if (genders[0] == genders[1])
        //                             {
        //                                 var ids = parents.Select(row => row["A1Q002"].ToString()).ToList();
        //                                 errors.Add($"0030 - Parents of the head of the household cannot be of the same sex '{string.Join(",", ids)}'");
        //                             }
        //                         }
        //                     }

        //                     // Check definition of household member
        //                     var members = individual_table.Select("A1Q005 = 2 AND A1Q006 = 2");
        //                     if (members.Length > 0)
        //                     {
        //                         var ids = members.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0050 - Household member must be a usual resident or must have slept here last night '{string.Join(",", ids)}'");
        //                     }

        //                     // Ensure the sex of the head of the household is different from that of the spouse
        //                     var spouse = individual_table.Select("A1Q003 = 2");
        //                     if (spouse.Length > 0)
        //                     {
        //                         var ids = new List<string>();
        //                         var head_gender = heads.FirstOrDefault()?["A1Q004"].ToString();
        //                         foreach (var sp in spouse)
        //                         {
        //                             var sp_gender = sp["A1Q004"].ToString();
        //                             if (head_gender != sp_gender)
        //                             {
        //                                 ids.Add(sp["A1Q002"].ToString());
        //                             }
        //                         }

        //                         if (ids.Count > 0)
        //                         {
        //                             errors.Add($"0060 - The sex of the head of the household must be different from that of the spouse '{string.Join(",", ids)}'");
        //                         }
        //                     }

        //                     // Check if the parents and children are correctly linked
        //                     var p_linkage = individual_table.Select("A1Q010A > 0 AND A1Q012A > 0");
        //                     if (p_linkage.Length > 0)
        //                     {
        //                         var ids = p_linkage.Where(p => p["A1Q004"].ToString() != "3").Select(p => p["A1Q002"].ToString()).ToList();
        //                         if (ids.Count > 0)
        //                         {
        //                             errors.Add($"0061 - Relationship between children and parents must be correct '{string.Join(",", ids)}'");
        //                         }
        //                     }

        //                     // Confirm that the age of the head of the household and the spouse is greater than 15
        //                     spouse = individual_table.Select("A1Q003 = 2 AND A1Q007 < 15");
        //                     if (spouse.Length > 0)
        //                     {
        //                         var ids = spouse.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0071 - The age of the head of the household and the spouse should not be less than 15 '{string.Join(",", ids)}'");
        //                     }

        //                     // Check the age difference between the head of the household and the parents
        //                     var head_age = Convert.ToInt32(heads.FirstOrDefault()?["A1Q007"]);
        //                     var u_parents = individual_table.Select($"A1Q003 = 6 AND A1Q007 < {head_age + 12}");
        //                     if (u_parents.Length > 0)
        //                     {
        //                         var ids = u_parents.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0072 - The age difference between the head of household and the parents must be greater than 12 years '{string.Join(",", ids)}'");
        //                     }

        //                     // Check for invalid parent/guardian
        //                     var invalid = individual_table.Select($"A1Q010A > {individual_table.Rows.Count} OR A1Q011A > {individual_table.Rows.Count} OR A1Q012A > {individual_table.Rows.Count} OR A1Q013A > {individual_table.Rows.Count}");
        //                     if (invalid.Length > 0)
        //                     {
        //                         var ids = invalid.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0120 - Invalid line number specified '{string.Join(",", ids)}'");
        //                     }

        //                     // Check for incorrect sex of parents or guardians
        //                     var inc_sex = individual_table.Select("(A1Q010A > 0  AND A1Q004 = 1) OR (A1Q011A > 0  AND A1Q004 = 1) OR (A1Q012A > 0  AND A1Q004 = 2) OR (A1Q013A > 0  AND A1Q004 = 2)");
        //                     if (inc_sex.Length > 0)
        //                     {
        //                         var ids = inc_sex.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0121 - Sex of parent or guardian is not correct '{string.Join(",", ids)}'");
        //                     }

        //                     var inc_rel = individual_table.Select("(A1Q010A > 0  AND A1Q003 != 6) OR  (A1Q012A > 0  AND A1Q003 != 6)");
        //                     if (inc_rel.Length > 0)
        //                     {
        //                         var ids = inc_rel.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"0122 - Relationship between parent and children is not correct '{string.Join(",", ids)}'");
        //                     }

        //                     // Check for electricity inconsistencies
        //                     var inc_elec = hh_table.Select("A1Q105A != A1Q105B");
        //                     if (inc_elec.Length > 0)
        //                     {
        //                         errors.Add("0600 - Household electricity inconsistent with connection to national grid");
        //                     }

        //                     // Check for cooking gas inconsistencies
        //                     var inc_cooking = hh_table.Select("A1Q106 = 1 AND A1Q105A != 1");
        //                     if (inc_cooking.Length > 0)
        //                     {
        //                         errors.Add("0601 - Household electricity inconsistent with type of cooking fuel");
        //                     }

        //                     ///==================Individual checks =================
        //                     var inconsistent_ages = individual_table.Select("A1Q007 != M102 AND M102 > 0");
        //                     if (inconsistent_ages.Length > 0)
        //                     {
        //                         var ids = inconsistent_ages.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"1061 - Respondent's age in individual questionnaire differs from age in household '{string.Join(",", ids)}'");
        //                     }

        //                     //"M334 = 1 AND M336 != ''"
        //                     var inconsistent_preg = individual_table.Select("M334 = 1 AND M336 != ''");
        //                     if (inconsistent_preg.Length > 0)
        //                     {
        //                         var ids = inconsistent_preg.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"3143 - Current pregnancy information (M334=n) inconsistent with current use information (M336=x) '{string.Join(",", ids)}'");
        //                     }

        //                     // Check for sex and pregnancy inconsistencies
        //                     var inconsistent_preg_sex = individual_table.Select("M601 = 2 AND (M303 > 0 OR M301  > 0)");
        //                     if (inconsistent_preg_sex.Length > 0)
        //                     {
        //                         var ids = inconsistent_preg_sex.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"5150 - Never had sexual intercourse (M601=0), but has children (M303=n) or is currently pregnant (M301=n) '{string.Join(",", ids)}'");
        //                     }

        //                     // First sex age inconsistency
        //                     var inconsistence = individual_table.Select("M602 > M102");
        //                     if (inconsistence.Length > 0)
        //                     {
        //                         var ids = inconsistence.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"5190 - Age at first sex (M602=n) exceeds current age (M102=n) '{string.Join(",", ids)}'");
        //                     }

        //                     // Sexual partner and pregnancy inconsistency
        //                     inconsistence = individual_table.Select("M603 = 0 AND M334 = 1");
        //                     if (inconsistence.Length > 0)
        //                     {
        //                         var ids = inconsistence.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"5200 - No sexual partners in last 12 months (M603=n), but is or may be pregnant '{string.Join(",", ids)}'");
        //                     }

        //                     // Union inconsistency
        //                     inconsistence = individual_table.Select("M1001 = 2 AND M1002 = 2 AND M201 = 2 AND M203 > 2");
        //                     if (inconsistence.Length > 0)
        //                     {
        //                         var ids = inconsistence.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"7250 - Husband/partner makes decisions or is present/listening, yet respondent not in union '{string.Join(",", ids)}'");
        //                     }

        //                     // Age inconsistency in individual questionnaire
        //                     inconsistence = individual_table.Select("A1Q007 != AM102 AND AM102 > 0");
        //                     if (inconsistence.Length > 0)
        //                     {
        //                         var ids = inconsistence.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"21061 - Respondent's age in individual questionnaire (AM102=n) differs from age in household '{string.Join(",", ids)}'");
        //                     }

        //                     // Age at circumcision inconsistency
        //                     inconsistence = individual_table.Select("AM802 > M102 AND AM802 > 0");
        //                     if (inconsistence.Length > 0)
        //                     {
        //                         var ids = inconsistence.Select(row => row["A1Q002"].ToString()).ToList();
        //                         errors.Add($"29010 - Age at circumcision (AM802=n) exceeds current age (M102=n) '{string.Join(",", ids)}'");
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Handle exception, e.g., logging the error
        //     }

        //     return errors;
        // }


        // public static DataSet generate_query(string item_name, string table_name, string cluster_column)
        // {
        //     Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //     var ds = new DataSet();

        //     // Generate the aggregated columns
        //     var categories = _context.AimsValueSets.Where(e => e.ItemName == item_name).ToList();
        //     var augs = new List<string>();
        //     foreach (var category in categories)
        //     {
        //         augs.Add(string.Format("SUM(CASE WHEN {0}='{1}' THEN 1 ELSE 0 END) AS '{2}'", category.ItemName, category.ValueId, r.Replace(category.Value, string.Empty)));
        //     }

        //     // First SQL query
        //     var sql1 = string.Format("SELECT {0} FROM {1}", string.Join(",", augs), table_name);
        //     DataTable table1 = ExecuteSqlQuery(sql1);

        //     if (table1 != null)
        //     {
        //         ds.Tables.Add(table1);
        //     }

        //     // Second SQL query
        //     var mainQuery = string.Format("SELECT [dbo].[sp_get_state_from_ea_id]({0}) AS [State], {1} FROM {2}", cluster_column, item_name, table_name);
        //     var sql2 = string.Format("SELECT [State], {0} FROM ({1}) rs GROUP BY [State]", string.Join(",", augs), mainQuery);
        //     DataTable table2 = ExecuteSqlQuery(sql2);

        //     if (table2 != null)
        //     {
        //         ds.Tables.Add(table2);
        //     }

        //     return ds;
        // }

        // private static DataTable ExecuteSqlQuery(string sql)
        // {
        //     using (var command = _context.Database.GetDbConnection().CreateCommand())
        //     {
        //         command.CommandText = sql;
        //         command.CommandType = CommandType.Text;

        //         using (var reader = command.ExecuteReader())
        //         {
        //             var table = new DataTable();
        //             table.Load(reader);
        //             return table;
        //         }
        //     }
        // }

        // public static int GetSignatures(int ea_id, int hh_id, int line)
        // {

        //     var location = System.Configuration.ConfigurationManager.AppSettings["signature_url"];
        //     //var location = @"C:\NAIIS\Signature";
        //     if (Directory.Exists(location))
        //     {
        //         var dir = new DirectoryInfo(location);
        //         var files = dir.GetFiles("*" + ea_id.ToString("D4") + hh_id.ToString("D4") + line.ToString("D2") + "*");
        //         return files.Count();
        //     }
        //     return 0;

        // }

        // public static void ChangeHHStatus(int ea_id, int hh_id, int status)
        // {
        //     var old = _context.LsnHhListings.FirstOrDefault(e => e.HhSerialNumber == hh_id && e.EaId == ea_id);
        //     var new_hh = old;
        //     new_hh.Status = status;

        //     _context.Entry(old).CurrentValues.SetValues(new_hh);
        //     _context.SaveChanges();
        // }
    }
}