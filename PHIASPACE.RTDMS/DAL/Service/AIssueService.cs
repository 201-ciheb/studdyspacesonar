using System;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;
using System.Linq;
using X.PagedList;
using X.PagedList.Extensions;
using PHIASPACE.RTDMS.DAL.MssqlDbService;
using System.Data;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;

namespace PHIASPACE.RTDMS.DAL.Service
{
    class AIssueService : IAIssueService
    {
        private readonly IAimsDbProvider _dbProvider;

        public AIssueService(IAimsDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<AimsIssue> AddIssue(AimsIssue issue)
        {
            var sqlQuery = "INSERT INTO aims_issue (issue_type_id, status_id, title, issue_content, date_created, opened_by, priority) " +
                           "VALUES (@IssueTypeId, @StatusId, @Title, @IssueContent, @DateCreated, @OpenedBy, @Priority);" +
                           "SELECT LAST_INSERT_ID();";

            issue.DateCreated = DateTime.Now;
            var parameters = new[]
            {
                _dbProvider.CreateParameter("@IssueTypeId", issue.IssueTypeId, DbType.Int32),
                _dbProvider.CreateParameter("@StatusId", issue.StatusId, DbType.Int32),
                _dbProvider.CreateParameter("@Title", issue.Title, DbType.String),
                _dbProvider.CreateParameter("@IssueContent", issue.IssueContent, DbType.String),
                _dbProvider.CreateParameter("@DateCreated", issue.DateCreated, DbType.DateTime),
                _dbProvider.CreateParameter("@OpenedBy", issue.OpenedBy, DbType.String),
                _dbProvider.CreateParameter("@Priority", issue.Priority, DbType.Int32)
            };

            issue.Id = await _dbProvider.ExecuteScalarAsync<int>(sqlQuery, _dbProvider.GetConnectionString(), parameters);
            return issue;
        }

        public async Task<int> AddDiscussion(AimsDiscussion discussion)
        {
            var sqlQuery = "INSERT INTO aims_discussion (issue_id, date_added, added_by, body) " +
                           "VALUES (@IssueId, @DateAdded, @AddedBy, @Body);" +
                           "SELECT LAST_INSERT_ID();";

            discussion.DateAdded = DateTime.Now;
            var parameters = new[]
            {
                _dbProvider.CreateParameter("@IssueId", discussion.IssueId, DbType.Int32),
                _dbProvider.CreateParameter("@DateAdded", discussion.DateAdded, DbType.DateTime),
                _dbProvider.CreateParameter("@AddedBy", discussion.AddedBy, DbType.String),
                _dbProvider.CreateParameter("@Body", discussion.Body, DbType.String)
            };

            return await _dbProvider.ExecuteScalarAsync<int>(sqlQuery, _dbProvider.GetConnectionString(), parameters);
        }

        public async Task<AimsIssue> GetIssue(int id)
        {
            var sqlQuery = "SELECT * FROM aims_issue WHERE id = @Id";
            var parameters = new[] { _dbProvider.CreateParameter("@Id", id, DbType.Int32) };
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString(), parameters);

            return dataTable.AsEnumerable().Select(row => new AimsIssue
            {
                Id = Convert.ToInt32(row["id"]),
                IssueTypeId = Convert.ToInt32(row["issue_type_id"]),
                StatusId = Convert.ToInt32(row["status_id"]),
                Title = row["title"].ToString(),
                IssueContent = row["issue_content"].ToString(),
                DateCreated = Convert.ToDateTime(row["date_created"]),
                OpenedBy = row["opened_by"].ToString(),
                Priority = Convert.ToInt32(row["priority"])
            }).FirstOrDefault();
        }

