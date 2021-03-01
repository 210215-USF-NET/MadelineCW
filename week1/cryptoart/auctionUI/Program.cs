using System;
using Serilog;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Configuration;
using auctionBL;
using auctionDL;

namespace auctionUI
{
    class Program
    {

        static void Main(string[] args)
        {
            logging.init();
        
            string connectionString = ConfigurationManager.AppSettings.Get("dbconnect");
            DbContextOptions<wzvzhuteContext> options = new DbContextOptionsBuilder<wzvzhuteContext>()
            .UseNpgsql(connectionString)
            .Options;
            using var context = new wzvzhuteContext(options);

            artMenu menu = new artMenu(context);
            menu.Start();



        }
    }
}
