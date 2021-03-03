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
    public class SellerRepo : IsellerRepo
    {

        private wzvzhuteContext _context;
        private ISellerMapper _mapper;


        public SellerRepo(wzvzhuteContext context, ISellerMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public mod.Seller AddSeller(mod.Seller newSeller)
        {

            try {
                if (newSeller.Id < 1) {
                    //_context.Collectors.Load();
                    newSeller = _mapper.Parse(_context.Sellers.Where(x => x.Name.ToLower() == newSeller.name.ToLower()).FirstOrDefault());

                }
            }
            catch { }
            if (Exists(newSeller.Id)) {
                newSeller = _mapper.Parse(_context.Sellers.Find(newSeller.Id));

            }
            else {

               Seller addedSeller = _context.Sellers.Add(_mapper.Parse(newSeller)).Entity;
                _context.SaveChanges();
                newSeller = _mapper.Parse(addedSeller);
            }
            return newSeller;
        }

        public void Save(mod.Seller seller)
        {
            Seller tc = _context.Sellers.Find(seller.Id);
            if (tc == null) {
                tc = _context.Sellers.Add(_mapper.Parse(seller)).Entity;
                _context.SaveChanges();

            }
            tc.Name = seller.name;
            _context.SaveChanges();
        }

        public void AddInventory(int id, int sellerid)
        {
            Sellersinventory SellInv = new Sellersinventory();
           if (_context.Sellersinventories.Where(x => x.Artid == id).Count() > 0) {
                Console.WriteLine("This Art is allready inventoried");
                return;
            }
            else {
                SellInv.Artid = id;
                SellInv.Sellerid = sellerid;
                _context.Sellersinventories.Add(SellInv);
                _context.SaveChanges();
            }
        }
        public List<mod.Seller> GetSellers()
        {

            return new List<mod.Seller>();

        }


        public bool Exists(int id)
        {

            return (_context.Sellers.Find(id) != null);

        }


    }
}
