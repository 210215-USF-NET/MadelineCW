using mod = auctionModels;
using entity = auctionDL;
namespace auctionDL
{
    public class SellerMapper : ISellerMapper
    {

        public mod.Seller Parse(entity.Seller seller)
        {
            if (seller != null) {
                return new mod.Seller {
                    name = seller.Name,

                    Id = seller.Id

                };

            }
            else {
                return new mod.Seller();
                }
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
