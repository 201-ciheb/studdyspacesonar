using System;
using System.Collections.Generic;

namespace PHIASPACE.INCIDENCE.DAL.Model;

public partial class AimsLnkTeamPerson
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int TeamId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public string? TmRole { get; set; }

    public int Active { get; set; }
}
