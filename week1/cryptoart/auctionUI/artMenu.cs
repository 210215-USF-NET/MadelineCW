using System;
using System.Collections.Generic;
using auctionDL;
using System.Linq;
using mod = auctionModels;
using Microsoft.EntityFrameworkCore;
namespace auctionUI
{
    public class artMenu : Imenu
    {
        public static mod.Collector collector;
        public static mod.Artist artist;
        public static mod.Seller seller;
        public static string active = "";
        static bool insub = true;
        public static Dictionary<string, Action> actionOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> CollectorOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> ArtistOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> SellerOptions = new Dictionary<string, Action>();


        private wzvzhuteContext _context;

        public artMenu(wzvzhuteContext context) {
            _context = context;
        }
        static void registerNewCollector(collectrepo cp)
        {
            Console.WriteLine("Please register as a Collector to continue.\n What is your name?");
            collector.Name = Console.ReadLine();
            Console.WriteLine("What country are you ordering from?");
            collector.Location = Console.ReadLine();
            
            collector=cp.SaveCollector(collector);
            Console.WriteLine($"Thank you for registering! your customer id is: {collector.Id}");
        }

        void registerNewSeller(SellerRepo sp)
        {
            Console.WriteLine("Please register as a Seller to continue.\n What is your name?");
            seller.name = Console.ReadLine();           
            sp.Save(seller);
            Console.WriteLine($"Thank you for registering! your customer id is: {seller.Id}");
        }

        void registerNewArtist(ArtistRepo ap)
        {
            Console.WriteLine("Please register to continue. What is your name?");
            artist.Name = Console.ReadLine();
            Console.WriteLine("What is your biography?");
            artist.Biography = Console.ReadLine();
            Console.WriteLine($"Thank you for registering! your artist id is: {artist.Id}");
            ap.Save(artist);
        }

         void buyArt()
        {
            if (active != "collector") {
                Console.WriteLine("please enter your collector Id");
                collector = new mod.Collector();
                active = "collector";
                string userinput = Console.ReadLine();
                try {
                    collector.Id = int.Parse(userinput);
                }
                catch {
                    collector.Name = userinput;
                }
                collectrepo cp = new collectrepo(_context,new mapper());
                collector = cp.AddCollector(collector);
                
                if (collector.Name!="") { Console.WriteLine($"welcome {collector.Name}");
                viewProfile();}
                else {
                    registerNewCollector(cp);
                }
            }
            insub = true;
            while (insub) {
                subMenu();
            }

            

        }


