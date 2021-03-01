using mod = auctionModels;

namespace auctionDL
{
    public class AuctionMapper : IAuctionMapper
    {

        public mod.Auction Parse(Auction auction)
        {
            return new mod.Auction {
                closingDate = auction.Closingdate,
                SellerId=(int)auction.Sellerid,
                Id= auction.Id

            };
        }

        public Auction Parse(mod.Auction auction)
        {
            Auction tc = new Auction();
            tc.Sellerid = auction.SellerId;
            tc.Closingdate = auction.closingDate;
            if (tc.Id == null) {
                tc.Id = auction.Id;
            }
            return tc;
        }
    }
}
