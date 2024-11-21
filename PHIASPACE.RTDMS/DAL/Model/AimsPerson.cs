using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsPerson
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? OtherNames { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string TagNumber { get; set; } = null!;

    public int Organization { get; set; }

    public int? Location { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public string? UserId { get; set; }

    public string? NationCode { get; set; }

    public int Status { get; set; }

    public int Gender { get; set; }
}
