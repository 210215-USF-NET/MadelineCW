using System.Collections.Generic;

namespace auctionDL
{
    public class Seller
    {
        public int Id { get; set; }
        public List<Art> inventory { get; set; }
        public List<Auction> currentAuctions { get; set; }
        public List<Auction> auctionHistory { get; set; }
        public string name { get; set; }
    }
}