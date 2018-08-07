using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HubPortal.Api {

    public class Startup {

        public Startup(IConfiguration configuration) {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            // Add service and create Policy with options
            services.AddMvc();
            //.AddJsonOptions(options => {
            //    options.SerializerSettings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors("MyPolicy");

            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "api/{controller=TransactionLookup}/{action=Get}/{id?}"
                    );
            });
        }
    }
}