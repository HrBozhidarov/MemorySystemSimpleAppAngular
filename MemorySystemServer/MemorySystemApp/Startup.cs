﻿namespace MemorySystemApp
{
    using System.Reflection;

    using MemorySystem.Data;
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystemApp.Infrastructures;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            app.UseRouting()
                .UseCors(opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .Initialize();
        }
    }
}
