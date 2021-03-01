using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Collectorsinventory
    {
        public int Collectorid { get; set; }
        public int Artid { get; set; }

        public virtual Art Art { get; set; }
        public virtual Collector Collector { get; set; }
    }
}
