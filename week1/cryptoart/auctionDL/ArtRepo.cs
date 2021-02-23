using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;

namespace auctionDL
{
    class ArtRepo : IArtRepo
    {

        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + "artFile.json";
        private List<Art> cachedArt;
        public Art AddArt(Art newArt)
        {
            cachedArt = GetArt();

            if (Exists(newArt.Id)) {
                logging.log("Art Id Exists, No need To Add to List");
                return newArt;
            }
            newArt.Id = cachedArt.Count;
            cachedArt.Add(newArt);
            logging.log("adding art " + newArt.Id + " to repository");
            json = JsonSerializer.Serialize(cachedArt);
            File.WriteAllText(filepath, json);
            return newArt;
        }

        public bool Exists(int id)
        {
            return (id < cachedArt.Count);
        }

        public List<Art> GetArt()
        {
            if (cachedArt != null) { return cachedArt; }
            try {

                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<Art>>(json);
            }
            catch (Exception) {
                logging.log("error with repo file, returning new Art List");
                return new List<Art>();
            }
        }

        public List<Art> GetArtByIds(int [] ids)
        {
             List<Art> subcollection=new List<Art>();
            int count = 0;
            foreach(Art a in cachedArt) {
               
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
