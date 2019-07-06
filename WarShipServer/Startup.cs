using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarShipServer.Models;
using WarShipServer.Services;

namespace WarShipServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<Field>();            
            services.AddSingleton<Fleet>();
            services.AddSingleton<ShipsAligner>();
            services.AddSingleton<PointsValidator>();
            services.AddSingleton<SquaresManager>();
            services.AddSingleton<PointsManager>();
            services.AddSingleton<Game>();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => { builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials(); }));
        }
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}