using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;

namespace auctionDL
{
    public class collectrepo : IcollectorRepo
    {

        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot")+"collectorFile.json";

        public Collector AddCollector(Collector customer)
        {
            List<Collector> customerList = GetCollectors();
            
            if (Exists(customer.Id,customerList))
            {
                logging.log("collector Id Exists, No need To Add to Collection");
                return customer;
            }

            customerList.Add(customer);
            logging.log("adding customer "+customer.Id+" to repository");
            json = JsonSerializer.Serialize(customerList);
            File.WriteAllText(filepath,json);
            return customer;
        }
    
        public List<Collector> GetCollectors()
        {
            try
            {
                
                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<Collector>>(json);
            }
            catch(Exception)
            {
                logging.log("error with repo file, returning new collector List");
                return new List<Collector>();
            }
            
        }


        public bool Exists(int id, List<Collector> collectors)
        {
            foreach(Collector c in collectors)
            {
                if (id == c.Id) { return true; }
            }
            return false;
        }

    }
}
