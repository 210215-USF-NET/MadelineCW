using System;
using System.Collections.Generic;


namespace auctionDL
{
    public interface IcollectorRepo
    {
        public Collector AddCollector(Collector newCollector);
        public List<Collector> GetCollectors();
        public bool Exists(int id);


    }
}
