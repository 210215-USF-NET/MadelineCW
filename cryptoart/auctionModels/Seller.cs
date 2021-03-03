using System.Collections.Generic;

namespace auctionModels
{
    public class Seller
    {
        private string _name = "";
        public int Id { get; set; }
        public List<Art> inventory { get; set; }
        public List<Auction> currentAuctions { get; set; }
        public List<Auction> auctionHistory { get; set; }
        public string name {
            get { return _name; }

            set { _name = value; }
        }


        public override string ToString()
        {
            string s = "Welcome\n";
            s += $"id: {Id}\n";
            s += $"name: {name}\n";
            /*if (currentAuctions.Count > 0) {
                s = $"Seller:/nId={Id}/nname={name}/ninventory=";
                foreach (Auction a in currentAuctions) {
                    s += a.ToString() + "/n";
                }
            }
            */

            return s;
        }
        
    }
}
    