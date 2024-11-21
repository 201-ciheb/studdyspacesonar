using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;

namespace PHIASPACE.RTDMS.DAL.Service
{
    public class ZoneService : IZoneService
    {
        private readonly IAimsDbProvider _dbProvider;

        public ZoneService(IAimsDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public int GetUserZone(string username)
        {
            // Placeholder return, modify as needed
            return 0;
            // Implementation can be added if required.
        }

        public IQueryable<AimsZone> GetZones()
        {
            var sqlQuery = "SELECT id, name, code FROM aims_zone";  
            var dataTable = _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString()).Result;

            return dataTable.AsEnumerable().Select(row => new AimsZone
            {
                Id = Convert.ToInt32(row["id"]),
                Name = row["name"].ToString(),
                Code = row["code"].ToString()
            }).AsQueryable();
        }


        public async Task<List<AimsCounty>> GetCountiesAsync()
        {
            var sqlQuery = "SELECT * FROM aims_county";
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());

            return dataTable.AsEnumerable().Select(row => new AimsCounty
            {
                CountyCode = row["county_code"].ToString(),
                GeoPolicticalRegion = row["geo_polictical_region"].ToString(),
                CountyName = row["county_name"].ToString(),
                Id = Convert.ToInt32(row["id"]),
                ZoneId = (int)(row["ZoneId"] != DBNull.Value ? Convert.ToInt32(row["ZoneId"]) : (int?)null)
            }).ToList();
        }


        public async Task<List<RtdmsIssueType>> GetIssueTypes()
        {
            var sqlQuery = "SELECT * FROM rtdms_issue_type";
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());

            return dataTable.AsEnumerable().Select(row => new RtdmsIssueType
            {
                Id = Convert.ToInt32(row["id"]),
                Name = row["name"].ToString() ?? string.Empty
            }).ToList();
        }


        public async Task<List<RtdmsIssueStatus>> GetIssueStatuses()
        {
            var sqlQuery = "SELECT * FROM rtdms_issue_status";
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());

            var statuses = dataTable.AsEnumerable().Select(row => new RtdmsIssueStatus
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString() ?? string.Empty
            }).ToList();

            return statuses;
        }


        public IQueryable<AimsLookup> GetLookupOptions(string category)
        {
            var sqlQuery = $"SELECT * FROM aims_lookup WHERE Category = '{category}' AND (Deleted IS NULL OR Deleted = 0)";
            var dataTable = _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString()).Result;

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine($"No lookup options found for category: {category}");
                return new List<AimsLookup>().AsQueryable();
            }

            return dataTable.AsEnumerable().Select(row => new AimsLookup
            {
                Id = Convert.ToInt32(row["id"]),
                LookupName = row["lookup_name"].ToString() ?? string.Empty,
                Orderd = row["orderd"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["orderd"]),
                Category = row["category"].ToString() ?? string.Empty,
                Deleted = row["deleted"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["deleted"])
            }).AsQueryable();
        }

        public IQueryable<AimsValueSet> GetValueSets()
        {
            var sqlQuery = "SELECT * FROM aims_value_set";
            var dataTable = _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString()).Result;

            return dataTable.AsEnumerable().Select(row => new AimsValueSet
            {
                Id = row.Field<int>("Id"),
                ItemName = row.Field<string>("ItemName"),
                TableName = row.Field<string>("TableName"),
                SpName = row.Field<string>("SpName"),
                Value = row.Field<string>("Value"),
                ValueId = row.Field<int>("ValueId"),
                ValuesetType = row.Field<string>("ValuesetType"),
                Label = row.Field<string>("Label"),
                RecordId = row.Field<int>("RecordId"),
                CountParameter = row.Field<string>("CountParameter")
            }).AsQueryable();
        }


        public async Task<List<LbLab>> GetLabsAsync()
        {
            var sqlQuery = "SELECT * FROM lb_lab WHERE delete_flag = 0 ORDER BY lab_name";
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());

            return dataTable.AsEnumerable().Select(row => new LbLab
            {
                Id = row.Field<string>("id"),
                LabName = row.Field<string>("lab_name"),
                LocationId = row.Field<int>("location_id"),
                DeleteFlag = row.Field<int>("delete_flag"),
                Ldms = row.Field<string>("ldms"),
                KenphiaCode = row.Field<string>("kenphia_code"),
                CountyCode = row.Field<string>("county_code")
            }).ToList();
        }


        public IQueryable<LbLab> GetLabs()
        {
            var sqlQuery = "SELECT * FROM lb_lab WHERE delete_flag = 0 ORDER BY lab_name";
            var dataTable = _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString()).Result;

            return dataTable.AsEnumerable().Select(row => new LbLab
            {
                Id = row.Field<string>("id"),
                LabName = row.Field<string>("lab_name"),
                LocationId = row.Field<int>("location_id"),
                DeleteFlag = row.Field<int>("delete_flag"),
                Ldms = row.Field<string>("ldms"),
                KenphiaCode = row.Field<string>("kenphia_code"),
                CountyCode = row.Field<string>("county_code")
            }).AsQueryable();
        }

        // Not implemented methods, throw exceptions for now
        public List<string> GetPersonRoles(string email)
        {
            throw new NotImplementedException();
        }

        public AimsPerson GetTeamLead(int cluster)
        {
            throw new NotImplementedException();
        }
    }
}
