using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Seller
    {
        public Seller()
        {
            Auctions = new HashSet<Auction>();
            Sellersinventories = new HashSet<Sellersinventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Sellersinventory> Sellersinventories { get; set; }
    }
}
