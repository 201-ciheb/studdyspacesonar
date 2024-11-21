namespace PHIASPACE.RTDMS.DAL.Model.Custom;

public class EaHhStatistics
{
    public int HHNumber { get; set; }
    public string HHName { get; set; }
    public int Cluster { get; set; }
    public int Rostered { get; set; }
    public int Eligible { get; set; }
    public int ConsentedInterview { get; set; }
    public int RefusedInterview { get; set; }
    public int Interviewed { get; set; }
    public int PTIDs { get; set; }
    public int ConsentedBloodDraw { get; set; }
    public int BloodDrawn { get; set; }
    public int ConsentedToBeContacted { get; set; }
    public int ContactInfoAvailable { get; set; }
    public int ChildrenEligible { get; set; }
    public int ChildrenWithBloodDraw { get; set; }
    public DateTime DateOpened { get; set; }
    public DateTime DateUploaded { get; set; }
}
