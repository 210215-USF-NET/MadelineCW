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
        public string OrderArtBy = "Artist";
        public string OrderAuctionBy = "Artist";

        static bool insub = true;
        public static Dictionary<string, Action> actionOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> CollectorOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> ArtistOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> SellerOptions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> optionOptions = new Dictionary<string, Action>();

        private wzvzhuteContext _context;

        public artMenu(wzvzhuteContext context) {
            _context = context;
        }
         void registerNewCollector(collectrepo cp)
        {
            Console.Clear();
            Console.WriteLine("Please register as a Collector to continue.");
            collector.Name = getinput("What is your name ?");

            collector.Location = getinput("What country are you ordering from?"); ;
            
            collector=cp.SaveCollector(collector);
            Console.WriteLine($"Thank you for registering! your customer id is: {collector.Id}");
        }

        void registerNewSeller(SellerRepo sp)
        {
            Console.Clear();
            Console.WriteLine("Please register as a Seller to continue.");
            seller.name = getinput("What is your name ?");           
            sp.Save(seller);
            Console.WriteLine($"Thank you for registering! your customer id is: {seller.Id}");
        }

        void registerNewArtist(ArtistRepo ap)
        {
            Console.Clear();
            artist.Name = getinput("Please register to continue. What is your name?");
            artist.Location = getinput("What Country are you in?");
            artist.Biography = getinput("What is your biography?");
            Console.WriteLine($"Thank you for registering! your artist id is: {artist.Id}");
            ap.Save(artist);
        }

         void buyArt()
        {
            Console.Clear();
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

                    string input = getinput("select an option");
                    if (CollectorOptions.ContainsKey(input)) {
                        CollectorOptions[input]();
                        
                    }
                  
                    break;
                case "artist":
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (KeyValuePair<string, Action> item in ArtistOptions) {
                        Console.WriteLine(item.Key);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string input1 = getinput("select an option");
                    if (ArtistOptions.ContainsKey(input1)) {
                        ArtistOptions[input1]();
                    }
                  
                    break;
                case "seller":

                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (KeyValuePair<string, Action> item in SellerOptions) {
                        Console.WriteLine(item.Key);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    string input2 = getinput("select an option");
                    if (SellerOptions.ContainsKey(input2)) {
                        SellerOptions[input2]();
                    }
                  
                    break;

            }
        }


        public void sellArt()
        {
            Console.Clear();
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
            Console.Clear();
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

                if (artist.Name != null) {
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


        public void viewArt()
        {
            Console.Clear();
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            
            if (OrderArtBy == "Price") {
                ap.ShowArtByPrice();
            }
            else {
                ap.ShowAll();
            }

            }
        public void viewArtByCollector()
        {
            Console.Clear();
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            ap.ShowArtByCollector(collector.Id);
        }
            public void viewBidsbyArt()
        {

        }

        public void viewBids()
        {
            Console.Clear();
            BidRepo bp = new BidRepo(_context, new BidMapper());
            bp.ShowBidsByBidder(collector.Id);


        }
        static void viewProfile()
        {
            Console.Clear();
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
        public void menuOptions()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (KeyValuePair<string, Action> item in actionOptions) {
                Console.WriteLine(item.Key);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            string input =getinput("select an option");
            if (actionOptions.ContainsKey(input)) {
                actionOptions[input]();
               // Console.Clear();
            }
           
        }
        static void logout()
        {
            Console.Clear();
            insub = false;
            active = "";
            collector=new mod.Collector();
            artist= new mod.Artist();
            seller=new mod.Seller();


    }
       public void bid()
        {
            Console.Clear();
            listAuctions();

            AuctionRepo cp = new AuctionRepo(_context, new AuctionMapper());
            int bd = getint("Enter the id of the Auction You would like to bid on");
            Auction A2Bid=cp.GetAuction(bd);
            if (A2Bid == null) {
                Console.WriteLine("please choose a valid auction");
                return;
            }
            BidRepo bp= new BidRepo(_context, new BidMapper());
            Bid bid = new Bid();
            bid.Amount = getdec("how much would you like to bid?"); ;
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
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            int artid = getint("Which Art Piece Do You Want to Auction Off?");
            if (ap.GetArt(artid,seller.Id).Name=="") {
                Console.WriteLine("this are does not exist in your inventory");
                return;
            }
            Auction au = new Auction();

            au.Artid = artid;
            au.Sellerid = seller.Id;
            au.Closingdate = getDate("When Do you want this Auction to close bidding?");
            //au.Minimumamount = getdec("Minimum Bid?");
            au.Minimumamount = 0.00m;
            cp.AddAuction(au);
        }




        public void getInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            List<Sellersinventory> inv = _context.Sellersinventories.Where(x => seller.Id == x.Sellerid).Include(y => y.Art).ThenInclude(k=>k.Artist).ToList().OrderBy(z=>z.Art.Artist.Name).ToList();
           
            if (inv.Count < 1) { Console.WriteLine("You have no inventory. log an artist to attach new art to this seller."); }
            string lastArtist = "";
            foreach (Sellersinventory i in inv) {
                if (i.Art.Artist.Name != lastArtist) {
                    lastArtist = i.Art.Artist.Name;
                    Console.WriteLine("---------------------");
                    Console.WriteLine($"Art by {lastArtist}\n");
                }
                Console.WriteLine($"ID: {i.Artid} | {i.Art.Name}");
            
            }
            Console.WriteLine("---------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }


        public void GetGallery()
        {
            Console.Clear();
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            ap.ShowArtByArtist(artist.Id);
        }

        public void listAuctions()
        {
            Console.Clear();
            AuctionRepo cp = new AuctionRepo(_context, new AuctionMapper());
            cp.ShowActiveAuctions();


        }



        public string getinput(string prompt)
        {
            Console.WriteLine(prompt);
            try {
                string val= Console.ReadLine();
                if (val == "exit") { exit(); }
                return val;
            }
            catch (Exception) {
                Console.WriteLine("please enter a valid string");
                return getinput(prompt);
            }

        }


        public decimal getdec(string prompt)
        {
            Console.WriteLine(prompt);
            try {
                string val = Console.ReadLine();
                if (val == "exit") { exit(); }
               
                return decimal.Parse(val);
            }
            catch (Exception) {
                Console.WriteLine("only decimals are valid");
                return getdec(prompt);
            }

        }

        public DateTime getDate(string prompt)
        {
            Console.WriteLine(prompt);
            try {
                string val = Console.ReadLine();
                if (val == "exit") { exit(); }

                return DateTime.Parse(val);
            }
            catch (Exception) {
                Console.WriteLine("only numbers are valid");
                return getDate(prompt);
            }

        }

        public int getint(string prompt)
        {
            Console.WriteLine(prompt);
            try {
                string val = Console.ReadLine();
                if (val == "exit") { exit(); }
                
                return int.Parse(val);
            }
            catch(Exception) {
                Console.WriteLine("only numbers are valid");
                return getint(prompt);
            }

        }

        public void attachToSeller()
        {
            Console.Clear();
            SellerRepo cp = new SellerRepo(_context, new SellerMapper());
            ArtRepo ap = new ArtRepo(_context, new ArtMapper());
            ap.ShowArtByArtist(artist.Id);
            int artid = getint("Please enter the id of the art you'de like to attach.");
            int sellid = getint("Please Enter the id of the seller you'de like to attach to.");
            try {
                cp.AddInventory(artid, sellid);
            }
            catch (Exception){
                Console.WriteLine("There was an issue with attachment, please try again.");
            }

        }

        public void setOrderBy()
        {
            int optionorderBy = getint("What do you want to order by?\n1) Artist\n2)Price");
        if (optionorderBy == 2) { 
            OrderArtBy = "Price";
            OrderAuctionBy = "Price";
        }else
        {
                OrderArtBy = "Artist";
                OrderAuctionBy = "Artist";
            }
    }

        public void Start()
        {
            actionOptions.Add("buy", new Action(buyArt));
            actionOptions.Add("sell", new Action(sellArt));
            actionOptions.Add("submit", new Action(submitArt));
            actionOptions.Add("viewArt", new Action(viewArt));
            actionOptions.Add("ListAuctions", new Action(listAuctions));
            actionOptions.Add("exit", new Action(exit));
            actionOptions.Add("orderBy", new Action(setOrderBy));

            CollectorOptions.Add("profile", new Action(viewProfile));
            CollectorOptions.Add("exit", new Action(exit));
            CollectorOptions.Add("logout", new Action(logout));
            CollectorOptions.Add("update", new Action(update));
            CollectorOptions.Add("bid", new Action(bid));
            CollectorOptions.Add("viewCollection", new Action(viewArtByCollector));
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