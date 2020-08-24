using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DataCheckingToolApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(baseDirectory, "log/log.txt"))
                .CreateLogger();
            //CreateHostBuilder(args).Build().Run();
            try
            {
                Log.Information("Starting web host");

                CreateHostBuilder(args)
                .Build().Run();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<Startup>();

            })
            .UseAutofac();
    }
}
