using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = auctionModels;
using entity = auctionDL;

namespace auctionDL
{
    public class ArtistMapper : IArtistMapper
    {

        public mod.Artist Parse(entity.Artist artist)
        {
            if (artist != null) {
                return new mod.Artist {
                    Name = artist.Name,
                    Biography = artist.Biography,
                    ArtistStatement=artist.Artiststatement,
                    Location=artist.Location,
                    Id = artist.Id

                };
            }
            else return new mod.Artist();
        }

        public entity.Artist Parse(mod.Artist artist)
        {
            entity.Artist a = new entity.Artist();
            a.Name = artist.Name;
            a.Location = artist.Location;
            a.Biography = artist.Biography;
            a.Artiststatement = artist.ArtistStatement;
            if (a.Id == null) {
                a.Id = artist.Id;
            }
            return a;
        }




    }
}
