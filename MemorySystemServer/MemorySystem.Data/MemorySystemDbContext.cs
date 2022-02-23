namespace MemorySystem.Data
{
    using MemorySystem.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class MemorySystemDbContext : IdentityDbContext<
        User,
        Role,
        string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public MemorySystemDbContext(DbContextOptions<MemorySystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Like>().HasKey(e => new { e.UserId, e.PictureId });

            builder.Entity<Like>()
                .HasOne(e => e.User)
                .WithMany(e => e.Likes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.Likes)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite>().HasKey(e => new { e.UserId, e.PictureId });

            builder.Entity<Favorite>()
                .HasOne(e => e.User)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Picture>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.Pictures)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
