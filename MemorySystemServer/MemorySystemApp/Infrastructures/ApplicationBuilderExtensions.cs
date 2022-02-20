namespace MemorySystemApp.Infrastructures
{
    using System.Collections.Generic;
    using System.Linq;

    using MemorySystemApp.Data;
    using MemorySystemApp.Data.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<MemorySystemDbContext>();

            db.Database.Migrate();

            if (db.Categories.Any())
            {
                return app;
            }

            foreach (var category in GetData())
            {
                db.Categories.Add(category);
            }

            db.SaveChanges();

            return app;
        }

        private static IEnumerable<Category> GetData()
           => new List<Category>
           {
                new Category { Type = CategoryType.Animal },
                new Category { Type = CategoryType.Education },
                new Category { Type = CategoryType.Love },
                new Category { Type = CategoryType.Nature },
                new Category { Type = CategoryType.Sport },
                new Category { Type = CategoryType.Travel },
           };
    }
}
