using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using GiveFreely.Api.Extensions;
using GiveFreely.Common;


namespace GiveFreely.Api
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
            services.RegisterRepository();
            services.RegisterEngines();
            services.RegisterDatabaseContext(Configuration);
            services.RegisterValidation();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(SystemParameters.SwaggerVersion, new OpenApiInfo
                {
                    Title = SystemParameters.SwaggerTitle,
                    Version = SystemParameters.SwaggerVersion,
                    Description = SystemParameters.SwaggerDescription
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint(SystemParameters.SwaggerURL, SystemParameters.SwaggerTitle));
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
