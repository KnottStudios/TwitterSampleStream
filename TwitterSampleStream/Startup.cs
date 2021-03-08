using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using TwitterDataBase;
using TwitterLibrary;
using TwitterSampleStreamAPI.Database;
using TwitterSampleStreamAPI.Handlers;
using TwitterSampleStreamAPI.Managers;

namespace TwitterSampleStreamAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            AddDependencyInjection(services);
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
        }

        private static void AddDependencyInjection(IServiceCollection services)
        {
            var config = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

            services.AddSingleton<ITwitterLogger>(l => new FileLogger(config["File_Logger_Path"], config["File_Logger_File"]));
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IEmojiHandler, EmojiHandler>();
            services.AddTransient<IUrlHandler, UrlHandler>();
            services.AddTransient<IUrlManager, UrlManager>();
            services.AddTransient<IEmojiManager, EmojiManager>();
            services.AddTransient<ITweetManager, TweetManager>();
            services.AddSingleton<ITwitterLocalStorage, TwitterLocalStorage>();
            services.AddSingleton<ITwitterDataHandler, TwitterDataHandler>();
            services.AddSingleton<ITwitterEngine>(s => new TwitterEngine(config["ConsumerKey"], config["ConsumerSecret"], config["AccessToken"], config["AccessSecret"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger( c => 
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwitterSampleStream V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
