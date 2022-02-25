namespace MemorySystemApp
{
    using System.Reflection;

    using MemorySystem.App.Infrastructures.Middlewares;
    using MemorySystem.Controllers;
    using MemorySystem.Data;
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services;
    using MemorySystemApp.Infrastructures;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MemorySystemDbContext>(options => options
                .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")))
                .AddIdentity()
                .JwtAuthentication(services.GetApplicationSettings(this.Configuration))
                .AddServices()
                .AddControllers()
                .AddNewtonsoftJson();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Result).GetTypeInfo().Assembly,
                typeof(ApiController).GetTypeInfo().Assembly);

            app.UseRouting()
                .UseCors(opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseAuthentication()
                .UseAuthorization()
                .UseMiddleware<ErrorHandlerMiddleware>()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .Initialize();
        }
    }
}
