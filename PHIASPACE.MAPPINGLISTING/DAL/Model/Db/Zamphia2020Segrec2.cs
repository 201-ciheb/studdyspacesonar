using System;
using System.Collections.Generic;

namespace PHIASPACE.MAPPINGLISTING.DAL.Model.Db;

public partial class Zamphia2020Segrec2
{
    public string? Scluster { get; set; }

    public string? Segnum { get; set; }

    public string? Shhnumb { get; set; }

    public string? Scummul { get; set; }

    public string? Spercent { get; set; }

    public string? Smore { get; set; }

    public string CaseNumber { get; set; } = null!;

    public string? FileName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
