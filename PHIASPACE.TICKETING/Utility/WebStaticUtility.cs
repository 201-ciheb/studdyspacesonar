using System.Security.Claims;

namespace PHIASPACE.TICKETING.Utility{
    public static class WebStaticUtility
    {
        public static string GetFullNameInitials(this ClaimsPrincipal user)
        {
            //check if user has more than one name
            var name = user.FindFirstValue(ClaimTypes.Name);
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