        public async Task AddPersonsToIssue(List<AimsLnkIssuePerson> persons)
        {
            var sqlQuery = "INSERT INTO aims_lnk_issue_person (issue_id, person_id) VALUES (@IssueId, @PersonId)";
            foreach (var person in persons)
            {
                var parameters = new[]
                {
                    _dbProvider.CreateParameter("@IssueId", person.IssueId, DbType.Int32),
                    _dbProvider.CreateParameter("@PersonId", person.PersonId, DbType.Int32)
                };
                await _dbProvider.ExecuteNonQueryAsync(sqlQuery, _dbProvider.GetConnectionString(), parameters);
            }
        }

        public async Task<IQueryable<AimsIssue>> GetIssues()
        {
            var sqlQuery = "SELECT * FROM aims_issue";
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString());

            var issues = dataTable.AsEnumerable().Select(row => new AimsIssue
            {
                Id = Convert.ToInt32(row["id"]),
                IssueTypeId = Convert.ToInt32(row["issue_type_id"]),
                StatusId = Convert.ToInt32(row["status_id"]),
                Title = row["title"].ToString(),
                IssueContent = row["issue_content"].ToString(),
                DateCreated = Convert.ToDateTime(row["date_created"]),
                OpenedBy = row["opened_by"].ToString(),
                Priority = Convert.ToInt32(row["priority"])
            }).AsQueryable();

            return issues;
        }

        public async Task<IQueryable<AimsDiscussion>> GetDiscussions(int issueId)
        {
            var sqlQuery = "SELECT * FROM aims_discussion WHERE issue_id = @IssueId";
            var parameters = new[] { _dbProvider.CreateParameter("@IssueId", issueId, DbType.Int32) };
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString(), parameters);

            var discussions = dataTable.AsEnumerable().Select(row => new AimsDiscussion
            {
                Id = Convert.ToInt32(row["id"]),
                IssueId = Convert.ToInt32(row["issue_id"]),
                DateAdded = Convert.ToDateTime(row["date_added"]),
                AddedBy = row["added_by"].ToString(),
                Body = row["body"].ToString()
            }).AsQueryable();

