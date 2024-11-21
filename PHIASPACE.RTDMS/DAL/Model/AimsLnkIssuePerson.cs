using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsLnkIssuePerson
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public int PersonId { get; set; }

    public virtual AimsIssue Issue { get; set; } = null!;

    public virtual AimsPerson Person { get; set; } = null!;
}