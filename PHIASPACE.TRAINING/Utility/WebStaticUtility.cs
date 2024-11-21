using System.Security.Claims;

namespace PHIASPACE.TRAINING.Utility{
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
    }
}