using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Collector
    {
        public Collector()
        {
            Bids = new HashSet<Bid>();
            Collectorsinventories = new HashSet<Collectorsinventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Collectorsinventory> Collectorsinventories { get; set; }
    }
}
