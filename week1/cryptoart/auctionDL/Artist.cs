using System;

namespace auctionDL
{
    public class Artist
    {
        private string countryCode;
        public int Id { get; set; }
        public string Name { get; set; }
        public string biography {get; set;}
        public string CountryCode {
            get { return countryCode;
            }
            set{
                countryCode = value;
            }
        }
        public Art[] Gallery;
        Signature signature;
    }
}