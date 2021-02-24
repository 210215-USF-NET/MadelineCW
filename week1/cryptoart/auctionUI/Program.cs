using System;
using Serilog;
using System.Collections.Generic;
using auctionBL;
using auctionDL;
namespace auctionUI
{
    class Program
    {
        public static Collector collector;
        public static Artist artist;
        public static Seller seller;
        public static string active = "";
        public static Dictionary<string, Action> actionOptions = new Dictionary<string, Action>();

        void registerNewCollector()
        {

        }

        void registerNewSeller()
        {

        }

        void registerNewArtist()
        {

        }

        static void buyArt()
        {
            Console.WriteLine("please enter your customer Id");
            collector = new Collector();
            active = "collector";
            collector.Id = int.Parse(Console.ReadLine());
            collectrepo cp = new collectrepo();
            collector=cp.AddCollector(collector);
            if (collector.registered) { Console.WriteLine($"welcome {collector.Name}"); }
            else {
                Console.WriteLine("Please register to continue. What is your name?");
                collector.Name = Console.ReadLine();
                collector.registered = true;
                Console.WriteLine("What country are you ordering from?");
                collector.Location = Console.ReadLine();
                collector.registered = true;
                Console.WriteLine($"Thank you for registering! your customer id is: {collector.Id}");
                cp.SaveCollector(collector);
            }
        }
        static void sellArt()
        {
            Console.WriteLine("please enter your seller Id");

        }

        static void submitArt()
        {
            Console.WriteLine("please enter artist Id");

        }

        static void viewArt()
        {

        }
        static void viewBids()
        {

        }
        static void viewProfile()
        {
            switch(active){
                case "collector":
                    Console.WriteLine(collector.ToString());
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
            Console.WriteLine("buyArt\nsellArt\nsubmitArt\n");
            Console.ForegroundColor = ConsoleColor.Yellow;


            actionOptions[Console.ReadLine()]();
        }

        static void Main(string[] args)
        {
            logging.init();
            actionOptions.Add("buy", new Action(buyArt));
            actionOptions.Add("sell", new Action(sellArt));
            actionOptions.Add("submit", new Action(submitArt));
            actionOptions.Add("viewArt", new Action(viewArt));
            actionOptions.Add("viewBids", new Action(viewBids));
            actionOptions.Add("profile", new Action(viewProfile));
            actionOptions.Add("exit", new Action(exit));
            Console.WriteLine("Welcome to Scarcity: your number one platform for CryptoArt;\n Please Enter What Fuction you'd like to perform.\n");

            while (true) {

                try { menuOptions(); }
                catch {
                    Console.WriteLine("please enter a valid option");
                    
                }

            }



        }
    }
}
