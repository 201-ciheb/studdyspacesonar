using System;
using System.Collections.Generic;

namespace PHIASPACE.INCIDENCE.DAL.Model;

public partial class LbLab
{
    public string Id { get; set; } = null!;

    public string LabName { get; set; } = null!;

    public int? LocationId { get; set; }

    public int DeleteFlag { get; set; }

    public string? Ldms { get; set; }

    public string KenphiaCode { get; set; } = null!;

    public string? CountyCode { get; set; }
}
