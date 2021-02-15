using System;


namespace helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names={"mads","madeline","maddy","Madzy","Ms Williams","hey you","pretty lady","rockstar","crazy woman", "friend","happy cool"};
            for(int i=0;i<10;++i){
                Random r = new Random();
                int gr= r.Next(0,names.Length);
            Console.WriteLine($"Hello {names[gr]}!");
            }
            //string name = Console.ReadLine();
           // Console.WriteLine($"hi {name}");
        }
    }
}
