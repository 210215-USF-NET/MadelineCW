using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;
using System.Linq;
using mod = auctionModels;

namespace auctionDL
{
    public class AuctionRepo : Iauctionrepo
    {

        private wzvzhuteContext _context;
        private IAuctionMapper _mapper;

        public AuctionRepo(wzvzhuteContext context, IAuctionMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public mod.Auction AddAuction(mod.Auction newAuction)
        {
            if (Exists(newAuction.Id)) {
                newAuction = _mapper.Parse(_context.Auctions.Find(newAuction.Id));

            }
            else {

                _context.Auctions.Add(_mapper.Parse(newAuction));
                _context.SaveChanges();

            }
            return newAuction;
        }
        public Auction AddAuction(Auction newAuction)
        {
            _context.Auctions.Add(newAuction);
            _context.SaveChanges();
            return newAuction;

        }
        public void Save(mod.Auction auction)
        {
            Auction tc = _context.Auctions.Find(auction.Id);
            tc.Closingdate = auction.closingDate;
            tc.Sellerid = auction.SellerId;
            _context.SaveChanges();
        }


        public bool Exists(int id)
        {

            return (_context.Auctions.Find(id) != null);

        }

        public List<mod.Auction> GetAuctions()
        {

            return new List<mod.Auction>();

        }

        public void ShowActiveAuctions()
        {


            DateTime dt = DateTime.Now;

            List<Auction> activeAuctions = _context.Auctions.Where(x => x.Closingdate > dt ).ToList();
            if (activeAuctions.Count<1) {
                Console.WriteLine("there are no active auctions, please check back again");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Auction a in activeAuctions) {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Auction Id :{a.Id}");
                Console.WriteLine($"Closes at :{a.Closingdate}");
                Art art = _context.Arts.Where(x => x.Id == a.Artid).FirstOrDefault();
                Console.WriteLine($"For Art :{art.Name}");
                Bid bid = _context.Bids.Where(x => x.Auctionid == a.Id).OrderBy(y=>y.Amount).FirstOrDefault();
                Console.WriteLine($"Current Winning Bid :{bid.Amount}");
                Console.WriteLine("---------------------------------------");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

        }



    }
}
