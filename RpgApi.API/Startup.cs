using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;
using RpgApi.Infrastructure.Services;
using RpgApi.Infrastructure.Utils;
using Serilog;

namespace RpgApi.API
{
#pragma warning disable CA1822 // Mark members as static
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
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddTransient<IConnection, Connection>();
            services.AddTransient<ICrudService<Character, int>, CharacterService>();
            services.AddTransient<IReadUpdateService<CharacterDetail, int>, CharacterDetailService>();
            services.AddTransient<IReadUpdateService<Attributes, int>, AttributesService>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "DbTest", Version = "V1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DbTest v1"); });

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}