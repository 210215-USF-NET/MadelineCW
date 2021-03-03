using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Blacklist
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int? Artid { get; set; }

        public virtual Art Art { get; set; }
    }
}
