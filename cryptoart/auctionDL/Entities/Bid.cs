using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Bid
    {
        public int Id { get; set; }
        public int? Auctionid { get; set; }
        public int? Collectorid { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Timeofbid { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual Collector Collector { get; set; }
    }
}
