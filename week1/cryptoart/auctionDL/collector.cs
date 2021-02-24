using System;
using System.Collections.Generic;
namespace auctionDL
{
    /// <summary>
    /// data structure for a collector;
    /// </summary>
    public class Collector
    {
        private string countryCode;
        private string _name = "";
        private bool _registered = false;
        public int Id { get; set; }
        
        public string Name {
            get { return _name; }

            set { _name = value; }
        }
        public string Location {
            get {
                return countryCode;
            }
            set {


                countryCode = value;
            }
        }
        public bool registered { get; set; }
        public List<Art> Gallery { get; set; }
        public List<Bid> currentBids { get; set; }
        public List<Bid> BidHistory { get; set; }
        public override string ToString()
        {
            string s = $"Collector:\nId={Id}\nname={Name}\nlocation={Location}\nCollection=";
            /*
            foreach (Art a in Gallery) {
                s += a.ToString() + "/n";
            }
            
            s += "current bids=\n";
            foreach (Bid b in currentBids) {
                s += b.ToString() + "/n";
            }
            s += "past bids=\n";
            foreach (Bid b in BidHistory) {
                s += b.ToString() + "/n";
            }
            */
            return s;
        }
    }
}
