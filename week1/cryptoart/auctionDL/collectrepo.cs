using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace auctionDL
{
    public class collectrepo
    {

        private string json;
        private string filepath = "D:/train/MadelineCW/week1/cryptoart/auctionDL/collectorFile.json";

        public Collector addCollector(Collector customer)
        {
            List<Collector> customerList = GetCollectors();
            
            if (CollectorAlreadyExists(customer.Id,customerList))
            {
                //log customer exists
                return customer;
            }

            customerList.Add(customer);
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
                //log error
                return new List<Collector>();
            }
            
        }


        public bool CollectorAlreadyExists(int id, List<Collector> collectors)
        {
            foreach(Collector c in collectors)
            {
                if (id == c.Id) { return true; }
            }
            return false;
        }

    }
}
