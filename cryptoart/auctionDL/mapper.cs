using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mod = auctionModels;
using entity = auctionDL;

namespace auctionDL
{
    public class mapper : imapper
    {
        public mod.Collector ParseCollector(entity.Collector collector)
        {
            if (collector != null) {
                return new mod.Collector {
                    Name = collector.Name,
                    Location = collector.Location,
                    Id = collector.Id

                };
            }
            else return new mod.Collector();
        }

        public entity.Collector ParseCollector(mod.Collector collector)
        {
            entity.Collector c = new entity.Collector();
            c.Name = collector.Name;
            c.Location = collector.Location;
            if (c.Id==null) {
                c.Id = collector.Id;
            }
                return c;
        }
    }
}
