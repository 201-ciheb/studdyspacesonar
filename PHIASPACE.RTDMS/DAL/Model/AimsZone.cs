﻿using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class AimsZone
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;
}