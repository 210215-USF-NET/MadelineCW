using System.Collections.Generic;


namespace auctionDL
{
    interface IsellerRepo
    {

        public Seller AddSeller(Seller newSeller);
        public List<Seller> GetSellers();
        public bool Exists(int id);
    }
}
