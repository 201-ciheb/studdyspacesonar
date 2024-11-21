using System;
using PHIASPACE.RTDMS.DAL.Model;

namespace DataAccess.Models
{
    public class Issue
    {

        public Issue(AimsIssue issue)
        {
            id = issue.Id;
            issue_type_id = issue.IssueTypeId;
            status_id = issue.StatusId;
            title = issue.Title;
            issue_content = issue.IssueContent;

            if (issue.HouseholdId.HasValue)
                household_id = issue.HouseholdId.Value;

            if (issue.EaId.HasValue)
                ea_id = issue.EaId.Value;

            if (issue.TeamId.HasValue)
                team_id = issue.TeamId.Value;

            if (issue.UpdatedAt.HasValue)
                updated_at = issue.UpdatedAt.Value;

            if (issue.LabId.HasValue)
                lab_id = issue.LabId.Value;

            if (issue.CountyId.HasValue)
                state_id = issue.CountyId.Value;

            impact = issue.Impact;
            resolution = issue.Resolution;
            priority = issue.Priority;
        } 


        public int id { get; set; }
        public int issue_type_id { get; set; }
        public int status_id { get; set; }
        public string title { get; set; }
        public string issue_content { get; set; }
        public int household_id { get; set; }
        public int team_id { get; set; }
        public string impact { set; get; }
        public string resolution { set; get; }
        public int priority { set; get; }
        public System.DateTime date_created { get; set; }
        public string opened_by { get; set; }
        public int ea_id { get; set; }
        public System.DateTime updated_at { get; set; }
        public int state_id { set; get; }
        public Guid lab_id { set; get; }

    }


}


