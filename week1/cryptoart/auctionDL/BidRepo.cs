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
   public  class BidRepo
    {

        private wzvzhuteContext _context;
        private IBidMapper _mapper;

        public BidRepo(wzvzhuteContext context, IBidMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public mod.Bid AddBid(mod.Bid newBid)
        {
            if (Exists(newBid.Id)) {
                newBid = _mapper.Parse(_context.Bids.Find(newBid.Id));

            }
            else {

                _context.Bids.Add(_mapper.Parse(newBid));
                _context.SaveChanges();

            }
            return newBid;
        }

        public void AddBid(Bid newBid)
        {
            _context.Bids.Add(newBid);
            _context.SaveChanges();
        }

        public void Save(mod.Bid bid)
        {
            Bid tc = _context.Bids.Find(bid.Id);
            tc.Amount = (decimal?) bid.BidAmount;
            tc.Timeofbid = bid.TimeOfBid;
            _context.SaveChanges();
        }

        public void ShowBidsByBidder(int id)
        {
            List<Bid> blist = _context.Bids.Where(x => x.Collectorid == id).OrderBy(y => y.Auctionid).ThenBy(z => z.Amount).ToList();
            foreach (Bid b in blist) {
                List <Auction> la= _context.Auctions.Where(k => k.Id == b.Auctionid).ToList();
                foreach (Auction auc in la) {
                    Console.WriteLine(_context.Arts.Where(x => x.Id == auc.Artid).FirstOrDefault().Name);
                    Console.WriteLine($" your bid {b.Amount}");
                    Console.WriteLine($"made at {b.Timeofbid}");
                    Console.WriteLine($"Bid Closes at {auc.Closingdate}");
                    Console.WriteLine($"Winning Bid so far : {_context.Bids.Where(x=>x.Auctionid==auc.Id).OrderBy(y=>y.Amount).FirstOrDefault().Amount}");
                    Console.WriteLine("------------------");
                }
            }
        }
        public bool Exists(int id)
        {

            return (_context.Bids.Find(id) != null);

        }

        public List<mod.Bid> GetBids()
        {

            return new List<mod.Bid>();

        }




    }
}
