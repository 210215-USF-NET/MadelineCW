using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auctionDL
{
    interface IArtRepo
    {
        public Art AddArt(Art newArt);
        public List<Art> GetArt();
        public bool Exists(int id);



    }


   
}
