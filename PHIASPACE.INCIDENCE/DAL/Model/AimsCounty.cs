using System;
using System.Collections.Generic;

namespace PHIASPACE.INCIDENCE.DAL.Model;

public partial class AimsCounty
{
    public int Id { get; set; }

    public string CountyCode { get; set; } = null!;

    public string? GeoPolicticalRegion { get; set; }

    public string? CountyName { get; set; }

    public int ZoneId { get; set; }
}
