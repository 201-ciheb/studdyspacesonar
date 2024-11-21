using System.Security.Claims;

namespace PHIASPACE.LINKAGE.Utils;
public static class ViewUtility
{
    public static string GetFullNameInitials(this ClaimsPrincipal user)
    {
        if (user == null)
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
        else if (name.Length > 1)
        {
            return name[0].ToString() + "" + name[1].ToString();
        }
        else
        {
            return name[0].ToString();
        }
    }

    public static string GetFullName(this ClaimsPrincipal user)
    {
        if (user == null)
            return string.Empty;
        string name = user.FindFirstValue(ClaimTypes.Name);
        return name;
    }

    public static string GetEmailAddress(this ClaimsPrincipal user)
    {
        if (user == null)
            return string.Empty;
        string email = user.FindFirstValue(ClaimTypes.Email);
        return email;
    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        if (user == null)
            return string.Empty;
        string username = user.FindFirstValue("preferred_username");
        return username;
    }

    public static string GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
            return string.Empty;
        string id = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return id;
    }

    public static string FormatDateAsPassedPhrase(DateTime? inputDate)
    {
        if (inputDate == null)
        {
            return "---";
        }
        DateTime currentDate = DateTime.Now;
        TimeSpan timeDifference = currentDate - (DateTime)inputDate;

        if (timeDifference.TotalDays >= 365)
        {
            int yearsDifference = (int)(timeDifference.TotalDays / 365);
            return yearsDifference == 1 ? "1 year ago" : $"{yearsDifference} years ago";
        }
        else if (timeDifference.TotalDays >= 30)
        {
            int monthsDifference = (int)(timeDifference.TotalDays / 30);
            return monthsDifference == 1 ? "1 month ago" : $"{monthsDifference} months ago";
        }
        else if (timeDifference.TotalDays >= 7)
        {
            int weeksDifference = (int)(timeDifference.TotalDays / 7);
            return weeksDifference == 1 ? "1 week ago" : $"{weeksDifference} weeks ago";
        }
        else if (timeDifference.TotalDays >= 1)
        {
            int daysDifference = (int)timeDifference.TotalDays;
            return daysDifference == 1 ? "1 day ago" : $"{daysDifference} days ago";
        }
        else if (timeDifference.TotalHours >= 1)
        {
            int hoursDifference = (int)timeDifference.TotalHours;
            return hoursDifference == 1 ? "1 hour ago" : $"{hoursDifference} hours ago";
        }
        else if (timeDifference.TotalMinutes >= 1)
        {
            int minutesDifference = (int)timeDifference.TotalMinutes;
            return minutesDifference == 1 ? "1 minute ago" : $"{minutesDifference} minutes ago";
        }
        else
        {
            return "Just now";
        }
    }

    public static string GetVariableValue<T>(this List<T> list, string key)
    {
        if (list != null)
        {
            var item = list.FirstOrDefault(m => m.GetType().GetProperty("Key")?.GetValue(m)?.ToString() == key);

            if (item != null)
            {
                var valueProperty = item.GetType().GetProperty("Value");
                if (valueProperty != null)
                {
                    var value = valueProperty.GetValue(item)?.ToString();
                    return value ?? "---";
                }
            }
        }

        return "---";
    }
}
