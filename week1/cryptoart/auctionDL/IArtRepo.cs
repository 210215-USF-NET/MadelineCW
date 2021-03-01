using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod=auctionModels;
namespace auctionDL
{
    public interface IArtRepo
    {
        public mod.Art AddArt(mod.Art newArt);
        public mod.Art GetArt(int id, int sid);
        public bool Exists(int id);



    }


   
}
