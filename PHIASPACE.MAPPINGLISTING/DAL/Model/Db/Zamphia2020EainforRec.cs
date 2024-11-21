using System;
using System.Collections.Generic;

namespace PHIASPACE.MAPPINGLISTING.DAL.Model.Db;

public partial class Zamphia2020EainforRec
{
    public string? EainforId { get; set; }

    public string? TeamLeadid { get; set; }

    public string? NetworkRank { get; set; }

    public string? Bestnet { get; set; }

    public string? Midnet { get; set; }

    public string? Worstnet { get; set; }

    public string? Altnet { get; set; }

    public string? Languages { get; set; }

    public string? Roads { get; set; }

    public string? Accomodation { get; set; }

    public string? Floodrisk { get; set; }

    public string? Police { get; set; }

    public string? Electricity { get; set; }

    public string? Otherlang { get; set; }

    public string? Notes { get; set; }

    public string CaseNumber { get; set; } = null!;

    public string? FileName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
