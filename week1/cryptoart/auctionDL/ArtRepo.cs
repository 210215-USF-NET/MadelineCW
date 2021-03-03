using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;
using mod=auctionModels;
using Microsoft.EntityFrameworkCore;
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



        public void ShowArtByCollector(int id)
        {
            List<Collectorsinventory> al = _context.Collectorsinventories.Where(x => x.Collectorid == id).Include(y => y.Art).ThenInclude(z=>z.Artist).ToList();
           // List<Art> al = _context.Arts.Include(x=>x.Collectorsinventories).Where(y => y.Collectorsinventories.;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Collectorsinventory a in al) {
                Console.WriteLine("--------------------");
                Console.WriteLine($"Current Value : {a.Art.Currentvalue}");
                Console.WriteLine($"By : {a.Art.Artist.Name}");
                ArtDetails(a.Art);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
        }


        public void ShowArtByArtist(int id)
        {

            List<Art> al = _context.Arts.Where(x => x.Artistid == id).ToList();
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Art a in al) {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--------------------");
                if (_context.Sellersinventories.Where(x => a.Id == x.Artid).Count() > 0 || _context.Collectorsinventories.Where(x => a.Id == x.Artid).Count() > 0) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("This art belongs to an inventory");
                }
                Console.WriteLine($"Current Value : {a.Currentvalue}");

                ArtDetails(a);
 
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public void ShowArtByPrice()
        {

            Console.Clear();

            List<Art> arts = _context.Arts.Include(x => x.Artist).OrderByDescending(y => y.Currentvalue).ToList();
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (Art a in arts) {
                Console.WriteLine("--------------------");
                Collectorsinventory ci = _context.Collectorsinventories.Where(x => a.Id == x.Artid).Include(y => y.Collector).FirstOrDefault();
                if (ci != null) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("This art belongs to " + ci.Collector.Name);
                }
                Auction auc = _context.Auctions.Where(x => x.Artid == a.Id && x.Closingdate > DateTime.Now).Include(y => y.Bids).FirstOrDefault();

                if (auc != null) {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"This art is currently being Auctioned off with a high bid of {auc.Bids.OrderByDescending(x => x.Amount).FirstOrDefault().Amount}");
                }
                ArtDetails(a);
                Console.WriteLine($"Current Value : {a.Currentvalue}");
                Console.WriteLine($"Art by {a.Artist.Name}");
               
            }
            Console.WriteLine("--------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public void ShowAll()
        {


      




            Console.Clear();
            List<Art> arts = _context.Arts.Include(x=>x.Artist).OrderBy(y=>y.Artist.Name).ToList();
            Console.ForegroundColor = ConsoleColor.Green;
            string lastArtist = "";
            foreach (Art a in arts) {
                if (a.Artist.Name != lastArtist) {
                    lastArtist = a.Artist.Name;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n###################");
                    Console.WriteLine($"Art by {lastArtist}");
                    Console.WriteLine("\n###################");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Collectorsinventory ci=_context.Collectorsinventories.Where(x => a.Id == x.Artid).Include(y => y.Collector).FirstOrDefault();
                if (ci!=null) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("This art belongs to "+ci.Collector.Name);
                }
                Auction auc = _context.Auctions.Where(x => x.Artid == a.Id && x.Closingdate > DateTime.Now).Include(y => y.Bids).FirstOrDefault();
            
                if (auc!=null){
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"This art is currently being Auctioned off with a high bid of {auc.Bids.OrderByDescending(x => x.Amount).FirstOrDefault().Amount}");
                }
                ArtDetails(a);
                Console.WriteLine($"Current Value : {a.Currentvalue}");
                Console.WriteLine("--------------------");
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public void ArtDetails(Art a)
        {

            Console.WriteLine($"Art Id : {a.Id}");
            Console.WriteLine($"Name: {a.Name}");
            Console.WriteLine($"Description:{a.Description}");
            
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

    }
}
