﻿using System;

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
        public string CountryCode {
            get {
                return countryCode;
            }
            set {
                countryCode = value;
            }
        }
        public bool registered { get; set; }
        public int[] Gallery;
        public Auction[] currentBids;
        public Auction[] BidHistory;

    }
}
