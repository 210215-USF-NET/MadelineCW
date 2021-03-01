using System;


namespace auctionModels
{
    public class Art
    {
        int id = 0;
        string name = "";
        string description = "";
        string artiststatement = "";
        decimal currentValue = 0.0M;
        public int Id {
            get {
                return id;
            }
            set { id = value; }
        }

        public string Name { get { return name; }set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string ArtistStatement { get { return artiststatement; } set { artiststatement = value; } }
        object[] blacklist {get; set;}
        object [] provenence{get; set;}
        object thumbnail{get; set;}
        object fullart{get; set;}
        string[] keywords{get;set;}
        int SeriesNumber{get;set;}
        int MaxSeries{get; set;}
        public decimal BuyNoWPrice{get; set;}
        public decimal CurrentValue { get { return currentValue; } set { currentValue = value; } }
        public override string ToString()
        {
            string s = $"Art:\nId={Id}\nname={Name}\ndescription={Description}\nartistv statement={ArtistStatement}\ncurrent value={CurrentValue}\n";
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
