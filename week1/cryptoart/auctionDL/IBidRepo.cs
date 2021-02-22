using System.Collections.Generic;

namespace auctionDL
{
    interface IBidRepo
    {
        public Bid AddBid(Bid newBid);
        public List<Bid> GetBids();
        public bool Exists(int id);

    }
}
