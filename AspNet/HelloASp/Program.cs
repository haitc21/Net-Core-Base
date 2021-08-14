using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloASp
{
    public class Program
    {
        /** 
        1. tao ra 1 doi tuong Ihost host
            -- Trien khai DI qua doi tuong IserviceProvider va IServiceCollection de dang ki DI
            -- Logging (Ilogging)
            -- Configuration 
            -- IHostedService co phuong thuc StartedAsync (chay cai ham Run)  khi chay se chay may chu HTTp (Ketrel HTTp)

        */

        public static void Main(string[] args)
        {
            IHostBuilder b = Host.CreateDefaultBuilder(args);
            // Cau hinh mac dinh cho Host
            b.ConfigureWebHostDefaults(webBuilder =>
           {
               // Tuy bien them ve host
               webBuilder.UseStartup<MyStartup>();
              // webBuilder.UseWebRoot("ChoLayStaticFile"); // k ;ay static file o wwwroot
           });
            IHost host = b.Build();
            host.Run();

        }


        // public static void Main(string[] args)
        // {
        //     CreateHostBuilder(args).Build().Run();
        // }

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });
    }
}
