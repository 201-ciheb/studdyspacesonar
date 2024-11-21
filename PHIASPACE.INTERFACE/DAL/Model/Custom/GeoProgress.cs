namespace PHIASPACE.INTERFACE.DAL.Model.Custom;

public class GeoProgress{
    public string Facility { get; set; }
    public string County { get; set; }
    public string Kmfl { get; set; }
    public double FinalGeoLat { get; set; }
    public double FinalGeoLon { get; set; }
    public int Status { get; set; }
}