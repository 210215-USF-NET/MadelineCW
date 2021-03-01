using System;


namespace auctionModels
{
    public class Auction
    {
        public int Id { get; set; }
        Art auctionItem { get; set; }
       public DateTime? closingDate { get; set; }
        Bid[] Bids {get; set;}
        public int SellerId{get; set;}
    }
}
