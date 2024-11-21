namespace PHIASPACE.INCIDENCE.DAL.Model
{
    public partial class AimsSubCounty
    {
        public int Id { get; set; }

        public string SubCountyCode { get; set; } = null!;
        public string? SubCountyName { get; set; }

        public int CountyCode { get; set; }
    }
}
