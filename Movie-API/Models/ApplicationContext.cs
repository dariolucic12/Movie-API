using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Movie_API.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>().HasNoKey();
            modelBuilder.Entity<Watchlist>().HasNoKey();
        }

        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
