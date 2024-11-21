using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIASPACE.RTDMS.DAL.Model;

[Table("aims_lnk_team_ea")]
public partial class AimsLnkTeamEa
{
    [Column("id")]
    public int Id { get; set; }
    [Column("team_id")]
    public int TeamId { get; set; }
    [Column("ea_id")]
    public int EaId { get; set; }
    [Column("active")]
    public int Active { get; set; }

    public virtual AimsEa Ea { get; set; } = null!;

    public virtual AimsTeam Team { get; set; } = null!;
}