using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsRecordSet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Label { get; set; }

    public string? BaseQuery { get; set; }

    public int? SortOrder { get; set; }
}
