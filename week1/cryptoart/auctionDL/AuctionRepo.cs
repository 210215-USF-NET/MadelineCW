using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;

namespace auctionDL
{
    class AuctionRepo : Iauctionrepo
    {
        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + "auctionFile.json";
        private List<Auction> cachedAuctions;
        public Auction AddAuction(Auction newAuction)
        {
            cachedAuctions = GetAuctions();

            if (Exists(newAuction.Id)) {
                logging.log("Artist Id Exists, No need To Add to List");
                return newAuction;
            }
            newAuction.Id = cachedAuctions.Count;
            cachedAuctions.Add(newAuction);
            logging.log("adding auction " + newAuction.Id + " to repository");
            json = JsonSerializer.Serialize(cachedAuctions);
            File.WriteAllText(filepath, json);
            return newAuction;
        }

        public bool Exists(int id)
        {
            return (id < cachedAuctions.Count);
        }

        public List<Auction> GetAuctions()
        {
            if (cachedAuctions != null) { return cachedAuctions; }
            try {

                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<Auction>>(json);
            }
            catch (Exception) {
                logging.log("error with repo file, returning new Auction List");
                return new List<Auction>();
            }
        }

        public List<Auction> GetAuctionByIds(int[] ids)
        {
            List<Auction> subcollection = new List<Auction>();
            int count = 0;
            foreach (Auction a in cachedAuctions) {

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