        public void subMenu()
        {
            CheckForWins();
            switch (active) {
                case "collector":
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (KeyValuePair<string, Action> item in CollectorOptions) {
                        Console.WriteLine(item.Key);
                    }
    
                    Console.ForegroundColor = ConsoleColor.Yellow;


                    CollectorOptions[Console.ReadLine()]();
                    break;
                case "artist":
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (KeyValuePair<string, Action> item in ArtistOptions) {
                        Console.WriteLine(item.Key);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    ArtistOptions[Console.ReadLine()]();
                    break;
                case "seller":

                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (KeyValuePair<string, Action> item in SellerOptions) {
                        Console.WriteLine(item.Key);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;


                    SellerOptions[Console.ReadLine()]();
                    break;

            }
        }


        public void sellArt()
        {
            if (active != "seller") {
                Console.WriteLine("please enter your seller Id");
                seller = new mod.Seller();
                active = "seller";
            
            string userinput = Console.ReadLine();
            try {
                    seller.Id = int.Parse(userinput);
                }
                catch {
                    seller.name = userinput;
                }
                SellerRepo cp = new SellerRepo(_context, new SellerMapper());
                seller = cp.AddSeller(seller);

                if (seller.name != "") {
                    Console.WriteLine($"welcome {seller.name}");
                    viewProfile();
                }
                else {
                    registerNewSeller(cp);
                }
            }
            insub = true;
            while (insub) {
                subMenu();
            }

        }


        public void submitArt()
        {

            if (active != "artist") {
                insub = true;
                Console.WriteLine("please enter your artist Id");
                artist = new mod.Artist();
                active = "artist";

                string userinput = Console.ReadLine();
                try {
                    artist.Id = int.Parse(userinput);
                }
                catch {
                    artist.Name = userinput;
                }
                ArtistRepo ap = new ArtistRepo(_context, new ArtistMapper());
               artist = ap.AddArtist(artist);

                if (artist.Name != "") {
                    Console.WriteLine($"welcome {artist.Name}");
                    viewProfile();
                }
                else {
                    registerNewArtist(ap);
                }
            }

            insub = true;
            while (insub) {
                subMenu();
            }
        }


        public void CheckForWins()
        {
            AuctionRepo ap = new AuctionRepo(_context, new AuctionMapper());
            switch (active) {
                case "artist":
                    ap.CheckForComission(artist.Id);
                    break;
                case "seller":
                    ap.CheckForSale(seller.Id);
                    break;
                case "collector":
                    ap.CheckForWin(collector.Id);
                    break;
            
            }

        }


        static void viewArt()
        {

        }
        public void viewBidsbyArt()
        {

        }

        public void viewBids()
        {
            BidRepo bp = new BidRepo(_context, new BidMapper());
            bp.ShowBidsByBidder(collector.Id);


        }
        static void viewProfile()
        {
            switch (active) {
                case "collector":
                    Console.WriteLine(collector.ToString());
                    break;
                case "artist":
                    Console.WriteLine(artist.ToString());
                    break;
                case "seller":
                    Console.WriteLine(seller.ToString());
                    break;

            }
        }
        static void exit()
        {
            Environment.Exit(0);
        }
        static void menuOptions()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (KeyValuePair<string, Action> item in actionOptions) {
                Console.WriteLine(item.Key);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;


            actionOptions[Console.ReadLine()]();
        }
        static void logout()
        {
            insub = false;
            active = "";
            collector=new mod.Collector();
            artist= new mod.Artist();
            seller=new mod.Seller();


    }
       public void bid()
        {
            listAuctions();
            Console.WriteLine("Enter the id of the Auction You would like to bid on");
            AuctionRepo cp = new AuctionRepo(_context, new AuctionMapper());
            int bd = int.Parse(Console.ReadLine());
            Auction A2Bid=cp.GetAuction(bd);
            if (A2Bid == null) {
                Console.WriteLine("please choose a valid auction");
                return;
            }
            BidRepo bp= new BidRepo(_context, new BidMapper());
            Bid bid = new Bid();
            Console.WriteLine("how much would you like to bid?");
            Decimal bidAmount = Decimal.Parse(Console.ReadLine());
            bid.Amount = bidAmount;
            Bid highBid = cp.GetHighBid(A2Bid);
            if (highBid != null) {
                if (bid.Amount <= highBid.Amount) {
                    Console.WriteLine($"Highest Bid is {highBid.Amount} please enter a bid of a higher amount");
                    return;
                }
            }
            bid.Collectorid = collector.Id;
            bid.Timeofbid = DateTime.Now;
            bid.Auctionid = A2Bid.Id;
            bp.AddBid(bid);
        }

        static void update()
        {

        }

        static void viewAuctions()
        {

        }


        public void createAuction()
        {
            
            AuctionRepo cp = new AuctionRepo(_context, new AuctionMapper());
            Console.WriteLine("Which Art Piece Do You Want to Auction Off?");
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            string artid = Console.ReadLine();
            if (ap.GetArt(int.Parse(artid),seller.Id).Name=="") {
                Console.WriteLine("this are does not exist in your inventory");
                return;
            }
            Auction au = new Auction();
            au.Artid = int.Parse(artid);
            au.Sellerid = seller.Id;
            Console.WriteLine("When Do you want this Auction to close bidding?");
            string cd = Console.ReadLine();
            DateTime closeDate = DateTime.Parse(cd);
            au.Closingdate = closeDate;
            Console.WriteLine("Minimum Bid?");
            au.Minimumamount = decimal.Parse(Console.ReadLine());
            cp.AddAuction(au);
        }




        public void getInventory()
        {
            List<Sellersinventory> inv = _context.Sellersinventories.Where(x => seller.Id == x.Sellerid).Include(y => y.Art).ToList();
           
            if (inv.Count < 1) { Console.WriteLine("You have no inventory. log an artist to attach new art to this seller."); }
            
            foreach (Sellersinventory i in inv) {
                Console.WriteLine($"ID: {i.Artid} | {i.Art.Name}");
            
            }

        }


        /*    public void GetGallery()
            {
                List<Artistcollection> ac = _context.Artistcollections.Where(x => artist.Id == x.Artistid).Include(y => y.Art).ToList();

                if (ac.Count < 1) { Console.WriteLine("You have no Art."); }

                foreach (Artistcollection i in ac) {
                    Console.WriteLine($"ID: {i.Artid} | {i.Art.Name}");

                }
            }
        */



        public void GetGallery()
        {
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            ap.ShowArtByArtist(artist.Id);

        }

        public void listAuctions()
        {
            AuctionRepo cp = new AuctionRepo(_context, new AuctionMapper());
            cp.ShowActiveAuctions();


        }


        public void attachToSeller()
        {
            SellerRepo cp = new SellerRepo(_context, new SellerMapper());
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            ap.ShowArtByArtist(artist.Id);
            Console.WriteLine("Please enter the id of the art you'de like to attach");
            int artid = int.Parse(Console.ReadLine());
            Console.WriteLine("Please Enter the id of the seller you'de like to attach to.");
            int sellid = int.Parse(Console.ReadLine());
            try {
                cp.AddInventory(artid, sellid);
            }
            catch (Exception){
                Console.WriteLine("There was an issue with attachment, please try again.");
            }

        }
        public void Start()
        {
            actionOptions.Add("buy", new Action(buyArt));
            actionOptions.Add("sell", new Action(sellArt));
            actionOptions.Add("submit", new Action(submitArt));
            actionOptions.Add("viewArt", new Action(viewArt));
            actionOptions.Add("viewBids", new Action(viewBids));
            actionOptions.Add("profile", new Action(viewProfile));
            actionOptions.Add("exit", new Action(exit));

            CollectorOptions.Add("profile", new Action(viewProfile));
            CollectorOptions.Add("exit", new Action(exit));
            CollectorOptions.Add("logout", new Action(logout));
            CollectorOptions.Add("update", new Action(update));
            CollectorOptions.Add("bid", new Action(bid));
            CollectorOptions.Add("viewCollection", new Action(viewArt));
            CollectorOptions.Add("viewBids", new Action(viewBids));
            CollectorOptions.Add("ListAuctions", new Action(listAuctions));




            SellerOptions.Add("profile", new Action(viewProfile));
            SellerOptions.Add("exit", new Action(exit));
            SellerOptions.Add("logout", new Action(logout));
            SellerOptions.Add("inventory", new Action(getInventory));
            SellerOptions.Add("createAuction", new Action(createAuction));

            ArtistOptions.Add("attach", new Action(attachToSeller));
            ArtistOptions.Add("profile", new Action(viewProfile));
            ArtistOptions.Add("gallery", new Action(GetGallery));
            ArtistOptions.Add("exit", new Action(exit));
            ArtistOptions.Add("logout", new Action(logout));

            Console.WriteLine("Welcome to Scarcity: your number one platform for CryptoArt;\n Please Enter What Fuction you'd like to perform.\n");

            while (true) {
                menuOptions();
            }
        }


    }
}