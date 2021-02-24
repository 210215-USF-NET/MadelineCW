using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Configuration;
using auctionBL;

namespace auctionDL
{
    public class ArtistRepo : IartistRepo
    {
        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + ConfigurationManager.AppSettings.Get("artistData");
        private List<Artist> cachedArtists;
        public Artist AddArtist(Artist newArtist)
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

        public void Save(Artist customer)
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

        public List<Artist> GetArtists()
        {
            if (cachedArtists != null) { return cachedArtists; }
            try {

                json = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<List<Artist>>(json);
            }
            catch (Exception) {
                logging.log("error with repo file, returning new Artist List");
                return new List<Artist>();
            }
        }

        public List<Artist> GetArtistByIds(int[] ids)
        {
            List<Artist> subcollection = new List<Artist>();
                foreach (int id in ids) {
                        subcollection.Add(cachedArtists[id]);
                    }
            return subcollection;
        }

    }
}
