using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitterDataBase;

namespace TwitterSampleStreamAPI
{

    public class ErrorHandlerFilter : IExceptionFilter
    {
        ITwitterLogger twitterLogger;

        public ErrorHandlerFilter()
        {
            var host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                twitterLogger = services.GetRequiredService<ITwitterLogger>();
            }
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                twitterLogger.Log(context.Exception.Message, context.Exception);
            }
        }
    }
}
