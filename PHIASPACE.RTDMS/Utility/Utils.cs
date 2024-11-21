using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.Model;
using PHIASPACE.RTDMS.DAL.MssqlDbService;

namespace PHIASPACE.RTDMS.Utility
{
    public static class Utils
    {
        private static IServiceScopeFactory? _serviceScopeFactory;

        public static void Initialize(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        // Method to use the DbContext within a scope
        private static void UseDbContext(Action<AimsMssqlDbContext> action)
        {
            if (_serviceScopeFactory == null)
                throw new InvalidOperationException("ServiceScopeFactory not initialized.");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AimsMssqlDbContext>();
                action(dbContext);
            }
        }

        public static string GetColorByPercentage(string value)
        {
            try
            {
                var count = Convert.ToDouble(value);
                if (count > 0 && count < 100)
                    return "yellow";
                else if (count == 100)
                    return "green";
                else if (count > 100)
                    return "red";
                else
                    return "gray";
            }
            catch (Exception)
            {
                return "gray";
            }
        }

        public static string GetColorByCompletionStatus(string value)
        {
            try
            {
                var count = Convert.ToInt32(value);
                if (count == 1)
                    return "green";
                else if (count == 0)
                    return "yellow";
                else
                    return "red";
            }
            catch (Exception)
            {
                return "gray";
            }
        }

        public static DataTable Transpose(DataTable dt)
        {
            DataTable dtNew = new DataTable();

            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(i.ToString());
            }

            for (int k = 0; k < dt.Columns.Count; k++)
            {
                DataRow r = dtNew.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                    r[j] = dt.Rows[j - 1][k];
                dtNew.Rows.Add(r);
            }

            return dtNew;
        }

        public static string GetColorByPercentageLow(string value)
        {
            try
            {
                var count = Convert.ToDouble(value);
                if (count > 50 && count < 90)
                    return "bg-orange-400";
                else if (count > 90 && count < 100)
                    return "bg-green-400";
                else if (count < 50)
                    return "bg-danger-400";
                else
                    return "bg-slate-400";
            }
            catch (Exception)
            {
                return "bg-slate-400";
            }
        }

        public static string RelativeDate(string value)
        {
            try
            {
                var yourDate = Convert.ToDateTime(value);
                var ts = new TimeSpan(DateTime.UtcNow.Ticks - yourDate.Ticks);
                double delta = Math.Abs(ts.TotalSeconds);

                if (delta < 60)
                    return ts.Seconds == 1 ? "one sec ago" : ts.Seconds + " secs ago";
                if (delta < 120)
                    return "a min ago";
                if (delta < 2700)
                    return ts.Minutes + " mins ago";
                if (delta < 5400)
                    return "an hr ago";
                if (delta < 86400)
                    return ts.Hours + " hrs ago";
                if (delta < 172800)
                    return "yesterday";
                if (delta < 2592000)
                    return ts.Days + " days ago";
                if (delta < 31104000)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "one month ago" : months + " months ago";
                }
                else
                {
                    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                    return years <= 1 ? "one year ago" : years + " years ago";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        // public static IQueryable<AimsLookup> GetLookupOptions(string category)
        // {
        //     return ExecuteDbContext(dbContext => dbContext.AimsLookups.Where(e => e.Category == category));
        // }

        // public static IQueryable<RtdmsIssueType> GetIssueTypes()
        // {
        //     return ExecuteDbContext(dbContext => dbContext.RtdmsIssueTypes);
        // }

        //public static async Task<IQueryable<AimsCounty>> GetCountiesAsync()
        //{
        //    return await ExecuteDbContextAsync(async dbContext =>
        //    {
        //        var county = await dbContext.AimsCountys.ToListAsync();
        //        return county.AsQueryable();
        //    });
        //}

        // public static IQueryable<RtdmsIssueStatus> GetIssueStatuses()
        // {
        //     return ExecuteDbContext(dbContext => dbContext.RtdmsIssueStatuses);
        // }

        // public static List<AimsLnkFormAge> GetFormLink()
        // {
        //     return ExecuteDbContext(dbContext => dbContext.AimsLnkFormAges.ToList());
        // }

        // public static List<string> PersonRoles(string email)
        // {
        //     return ExecuteDbContext(dbContext =>
        //     {
        //         var person = dbContext.AimsPersons
        //             .Include(p => p.AimsPersonRoles)
        //             .ThenInclude(pr => pr.Role)
        //             .FirstOrDefault(p => p.Email == email);

        //         return person?.AimsPersonRoles.Select(pr => pr.Role.Code).ToList() ?? new List<string>();
        //     });
        // }

        // public static IQueryable<aims_tree_data> GetTreeData()
        // {
        //     return ExecuteDbContext(dbContext =>
        //     {
        //         var data = dbContext.Aims_Tree_Datas;
        //         if (!data.Any())
        //         {
        //             Console.WriteLine("No data found in Aims_Tree_Datas.");
        //         }
        //         return data;
        //     });
        // }

        // public static IQueryable<AimsEa> GetEa()
        // {
        //     return ExecuteDbContext(dbContext => dbContext.AimsEas);
        // }

        // public static IQueryable<AimsZone> GetZones()
        // {
        //     return ExecuteDbContext(dbContext => dbContext.AimsZones);
        // }

        // public static int GetUserZone(string username)
        // {
        //     return ExecuteDbContext(dbContext =>
        //     {
        //         var link = dbContext.AimsLnkZonePersons.FirstOrDefault(e => e.Username == username);
        //         return link != null ? Convert.ToInt32(link.Zone) : 0;
        //     });
        // }

        public static int ConvertObjectToInt(object value)
        {
            return int.TryParse(value.ToString(), out var val) ? val : 0;
        }

        //public static IQueryable<AimsValueSet> GetValueSets()
        //{
        //    return ExecuteDbContext(dbContext => dbContext.AimsValueSets);
        //}

        // Helper method for synchronous database operations
        // private static TResult ExecuteDbContext<TResult>(Func<RtdmsDbContext, TResult> func)
        // {
        //     if (_serviceScopeFactory == null)
        //         throw new InvalidOperationException("ServiceScopeFactory not initialized.");

        //     using (var scope = _serviceScopeFactory.CreateScope())
        //     {
        //         var dbContext = scope.ServiceProvider.GetRequiredService<RtdmsDbContext>();
        //         return func(dbContext);
        //     }
        // }


        // Helper method for asynchronous database operations
        // private static async Task<TResult> ExecuteDbContextAsync<TResult>(Func<RtdmsDbContext, Task<TResult>> func)
        // {
        //     if (_serviceScopeFactory == null)
        //         throw new InvalidOperationException("ServiceScopeFactory not initialized.");

        //     using (var scope = _serviceScopeFactory.CreateScope())
        //     {
        //         var dbContext = scope.ServiceProvider.GetRequiredService<RtdmsDbContext>();
        //         return await func(dbContext);
        //     }
        // }
    }
}
