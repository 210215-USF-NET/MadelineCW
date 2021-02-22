using System;


namespace auctionDL
{
    public class Bid
    {
        public int Id { get; set; }
        double BidAmount { get; set; }
        DateTime TimeOfBid { get; set; }
        int CollectorId { get; set; }
    }
}
