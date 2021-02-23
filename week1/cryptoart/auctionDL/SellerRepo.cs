﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;

namespace auctionDL
{
    class SellerRepo : IsellerRepo
    {
        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + "SellerFile.json";
        private List<Seller> cachedSellers;
        public Seller AddSeller(Seller newSeller)
        {
            cachedSellers = GetSellers();

            if (Exists(newSeller.Id)) {
                logging.log("Seller Id Exists, No need To Add to List");
                return newSeller;
            }
            newSeller.Id = cachedSellers.Count;
            cachedSellers.Add(newSeller);
            logging.log("adding Seller " + newSeller.Id + " to repository");
            json = JsonSerializer.Serialize(cachedSellers);
            File.WriteAllText(filepath, json);
            return newSeller;
        }

        public bool Exists(int id)
        {
            return (id < cachedSellers.Count);
        }

        public List<Seller> GetSellers()
        {
            if (cachedSellers != null) { return cachedSellers; }
            try {

                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<Seller>>(json);
            }
            catch (Exception) {
                logging.log("error with repo file, returning new Seller List");
                return new List<Seller>();
            }
        }

        public List<Seller> GetSellerByIds(int[] ids)
        {
            List<Seller> subcollection = new List<Seller>();
            foreach (int id in ids) {
                subcollection.Add(cachedSellers[id]);
            }
            return subcollection;
        }


    }
}
