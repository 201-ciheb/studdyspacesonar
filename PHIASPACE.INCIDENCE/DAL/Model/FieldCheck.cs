using System;
using System.Collections.Generic;

namespace PHIASPACE.INCIDENCE.DAL.Model;

public partial class FieldCheck
{
    public int Id { get; set; }

    public string? TableId { get; set; }

    public string? TableName { get; set; }

    public string? TableDetail { get; set; }

    public string? TableHeader { get; set; }

    public string? Graph { get; set; }

    public string? TableJson { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
