using System;
namespace auctionUI
{
    public class artMenu : Imenu
    {
        private string _menuText = "welcome to the cryptoart text gallery";
        public void Start()
        {
            Console.WriteLine(_menuText);
        }


    }
}