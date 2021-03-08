using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TwitterDataBase;
using TwitterLibrary;

namespace TwitterSampleStreamAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ITwitterLogger twitterLogger;
            ITwitterDataHandler dataHandler;
            ITwitterEngine twitterEngine;
            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                dataHandler = services.GetRequiredService<ITwitterDataHandler>();
                twitterEngine = services.GetRequiredService<ITwitterEngine>();
                twitterLogger = services.GetRequiredService<ITwitterLogger>();
            }
            try
            {
                Thread Th = new Thread(new ThreadStart(() => twitterEngine.StartSampleStream(dataHandler.HandleTweet)));
                Th.Start();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                twitterLogger.Log(ex.Message, ex);
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
