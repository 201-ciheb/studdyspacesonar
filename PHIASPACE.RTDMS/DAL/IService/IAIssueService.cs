using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PHIASPACE.RTDMS.DAL.Model;
using X.PagedList;

namespace PHIASPACE.RTDMS.DAL.IService
{
    public interface IAIssueService
    {
        Task<AimsIssue> AddIssue(AimsIssue issue);
        Task<int> AddDiscussion(AimsDiscussion discussion);
        Task<AimsIssue> GetIssue(int id);
        Task<IQueryable<AimsIssue>> GetIssues();
        Task<IQueryable<AimsIssue>> GetIssue(int ea_id, int hh_id);
        Task<IQueryable<AimsDiscussion>> GetDiscussions(int issueId);
        Task UpdateIssue(AimsIssue issue);
        Task AddPersonsToIssue(List<AimsLnkIssuePerson> persons);
        Task<IPagedList<AimsIssue>> GetFilteredIssues(string query, DateTime? startDate, DateTime? endDate, int? categoryId, int? searchStatusId, int pageIndex, int pageSize);
    }
}
