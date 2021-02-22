using System;


namespace auctionDL
{
    public class Auction
    {
        public int Id { get; set; }
        Art auctionItem { get; set; }
        DateTime closingDate { get; set; }
        Bid[] Bids {get; set;}
        int SellerId{get; set;}
    }
}
