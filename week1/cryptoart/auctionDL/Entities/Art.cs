using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Art
    {
        public Art()
        {
            Artistcollections = new HashSet<Artistcollection>();
            Auctions = new HashSet<Auction>();
            Blacklists = new HashSet<Blacklist>();
            Collectorsinventories = new HashSet<Collectorsinventory>();
            Sellersinventories = new HashSet<Sellersinventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Artistcommentary { get; set; }
        public byte[] Artpiece { get; set; }
        public byte[] Thumbnail { get; set; }
        public int? Seriesnumber { get; set; }
        public int? Maxseries { get; set; }
        public decimal? Buynowprice { get; set; }
        public decimal? Currentvalue { get; set; }
        public int? Artistid { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Artistcollection> Artistcollections { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Blacklist> Blacklists { get; set; }
        public virtual ICollection<Collectorsinventory> Collectorsinventories { get; set; }
        public virtual ICollection<Sellersinventory> Sellersinventories { get; set; }
    }
}
