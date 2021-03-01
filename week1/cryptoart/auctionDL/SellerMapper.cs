using mod = auctionModels;
using entity = auctionDL;
namespace auctionDL
{
    public class SellerMapper : ISellerMapper
    {

        public mod.Seller Parse(entity.Seller seller)
        {
            return new mod.Seller {
                name = seller.Name,

                Id = seller.Id

            };
        }

        public entity.Seller Parse(mod.Seller seller)
        {
            entity.Seller c = new entity.Seller();
            c.Name = seller.name;

            if (c.Id == null) {
                c.Id = seller.Id;
            }
            return c;
        }


    }
}
