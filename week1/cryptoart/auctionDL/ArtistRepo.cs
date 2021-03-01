using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Configuration;
using auctionBL;
using mod = auctionModels;

namespace auctionDL
{
    public class ArtistRepo : IartistRepo
    {
        private wzvzhuteContext _context;
        private IArtistMapper _mapper;


        public ArtistRepo(wzvzhuteContext context, IArtistMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public mod.Artist AddArtist(mod.Artist newArtist)
        {
            if (Exists(newArtist.Id)) {
                newArtist = _mapper.Parse(_context.Artists.Find(newArtist.Id));

            }
            else {

                _context.Artists.Add(_mapper.Parse(newArtist));
                _context.SaveChanges();

            }
            return newArtist;
        }

        public void Save(mod.Artist artist)
        {
            Artist tc = _context.Artists.Find(artist.Id);
            if (tc == null) {
                tc = _context.Artists.Add(_mapper.Parse(artist)).Entity;
                _context.SaveChanges();

            }
            tc.Name = artist.Name;
            tc.Artiststatement = artist.ArtistStatement;
            tc.Biography = artist.Biography;
            tc.Location = artist.Location;
            _context.SaveChanges();
        }


        public List<mod.Artist> GetArtists()
        {

            return new List<mod.Artist>();

        }



        public bool Exists(int id)
        {

            return (_context.Artists.Find(id) != null);

        }



    }
}
