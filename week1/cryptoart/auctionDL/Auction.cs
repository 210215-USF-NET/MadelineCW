using System;


namespace auctionDL
{
    public class Auction
    {
        Art auctionItem { get; set; }
        DateTime closingDate { get; set; }
        Bid[] Bids;
        int SellerId;
    }
}
