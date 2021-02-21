using System;
using System.Collections.Generic;


namespace auctionDL
{
    public interface IcollectorReo
    {
        public Collector AddCollector(Collector newCollector);
        public List<Collector> GetCollectors();
        public bool CollectorAlreadyExists(int id, List<Collector> collectors);


    }
}
