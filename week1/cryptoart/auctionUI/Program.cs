using System;
using System.Drawing;

using auctionDL;
namespace auctionUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Collector CurrentUser = new Collector();
            CurrentUser.Id = 999;
            Console.WriteLine("Welcome to Scarcity: your number one platform for CryptoArt;\n Please Enter your customer Id to get Started.");
            CurrentUser.Id=int.Parse(Console.ReadLine());
            collectrepo cp = new collectrepo();
            cp.addCollector(CurrentUser);
             



        }
    }
}
