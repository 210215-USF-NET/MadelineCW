using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace auctionBL
{
    public static class logging
    {

        public static void init()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
          
        }
        public static void log(string info)
        {
            Log.Information(info);
            Log.CloseAndFlush();
        }
    }
}
