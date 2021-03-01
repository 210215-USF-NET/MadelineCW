using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = auctionModels;
using entity = auctionDL;
namespace auctionDL
{
    public interface IArtistMapper
    {

        public mod.Artist Parse(entity.Artist artist);
        public entity.Artist Parse(mod.Artist artist);



    }
}
