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
    class ArtistRepo : IartistRepo
    {
        private string json;
        private string filepath = ConfigurationManager.AppSettings.Get("dataRoot") + "artistsFile.json";
        private List<Artist> cachedArtists;
        public Artist AddArtist(Artist newArtist)
        {
            cachedArtists = GetArtists();

            if (Exists(newArtist.Id)) {
                logging.log("Artist Id Exists, No need To Add to List");
                return newArtist;
            }

            cachedArtists.Add(newArtist);
            logging.log("adding art " + newArtist.Id + " to repository");
            json = JsonSerializer.Serialize(cachedArtists);
            File.WriteAllText(filepath, json);
            return newArtist;
        }

        public bool Exists(int id)
        {
            foreach (Artist a in cachedArtists) {
                if (id == a.Id) { return true; }
            }
            return false;
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
            int count = 0;
            foreach (Artist a in cachedArtists) {

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
