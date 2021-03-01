using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = auctionModels;
namespace auctionDL
{
    public interface imapper
    {
        mod.Collector ParseCollector(Collector collector);
        Collector ParseCollector(mod.Collector collector);
    }
}