            return discussions;
        }

        public async Task UpdateIssue(AimsIssue issue)
        {
            var sqlQuery = "UPDATE aims_issue SET issue_type_id = @IssueTypeId, status_id = @StatusId, title = @Title, " +
                           "issue_content = @IssueContent, priority = @Priority WHERE id = @Id";

            var parameters = new[]
            {
                _dbProvider.CreateParameter("@IssueTypeId", issue.IssueTypeId, DbType.Int32),
                _dbProvider.CreateParameter("@StatusId", issue.StatusId, DbType.Int32),
                _dbProvider.CreateParameter("@Title", issue.Title, DbType.String),
                _dbProvider.CreateParameter("@IssueContent", issue.IssueContent, DbType.String),
                _dbProvider.CreateParameter("@Priority", issue.Priority, DbType.Int32),
                _dbProvider.CreateParameter("@Id", issue.Id, DbType.Int32)
            };

            await _dbProvider.ExecuteNonQueryAsync(sqlQuery, _dbProvider.GetConnectionString(), parameters);
        }

        public async Task<IQueryable<AimsIssue>> GetIssue(int ea_id, int hh_id)
        {
            var sqlQuery = "SELECT * FROM aims_issue WHERE ea_id = @EaId AND household_id = @HhId";
            var parameters = new[]
            {
                _dbProvider.CreateParameter("@EaId", ea_id, DbType.Int32),
                _dbProvider.CreateParameter("@HhId", hh_id, DbType.Int32)
            };

            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString(), parameters);

            var issues = dataTable.AsEnumerable().Select(row => new AimsIssue
            {
                Id = Convert.ToInt32(row["id"]),
                IssueTypeId = Convert.ToInt32(row["issue_type_id"]),
                StatusId = Convert.ToInt32(row["status_id"]),
                Title = row["title"].ToString(),
                IssueContent = row["issue_content"].ToString(),
                DateCreated = Convert.ToDateTime(row["date_created"]),
                OpenedBy = row["opened_by"].ToString(),
                Priority = Convert.ToInt32(row["priority"])
            }).AsQueryable();

            return issues;
        }

        public async Task<IQueryable<AimsIssue>> GetPersonIssuesAsync(int person_id)
        {
            var categoryQuery = "SELECT issue_type_id FROM aims_lnk_issue_type_to_person WHERE person_id = @PersonId";
            var issueIdQuery = "SELECT issue_id FROM aims_lnk_issue_person WHERE person_id = @PersonId";
            var issueQuery = "SELECT * FROM aims_issue WHERE issue_type_id IN (" + categoryQuery + ") AND id IN (" + issueIdQuery + ")";

            var parameters = new[] { _dbProvider.CreateParameter("@PersonId", person_id, DbType.Int32) };
            var dataTable = await _dbProvider.ExecuteQueryAsyncReturnDataTable(issueQuery, _dbProvider.GetConnectionString(), parameters);

            var issues = dataTable.AsEnumerable().Select(row => new AimsIssue
            {
                Id = Convert.ToInt32(row["id"]),
                IssueTypeId = Convert.ToInt32(row["issue_type_id"]),
                StatusId = Convert.ToInt32(row["status_id"]),
                Title = row["title"].ToString(),
                IssueContent = row["issue_content"].ToString(),
                DateCreated = Convert.ToDateTime(row["date_created"]),
                OpenedBy = row["opened_by"].ToString(),
                Priority = Convert.ToInt32(row["priority"])
            }).AsQueryable();

            return issues;
        }

        public Task<IPagedList<AimsIssue>> GetFilteredIssues(
       string query, DateTime? startDate, DateTime? endDate,
       int? categoryId, int? searchStatusId, int pageIndex, int pageSize)
        {
            var sqlQuery = "SELECT * FROM aims_issue WHERE " +
                           "(title LIKE @Query OR issue_content LIKE @Query OR county_id IN " +
                           "(SELECT id FROM aims_county WHERE county_name LIKE @Query) " +
                           "OR lab_id IN (SELECT id FROM lb_lab WHERE lab_name LIKE @Query)) " +
                           "AND (@StartDate IS NULL OR date_created >= @StartDate) " +
                           "AND (@EndDate IS NULL OR date_created <= @EndDate) " +
                           "AND (@CategoryId IS NULL OR issue_type_id = @CategoryId) " +
                           "AND (@SearchStatusId IS NULL OR county_id = @SearchStatusId) " +
                           "ORDER BY date_created DESC";

            var parameters = new[]
            {
        _dbProvider.CreateParameter("@Query", $"%{query}%", DbType.String),
        _dbProvider.CreateParameter("@StartDate", startDate ?? (object)DBNull.Value, DbType.DateTime),
        _dbProvider.CreateParameter("@EndDate", endDate ?? (object)DBNull.Value, DbType.DateTime),
        _dbProvider.CreateParameter("@CategoryId", categoryId ?? (object)DBNull.Value, DbType.Int32),
        _dbProvider.CreateParameter("@SearchStatusId", searchStatusId ?? (object)DBNull.Value, DbType.Int32)
    };

            var dataTable = _dbProvider.ExecuteQueryAsyncReturnDataTable(sqlQuery, _dbProvider.GetConnectionString(), parameters).Result;

            var issues = dataTable.AsEnumerable().Select(row => new AimsIssue
            {
                Id = Convert.ToInt32(row["id"]),
                IssueTypeId = Convert.ToInt32(row["issue_type_id"]),
                StatusId = Convert.ToInt32(row["status_id"]),
                Title = row["title"].ToString(),
                IssueContent = row["issue_content"].ToString(),
                DateCreated = Convert.ToDateTime(row["date_created"]),
                OpenedBy = row["opened_by"].ToString(),
                Priority = Convert.ToInt32(row["priority"])
            }).AsQueryable();

            return Task.FromResult(issues.ToPagedList(pageIndex, pageSize));
        }
    }
}