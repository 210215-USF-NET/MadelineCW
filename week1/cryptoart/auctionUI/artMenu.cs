using System;
using System.Collections.Generic;
using auctionDL;
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
           // collector.registered = true;
            Console.WriteLine("What country are you ordering from?");
            collector.Location = Console.ReadLine();
            Console.WriteLine($"Thank you for registering! your customer id is: {collector.Id}");
            cp.SaveCollector(collector);

        }

        void registerNewSeller(SellerRepo sp)
        {
            Console.WriteLine("Please register as a Seller to continue.\n What is your name?");
            seller.name = Console.ReadLine();
            // collector.registered = true;
            Console.WriteLine($"Thank you for registering! your customer id is: {seller.Id}");
            sp.Save(seller);

        }

        void registerNewArtist()
        {

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
            while (insub) {
                subMenu();
                /*
                try { menuOptions(); }
                catch {
                    Console.WriteLine("please enter a valid option");
                    
                }
                */
            }

            

        }


        static void subMenu()
        {
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


        static void submitArt()
        {
            Console.WriteLine("please enter artist Id");
            artist = new mod.Artist();
            active = "artist";
            artist.Id = int.Parse(Console.ReadLine());
            ArtistRepo cp = new ArtistRepo();
            artist = cp.AddArtist(artist);
            if (artist.registered) { Console.WriteLine($"welcome {artist.Name}"); }
            else {
                Console.WriteLine("Please register to continue. What is your name?");
                artist.Name = Console.ReadLine();
                artist.registered = true;
                Console.WriteLine("What is your biography?");
                artist.Biography = Console.ReadLine();
                Console.WriteLine($"Thank you for registering! your artist id is: {artist.Id}");
                cp.Save(artist);
            }
        }
        static void viewArt()
        {

        }
        static void viewBids()
        {

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
//            Console.WriteLine("buyArt\nsellArt\nsubmitArt\n");
            Console.ForegroundColor = ConsoleColor.Yellow;


            actionOptions[Console.ReadLine()]();
        }
        static void logout()
        {
            insub = false;
            
        }
        static void bid()
        {

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
            if (ap.GetArt(int.Parse(artid),seller.Id)==null) {
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






        public void listAuctions()
        {
            AuctionRepo cp = new AuctionRepo(_context, new AuctionMapper());
            cp.ShowActiveAuctions();


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
            CollectorOptions.Add("viewCurrentAuctions", new Action(viewAuctions));
            CollectorOptions.Add("ListAuctions", new Action(listAuctions));




            SellerOptions.Add("profile", new Action(viewProfile));
            SellerOptions.Add("exit", new Action(exit));
            SellerOptions.Add("logout", new Action(logout));
            SellerOptions.Add("createAuction", new Action(createAuction));

            ArtistOptions.Add("profile", new Action(viewProfile));
            ArtistOptions.Add("exit", new Action(exit));
            ArtistOptions.Add("logout", new Action(logout));

            Console.WriteLine("Welcome to Scarcity: your number one platform for CryptoArt;\n Please Enter What Fuction you'd like to perform.\n");

            while (true) {
                menuOptions();
                /*
                try { menuOptions(); }
                catch {
                    Console.WriteLine("please enter a valid option");
                    
                }
                */
            }
        }


    }
}