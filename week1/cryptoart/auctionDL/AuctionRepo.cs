using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;
using Microsoft.EntityFrameworkCore;
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

        public Auction GetAuction(int id)
        {
            return _context.Auctions.Find(id);
        }

        public Bid GetHighBid(Auction a2b)
        {
            Bid hiBid = _context.Bids.Where(x => x.Auctionid == a2b.Id).OrderByDescending(y => y.Amount).FirstOrDefault();
            return hiBid;
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

        public bool CheckForComission(int artistid)
        {
            DateTime dt = DateTime.Now;

            List<Auction> activeAuctions = _context.Auctions.Where(x => x.Closingdate <= dt && (x.Notify & 4) != 4).Include(y => y.Art).Include(z => z.Bids).ToList();
            foreach (Auction ac in activeAuctions) {
                
                if (ac.Art.Artistid == artistid) {
                    Console.WriteLine($"Congatulations! your art piece {ac.Art.Name} sold for {ac.Bids.Where(z => z.Timeofbid < ac.Closingdate).OrderByDescending(x => x.Amount).FirstOrDefault().Amount} !");
                    ac.Art.Currentvalue = ac.Bids.Where(z => z.Timeofbid < ac.Closingdate).OrderByDescending(x => x.Amount).FirstOrDefault().Amount;
                    ac.Notify += 4;
                    _context.SaveChanges();
                }
            }
            return false;
        }


        public bool CheckForSale(int sellerid)
        {
            DateTime dt = DateTime.Now;

            List<Auction> activeAuctions = _context.Auctions.Where(x => x.Closingdate <= dt && (x.Notify & 2) != 2 && x.Sellerid==sellerid).Include(y => y.Art).Include(z => z.Bids).ToList();
            foreach (Auction ac in activeAuctions) {

                    Console.WriteLine($"Congatulations! your art piece {ac.Art.Name} sold for {ac.Bids.Where(z => z.Timeofbid < ac.Closingdate).OrderByDescending(x => x.Amount).FirstOrDefault().Amount} !");
                Sellersinventory si = new Sellersinventory();
                si.Artid = System.Convert.ToInt32(ac.Artid);
                si.Sellerid = sellerid;
                try {
                    _context.Sellersinventories.Remove(_context.Sellersinventories.Where(x => x.Artid == ac.Artid).FirstOrDefault());
                }
                catch { }

                ac.Art.Currentvalue = ac.Bids.Where(z => z.Timeofbid < ac.Closingdate).OrderByDescending(x => x.Amount).FirstOrDefault().Amount;
                ac.Notify += 2;
                    _context.SaveChanges();
            }
            return false;
        }

        public bool CheckForWin(int collectorid)
        {
            DateTime dt = DateTime.Now;

            List<Auction> activeAuctions = _context.Auctions.Where(x => x.Closingdate <= dt && (x.Notify & 1)!=1).Include(y => y.Art).Include(z => z.Bids).ToList();

            foreach (Auction ac in activeAuctions) {
                Bid bid = ac.Bids.Where(z => z.Timeofbid < ac.Closingdate).OrderByDescending(x => x.Amount).FirstOrDefault();
                Console.WriteLine(bid.Collectorid);
                if (bid.Collectorid==collectorid) {
                    Console.WriteLine($"Congatulations! You had the winning bid of {bid.Amount} on the art piece {ac.Art.Name} ! Enjoy your art!");

                    Collectorsinventory ci = new Collectorsinventory();
                    int id = System.Convert.ToInt32(ac.Artid);
                    ci.Artid = id;
                    ci.Collectorid = collectorid;
                    if (_context.Collectorsinventories.Where(x => x.Artid == ci.Artid).Count() < 1) {
                        _context.Collectorsinventories.Add(ci);
                    }
                    ac.Art.Currentvalue = bid.Amount;
                    ac.Notify += 1;
                    _context.SaveChanges();
                }
            }
            return false;
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
                Bid bid = _context.Bids.Where(x => x.Auctionid == a.Id).OrderByDescending(y=>y.Amount).FirstOrDefault();
                if (bid != null) {
                    Console.WriteLine($"Current Winning Bid :{bid.Amount}");
                }
                else {
                    Console.WriteLine($"No Current Bids");

                }
                Console.WriteLine("---------------------------------------");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

        }



    }
}
