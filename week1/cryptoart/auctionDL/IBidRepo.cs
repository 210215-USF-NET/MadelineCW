using System.Collections.Generic;
using mod = auctionModels;
namespace auctionDL
{
    interface IBidRepo
    {
        public mod.Bid AddBid(mod.Bid newBid);
        public List<mod.Bid> GetBids();
        public bool Exists(int id);

    }
}
