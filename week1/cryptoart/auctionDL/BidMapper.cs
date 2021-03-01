using System.Collections.Generic;
using mod = auctionModels;

namespace auctionDL
{
    class BidMapper : IBidMapper
    {

        public mod.Bid Parse(Bid bid)
        {
            return new mod.Bid {
               BidAmount = (double) bid.Amount,
               TimeOfBid = bid.Timeofbid,
                Id = bid.Id

            };
        }

        public Bid Parse(mod.Bid bid)
        {
            Bid tc = new Bid();
            tc.Amount = (decimal?)bid.BidAmount;
            tc.Timeofbid = bid.TimeOfBid;
            if (tc.Id == null) {
                tc.Id = bid.Id;
            }
            return tc;
        }

    }
}
