using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = auctionModels;


namespace auctionDL
{
    public interface IArtMapper
    {
        mod.Art Parse(Art art);
        Art Parse(mod.Art art);


    }
}
