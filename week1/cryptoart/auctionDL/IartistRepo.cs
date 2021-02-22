using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auctionDL
{
    interface IartistRepo
    {

        public Artist AddArtist(Artist newArt);
        public List<Artist> GetArtists();
        public bool Exists(int id);
    }
}
