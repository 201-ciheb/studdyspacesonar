using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsEa
{
    public int Id { get; set; }

    public double? SnN { get; set; }

    public double? SnM { get; set; }

    public string? CountyName { get; set; }

    public string? SenatorialDistrict { get; set; }

    public double? StLgaCode { get; set; }

    public string? LgaName { get; set; }

    public int? LgaId { get; set; }

    public double? EaCode { get; set; }

    public string? EaName { get; set; }

    public string? PrimaryLocalityName { get; set; }

    public string? SecondaryLocalityName { get; set; }

    public string? Ruralurban { get; set; }

    public double? InclusionProbability { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int? StateId { get; set; }

    public int Active { get; set; }

    public string? ValidatedLatitude { get; set; }

    public string? ValidatedLongitude { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset DateCreated { get; set; }

    public DateTimeOffset DateModified { get; set; }

    public DateTime? DateStarted { get; set; }

    public DateTime? DateClosed { get; set; }

    public virtual ICollection<AimsIssue> AimsIssues { get; set; } = new List<AimsIssue>();

    public virtual ICollection<AimsLnkTeamEa> AimsLnkTeamEas { get; set; } = new List<AimsLnkTeamEa>();
}