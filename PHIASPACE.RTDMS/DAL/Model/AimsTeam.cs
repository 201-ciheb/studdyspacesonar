using PHIASPACE.RTDMS.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace PHIASPACE.RTDMS.DAL.Model;

[Table("aims_team")]
public partial class AimsTeam
{
    public AimsTeam()
    {
        this.AimsLnkTeamEas = new HashSet<AimsLnkTeamEa>();
        this.AimsLnkTeamPeople = new HashSet<AimsLnkTeamPerson>();
    }

    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("created_by")]
    public int CreatedBy { get; set; }

    public int Status { get; set; }
    [Column("team_code")]
    public int? TeamCode { get; set; }

    public virtual ICollection<AimsLnkTeamEa> AimsLnkTeamEas { get; set; } = new List<AimsLnkTeamEa>();

    public virtual ICollection<AimsLnkTeamPerson> AimsLnkTeamPeople { get; set; } = new List<AimsLnkTeamPerson>();

    public virtual AimsPerson CreatedByNavigation { get; set; } = null!;

    public virtual PagedList.IPagedList<Step> Steps { get; set; }
}