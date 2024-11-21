using System;
using System.Collections.Generic;

namespace PHIASPACE.CORE.DAL.Model.DB
{
    public partial class ClientClaim
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
    }
}
