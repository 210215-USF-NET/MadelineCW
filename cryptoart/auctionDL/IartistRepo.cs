using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = auctionModels;
namespace auctionDL
{
    public interface IartistRepo
    {

        public mod.Artist AddArtist(mod.Artist newArt);
        public List<mod.Artist> GetArtists();
        public bool Exists(int id);
    }
}
