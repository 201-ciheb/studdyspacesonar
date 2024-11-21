namespace PHIASPACE.INTERFACE.DAL.Model.Custom;
public class TeamPerformance{
    public string Team { get; set; }
    public int Target { get; set; }
    public int Assessed { get; set; }
    public int Today { get; set; }
    public int OffTarget { get; set; }
}