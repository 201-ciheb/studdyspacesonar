using System;
using System.Collections.Generic;

namespace PHIASPACE.INCIDENCE.DAL.Model;

public partial class AimsLookup
{
    public int Id { get; set; }

    public string? LookupName { get; set; }

    public int? Orderd { get; set; }

    public string? Category { get; set; }

    public int? Deleted { get; set; }
}
