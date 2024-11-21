using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsCountry
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Country { get; set; } = null!;
}
