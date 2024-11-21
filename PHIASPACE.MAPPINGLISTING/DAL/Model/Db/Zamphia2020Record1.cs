using System;
using System.Collections.Generic;

namespace PHIASPACE.MAPPINGLISTING.DAL.Model.Db;

public partial class Zamphia2020Record1
{
    public string? Lcluster { get; set; }

    public string? Ltleadnum { get; set; }

    public string? Lprovince { get; set; }

    public string? Ldistrict { get; set; }

    public string? Lconst { get; set; }

    public string? Lward { get; set; }

    public string? Ltype { get; set; }

    public string? Ldate { get; set; }

    public string? Lhouseholds { get; set; }

    public string? Lsegnum { get; set; }

    public string? Lexpect { get; set; }

    public string CaseNumber { get; set; } = null!;

    public string? FileName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
