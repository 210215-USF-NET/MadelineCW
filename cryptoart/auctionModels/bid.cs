using System;


namespace auctionModels
{
    public class Bid
    {
        public int Id { get; set; }
        public double BidAmount { get; set; }
        public DateTime? TimeOfBid { get; set; }
        int CollectorId { get; set; }
    }
}
