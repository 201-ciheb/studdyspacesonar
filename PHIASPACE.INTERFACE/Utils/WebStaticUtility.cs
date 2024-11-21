using System.Security.Claims;
using PHIASPACE.INTERFACE.DAL.Model.Custom;
using PHIASPACE.INTERFACE.Models;

namespace PHIASPACE.INTERFACE.Utils{
    public static class WebStaticUtility
    {
        public static string GetFullNameInitials(this ClaimsPrincipal user)
        {
            if(user == null)
                return "__";
            //check if user has more than one name
            string name = user.FindFirstValue(ClaimTypes.Name);
            //var name = user.FindFirstValue("name");
            var names = name.Split(" ");
            if (names.Length > 1)
            {
                //user has more than 1 name
                return names[0][0].ToString() + names[1][0].ToString();
            }
            else if(name.Length > 1)
            {
                return name[0].ToString() + "" + name[1].ToString();
            }
            else
            {
                return name[0].ToString();
            }
        }

        public static ScoringResult CalculateScore(this SatLabApiResponse apiResponse, List<ScoreMap> mapping)
        {
            //return a dictionary of variables found and score or zero if it was not found
            Dictionary<string,double> dic = new Dictionary<string, double>();
            var scoringResult = new ScoringResult();
            double totalScore = 0;
            var properties = typeof(SatLabApiResponse).GetProperties();
            foreach (var item in properties)
            {
                var found = mapping.FirstOrDefault(m => m.VariableName.Replace("/","") == item.Name);
                if(found != null){
                    //check the answer 1 = Yes, 0 = No
                    var value = item.GetValue(apiResponse);
                    var option = new string[]{"2","3","4"};
                    if((string)value == "1"){
                        totalScore = totalScore + found.Score;
                        dic.Add(item.Name, found.Score);
                    }                    
                }
            }
            scoringResult.Details = dic;
            scoringResult.TotalScore = totalScore;
            return scoringResult;
        }

        public static Dictionary<string,string> GetVariableResult(this SatLabApiResponse apiResponse, List<string> mapping)
        {
            Dictionary<string,string> dic = new Dictionary<string, string>();
            var scoringResult = new ScoringResult();
            double totalScore = 0;
            var properties = typeof(SatLabApiResponse).GetProperties();
            foreach (var item in properties)
            {
                var found = mapping.FirstOrDefault(m => m.Replace("/","") == item.Name);
                if(found != null){
                    var value = item.GetValue(apiResponse);
                    dic.Add(item.Name, (string)value);                
                }
            }
            return dic;
        }
    }
}