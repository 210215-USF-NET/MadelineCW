using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;
using mod = auctionModels;
namespace auctionDL
{
    class BidRepo
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

        public void Save(mod.Bid bid)
        {
            Bid tc = _context.Bids.Find(bid.Id);
            tc.Amount = (decimal?) bid.BidAmount;
            tc.Timeofbid = bid.TimeOfBid;
            _context.SaveChanges();
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
