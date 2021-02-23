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
            collector.Id = int.Parse(Console.ReadLine());
            collectrepo cp = new collectrepo();
            collector=cp.AddCollector(collector);
            if (collector.registered) { Console.WriteLine($"welcome {collector.Name}"); }
            else {
                Console.WriteLine("Please register to continue. What is your name?");

                collector.Name = Console.ReadLine();
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

        static void Main(string[] args)
        {
            logging.init();


            Console.WriteLine("Welcome to Scarcity: your number one platform for CryptoArt;\n Please Enter What Fuction you'd like to perform.\n");
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("buyArt\nsellArt\nsubmitArt\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Dictionary<string, Action> actionOptions = new Dictionary<string, Action>();
            actionOptions.Add("buy", new Action(buyArt));
            actionOptions.Add("sell", new Action(sellArt));
            actionOptions.Add("submit", new Action(submitArt));
            actionOptions[Console.ReadLine()]();



        }
    }
}
