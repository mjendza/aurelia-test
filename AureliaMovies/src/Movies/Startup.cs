using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using AureliaMovies.Data;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace Movies
{
    public class Startup
    {

        public Startup()
        {
            Configuration = new Configuration()
                                .AddJsonFile("config.json");
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(options =>
            {
                options.OutputFormatters                       
                       .Select(f => f.Instance as JsonOutputFormatter)
                       .First(f => f != null)
                       .SerializerSettings
                       .ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<MoviesData>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}");
            });

            var seeder = new DatabaseSeed(new MoviesData(Configuration));
            seeder.Seed();
        }
    }
}
