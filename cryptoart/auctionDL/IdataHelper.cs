using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auctionDL
{
    interface IdataHelper
    {
        public List<Collector> GetDeserializedData(string dataConnector);
        public void SetSerializableData(string dataConnector, List<Collector> serialData);
    }
}
