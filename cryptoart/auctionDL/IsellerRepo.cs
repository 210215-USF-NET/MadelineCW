using System.Collections.Generic;
using mod = auctionModels;

namespace auctionDL
{
    public interface IsellerRepo
    {

        public mod.Seller AddSeller(mod.Seller newSeller);
        public List<mod.Seller> GetSellers();
        public bool Exists(int id);
    }
}
