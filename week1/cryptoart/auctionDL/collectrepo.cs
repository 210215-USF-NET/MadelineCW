﻿using System;
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
        private List<Collector> customerList;


        public Collector AddCollector(Collector customer)
        {
            customerList = GetCollectors();
            
            if (Exists(customer.Id))
            {
                logging.log("collector Id Exists, No need To Add to Collection");
                return customer;
            }

            customer.Id = customerList.Count;
            customerList.Add(customer);
            logging.log("adding customer "+customer.Id+" to repository");
            SaveJson();
            return customer;
        }
    
        private void SaveJson() {
            json = JsonSerializer.Serialize(customerList);
            File.WriteAllText(filepath, json);
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


        public bool Exists(int id)
        {
  
                return (id< customerList.Count);

        }

    }
}
