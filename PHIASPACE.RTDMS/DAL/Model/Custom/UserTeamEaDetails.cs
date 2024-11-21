namespace PHIASPACE.RTDMS.DAL.Model.Custom;

public class UserTeamEaDetails
{
    // public string Username { get; set; }
    public decimal Team { get; set; }
    // public decimal Role { get; set; }
    // public decimal Sex { get; set; }
    public decimal Cluster { get; set; }
    public string Region { get; set; }
    public string Zone { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public int DuplicatePtids { get; set; }
    public long HouseHoldsVisited { get; set; }
    public long HouseHoldsCompleted { get; set; }
    public long HouseHoldsRefused { get; set; }
}
