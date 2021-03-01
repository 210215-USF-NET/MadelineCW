using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = auctionModels;
using entity = auctionDL;
namespace auctionDL
{
   public class ArtMapper : IArtMapper
    {

        public mod.Art Parse(entity.Art art)
        {
            return new mod.Art {
                Name = art.Name,
                Description = art.Description,
                ArtistStatement = art.Artistcommentary,
                BuyNoWPrice = (decimal) art.Buynowprice,
                CurrentValue=(decimal) art.Currentvalue,
                
                Id = art.Id

            };
        }

        public entity.Art Parse(mod.Art art)
        {
            entity.Art c = new entity.Art();
            c.Name = art.Name;
            c.Description = art.Description;
            c.Artistcommentary = art.ArtistStatement;
            c.Buynowprice = art.BuyNoWPrice;
            c.Currentvalue = art.CurrentValue;
            if (c.Id == null) {
                c.Id = art.Id;
            }
            return c;
        }


    }
}
