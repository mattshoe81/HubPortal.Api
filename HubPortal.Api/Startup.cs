using Amazon.Kinesis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HubPortal.Api {

    public class Startup {

        //public Startup(IConfiguration configuration) {
        public Startup(IHostingEnvironment env /*IConfiguration configuration*/) {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
            //this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors("CorsPolicy");

            //app.UseMiddleware<Utilities.KinesisLogger>();

            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "api/{controller}/{action?}/{id?}"
                    );
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc();

            services.AddDefaultAWSOptions(this.Configuration.GetAWSOptions());
            //services.AddAWSService<IAmazonKinesis>();
        }
    }
}
