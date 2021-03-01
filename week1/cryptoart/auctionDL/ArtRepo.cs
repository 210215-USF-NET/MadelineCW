using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;
using mod=auctionModels;
using System.Linq;

namespace auctionDL
{
    public class ArtRepo : IArtRepo
    {
        private wzvzhuteContext _context;
        private IArtMapper _mapper;

        public ArtRepo(wzvzhuteContext context, IArtMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public mod.Art AddArt(mod.Art newArt)
        {
            if (Exists(newArt.Id)) {
                newArt = _mapper.Parse(_context.Arts.Find(newArt.Id));

            }
            else {

                _context.Arts.Add(_mapper.Parse(newArt));
                _context.SaveChanges();

            }
            return newArt;
        }

        public void Save(mod.Art art)
        {
            Art tc = _context.Arts.Find(art.Id);
            tc.Name = art.Name;
            tc.Description = art.Description;
            tc.Artistcommentary = art.ArtistStatement;
            tc.Buynowprice = art.BuyNoWPrice;
            tc.Currentvalue = art.CurrentValue;


            _context.SaveChanges();
        }


        public bool Exists(int id)
        {

            return (_context.Arts.Find(id) != null);

        }

        public List<mod.Art> GetArts()
        {
           
            return new List<mod.Art>();

        }

        public mod.Art GetArt(int id,int sellerid)
        {
            Sellersinventory si = _context.Sellersinventories.Where(x => x.Artid == id&&x.Sellerid==sellerid).FirstOrDefault();
            if (si == null) {
                return new mod.Art();
            }
            Art art = _context.Arts.Where(x => x.Id == si.Artid).FirstOrDefault();
            if (si == null) {
              
                Console.WriteLine("this Art Is not part of your inventory");
            }
            return _mapper.Parse(art);

        }


        /*
        public List<mod.Art> GetArtByIds(int [] ids)
        {
             List<mod.Art> subcollection=new List<mod.Art>();
            foreach (int id in ids) {
                subcollection.Add(cachedArt[id]);
            }
            return subcollection;
        }

        */

    }
}
