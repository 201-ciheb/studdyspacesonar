using System;
using PHIASPACE.RTDMS.DAL.IService;
using System.Linq;
using PHIASPACE.RTDMS.DAL.MssqlDbService;

public class IssueService : IIssueService
{
    static readonly AimsMssqlDbContext _entity = new AimsMssqlDbContext();


    // public RtdmsIssue AddIssue(RtdmsIssue issue)
    // {
    //     issue.DateCreated = DateTime.Now;
    //     _entity.RtdmsIssues.Add(issue);
    //     _entity.SaveChanges();
    //     return issue;
    // }

    // public int AddDiscussion(RtdmsDiscussion discussion)
    // {
    //     discussion.DateAdded = DateTime.Now;
    //     _entity.RtdmsDiscussions.Add(discussion);
    //     _entity.SaveChanges();
    //     return discussion.Id;
    // }

    // public RtdmsIssue GetIssue(int id)
    // {
    //     return _entity.RtdmsIssues.FirstOrDefault(e => e.Id == id);
    // }

    // public RtdmsPmIssue GetPMIssue(int id) // Corrected return type
    // {
    //     return _entity.RtdmsPmIssues.FirstOrDefault(e => e.Id == id);
    // }

    // public IQueryable<RtdmsIssue> GetIsssues()
    // {
    //     return _entity.RtdmsIssues.AsQueryable();
    // }

    // public IQueryable<RtdmsDiscussion> GetDiscussions(int issue_id, int type_id)
    // {
    //     return _entity.RtdmsDiscussions.Where(e => e.IssueId == issue_id && e.IsType == type_id);
    // }

    // public RtdmsPmIssue AddPMIssue(RtdmsPmIssue issue) // Corrected parameter and return type
    // {
    //     issue.DateCreated = DateTime.Now;
    //     _entity.RtdmsPmIssues.Add(issue);
    //     _entity.SaveChanges();
    //     return issue;
    // }

    // public IQueryable<RtdmsPmIssue> GetPMIsssues() // Corrected return type
    // {
    //     return _entity.RtdmsPmIssues.AsQueryable();
    // }

    // public void Update(RtdmsIssue issue)
    // {
    //     var old = _entity.RtdmsIssues.FirstOrDefault(e => e.Id == issue.Id);
    //     if (old != null)
    //     {
    //         _entity.Entry(old).CurrentValues.SetValues(issue);
    //         _entity.SaveChanges();
    //     }
    // }

    // public void UpdatePM(RtdmsPmIssue issue) // Corrected parameter type
    // {
    //     var old = _entity.RtdmsPmIssues.FirstOrDefault(e => e.Id == issue.Id);
    //     if (old != null)
    //     {
    //         _entity.Entry(old).CurrentValues.SetValues(issue);
    //         _entity.SaveChanges();
    //     }
    // }
}
