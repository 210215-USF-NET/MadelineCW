using System;


namespace auctionDL
{
    public class Art
    {
        int id = 0;
        string name = "";
        string description = "";
        string artiststatement = "";
        double currentValue = 0.0;
        public int Id {
            get {
                return id;
            }
            set { id = value; }
        }

        string Name { get { return name; }set { name = value; } }
        string Description { get { return description; } set { description = value; } }
        string ArtistStatement { get { return artiststatement; } set { artiststatement = value; } }
        object[] blacklist {get; set;}
        object [] provenence{get; set;}
        object thumbnail{get; set;}
        object fullart{get; set;}
        string[] keywords{get;set;}
        int SeriesNumber{get;set;}
        int MaxSeries{get; set;}
        double BuyNoWPrice{get; set;}
        double CurrentValue { get { return currentValue; } set { currentValue = value; } }
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
