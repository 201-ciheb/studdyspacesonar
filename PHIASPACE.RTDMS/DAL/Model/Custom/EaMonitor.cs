namespace PHIASPACE.RTDMS.DAL.Model.Custom;

public class EaMonitor
{
    public string EACode { get; set; }
    public string State { get; set; }
    public string LGA { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateCompleted { get; set; }
    public int DurationDays { get; set; }
    public int HHExpected { get; set; }
    public int HHCompleted { get; set; }
    public int HHPostponed { get; set; }
    public int HHRefused { get; set; }
    public int HHOtherReasons { get; set; }
    public double CompletionPercentage { get; set; }
    public int IndividualsEligibleForInterview { get; set; }
    public double ResponseRate { get; set; }
    public int AdultEligibleForInterview { get; set; }
    public int AdultInterviewsCompleted { get; set; }
    public int AdultInterviewsPostponed { get; set; }
    public int AdultInterviewsRefused { get; set; }
    public int AdultInterviewsOtherReasons { get; set; }
    public bool AdultInterviewCompleted { get; set; }
    public int AdolescentsEligibleForInterview { get; set; }
    public int AdolescentsInterviewsCompleted { get; set; }
    public int AdolescentsInterviewsPostponed { get; set; }
    public int AdolescentsInterviewsRefused { get; set; }
    public int AdolescentsInterviewsOtherReasons { get; set; }
    public bool AdolescentInterviewCompleted { get; set; }
    public int ChildEligibleForInterview { get; set; }
    public int ChildInterviewCompleted { get; set; }
}
