using Microsoft.AspNetCore.Mvc.Rendering;
using PHIASPACE.INCIDENCE.DAL.Model;

namespace PHIASPACE.INCIDENCE.Models
{
    public class IncidentModel
    {
        public int Id { get; set; }
        public string FieldWorkArea { get; set; }  
        public int CountyId { get; set; }
        public List<SelectListItem> Counties { get; set; }
        public int SubCounty { get; set; }
        public string SiteofIncidence { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string Description { get; set; }
        public string HHsAffected { get; set; } 
        public string PTIDSAffected { get; set; }
        public string PersonsInvolved {  get; set; }
        public int SurveyPhaseId { set; get; }    
        public int EventCategoryId { get; set; }
        public string ImmediateAction { get; set; }
        public string OrganisationsReportedTo { get; set; }
        public string EventCause { get; set; }  
        public string CorrectiveActions { get; set; }
        public int Deleted { get; set; } 
    }
}
