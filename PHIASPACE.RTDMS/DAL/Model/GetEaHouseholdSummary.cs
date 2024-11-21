using System;
using System.Collections.Generic;

namespace PHIASPACE.RTDMS.DAL.Model;

public partial class GetEaHouseholdSummary
{
    public decimal? ClusterNumber { get; set; }

    public decimal HouseholdsVisited { get; set; }

    public long HouseholdsCompleted { get; set; }

    public long HouseholdsRefused { get; set; }
}
