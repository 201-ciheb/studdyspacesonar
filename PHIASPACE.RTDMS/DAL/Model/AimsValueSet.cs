using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsValueSet
{
    public int Id { get; set; }

    public string? ItemName { get; set; }

    public string? TableName { get; set; }

    public string? SpName { get; set; }

    public string? Value { get; set; }

    public int? ValueId { get; set; }

    public string? ValuesetType { get; set; }

    public string? Label { get; set; }

    public int? RecordId { get; set; }

    public string? CountParameter { get; set; }
}
