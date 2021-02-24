using System;

namespace auctionDL
{
    public class Artist
    {
        private string countryCode="";
        int id = 0;
        string name = "";
        string biography = "";
        public bool registered { get; set; }
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Biography { get { return biography; } set { biography = value; } }
        public string CountryCode {
            get { return countryCode;
            }
            set{
                countryCode = value;
            }
        }
        public Art[] Gallery;
        Signature signature;
        public override string ToString()
        {
            string s = $"Seller:\nId={Id}\nname={name}\nbiography{biography}=";
  
            return s;
        }
    }
}