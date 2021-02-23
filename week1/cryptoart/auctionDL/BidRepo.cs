using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;

namespace auctionDL
{
    class BidRepo
    {

        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + "bidFile.json";
        private List<Bid> cachedBids;
        public Bid AddBid(Bid newBid)
        {
            cachedBids = GetBids();

            if (Exists(newBid.Id)) {
                logging.log("Bid Id Exists, No need To Add to List");
                return newBid;
            }

            newBid.Id = cachedBids.Count;
            cachedBids.Add(newBid);
            
            logging.log("adding bid " + newBid.Id + " to repository");
            json = JsonSerializer.Serialize(cachedBids);
            File.WriteAllText(filepath, json);
            return newBid;
        }

        public bool Exists(int id)
        {
            return (id < cachedBids.Count);
        }

        public List<Bid> GetBids()
        {
            if (cachedBids != null) { return cachedBids; }
            try {

                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<Bid>>(json);
            }
            catch (Exception) {
                logging.log("error with repo file, returning new Bid List");
                return new List<Bid>();
            }
        }

        public List<Bid> GetBidByIds(int[] ids)
        {
            List<Bid> subcollection = new List<Bid>();
            int count = 0;



            foreach (Bid a in cachedBids) {

                if (count > ids.Length) { break; }
                foreach (int id in ids) {
                    if (id == a.Id) {
                        subcollection.Add(a);
                        count++;
                        break;
                    }
                }

            }
            return subcollection;
        }






    }
}
