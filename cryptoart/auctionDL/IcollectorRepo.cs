using System;
using System.Collections.Generic;
using mod = auctionModels;

namespace auctionDL
{
    public interface IcollectorRepo
    {
        public mod.Collector AddCollector(mod.Collector newCollector);
        public List<mod.Collector> GetCollectors();
        public bool Exists(int id);


    }
}
