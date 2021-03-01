using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Auction
    {
        public Auction()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public int? Sellerid { get; set; }
        public int? Artid { get; set; }
        public decimal? Minimumamount { get; set; }
        public DateTime? Closingdate { get; set; }

        public virtual Art Art { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
