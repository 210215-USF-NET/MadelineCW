using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Configuration;

namespace auctionDL
{
    public class dataHelper : IdataHelper
    {
        private string json;
        private string filepath;

        public List<Collector> GetDeserializedData(string dataConnector)
        {
            filepath = dataConnector;
            json = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Collector>>(json);
        }

        public void SetSerializableData(string dataConnector, List<Collector> serialData)
        {
            json = JsonSerializer.Serialize(serialData);
            File.WriteAllText(filepath, json);
        }

    }
}
