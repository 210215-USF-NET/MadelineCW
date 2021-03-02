using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using mod = auctionModels;

using auctionBL;

using System.Linq;
namespace auctionDL
{
    public class collectrepo : IcollectorRepo
    {
        private wzvzhuteContext _context;
        private imapper _mapper;
       
       
        public collectrepo(wzvzhuteContext context,imapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public mod.Collector AddCollector(mod.Collector customer)
        {
            try {
                if (customer.Id < 1) {
                    //_context.Collectors.Load();
                    customer = _mapper.ParseCollector(_context.Collectors.Where(x => x.Name.ToLower() == customer.Name.ToLower()).FirstOrDefault());

                }
            }
            catch { }
            if (Exists(customer.Id)) {
                customer = _mapper.ParseCollector(_context.Collectors.Find(customer.Id));
               
            }
            else {

                _context.Collectors.Add(_mapper.ParseCollector(customer));
                _context.SaveChanges();
              
            }
            return customer;
        }
    
        public mod.Collector SaveCollector(mod.Collector customer) {
            Collector tc = _context.Collectors.Find(customer.Id);
            if (tc == null) {
               tc=_context.Collectors.Add(_mapper.ParseCollector(customer)).Entity;
                _context.SaveChanges();
                return _mapper.ParseCollector(tc);
               
            }
            tc.Name = customer.Name;
            tc.Location = customer.Location;
            _context.SaveChanges();
            return _mapper.ParseCollector(tc);
        }


        public List<mod.Collector> GetCollectors()
        {
        
                return new List<mod.Collector>();
            
        }


        public bool Exists(int id)
        {

            return (_context.Collectors.Find(id) != null);

        }

    }
}
