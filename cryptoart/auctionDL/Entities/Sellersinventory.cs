using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Sellersinventory
    {
        public int Sellerid { get; set; }
        public int Artid { get; set; }

        public virtual Art Art { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
