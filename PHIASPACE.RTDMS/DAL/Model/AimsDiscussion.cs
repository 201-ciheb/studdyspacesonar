using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsDiscussion
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public DateTime DateAdded { get; set; }

    public string AddedBy { get; set; } = null!;

    public string Body { get; set; } = null!;

    public virtual AimsIssue Issue { get; set; } = null!;
}