using System;

namespace auctionDL
{
    /// <summary>
    /// data structure for a collector;
    /// </summary>
    public class Collector
    {
        private string countryCode;
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode {
            get { return countryCode;
            }
            set{
                countryCode = value;
            }
        }
        public Art[] Gallery;
        public Auction[] currentBids;
        public Auction[] BidHistory;

    }
}
