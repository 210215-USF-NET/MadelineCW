using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;
using mod = auctionModels;

namespace auctionDL
{
    public class ArtistRepo : IartistRepo
    {
        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + ConfigurationManager.AppSettings.Get("artistData");
        private List<mod.Artist> cachedArtists;
        public mod.Artist AddArtist(mod.Artist newArtist)
        {
            cachedArtists = GetArtists();

            if (Exists(newArtist.Id)) {
                logging.log("Artist Id Exists, No need To Add to List");
                cachedArtists[newArtist.Id].registered = true;
                return cachedArtists[newArtist.Id];
            }
            newArtist.Id = cachedArtists.Count;
            newArtist.registered = false;
            cachedArtists.Add(newArtist);
            logging.log("adding artist " + newArtist.Id + " to repository");
            SaveJson();
            return newArtist;
        }

        public void Save(mod.Artist customer)
        {
            cachedArtists[customer.Id] = customer;
            SaveJson();
        }

        private void SaveJson()
        {
            json = JsonSerializer.Serialize(cachedArtists);
            File.WriteAllText(filepath, json);
        }


        public bool Exists(int id)
        {
            return (id < cachedArtists.Count);
        }

        public List<mod.Artist> GetArtists()
        {
            if (cachedArtists != null) { return cachedArtists; }
            try {

                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<mod.Artist>>(json);
            }
            catch (Exception) {
                logging.log("error with repo file, returning new Artist List");
                return new List<mod.Artist>();
            }
        }

        public List<mod.Artist> GetArtistByIds(int[] ids)
        {
            List<mod.Artist> subcollection = new List<mod.Artist>();
                foreach (int id in ids) {
                        subcollection.Add(cachedArtists[id]);
                    }
            return subcollection;
        }

    }
}
