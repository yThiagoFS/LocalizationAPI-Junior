using BaltaIoChallenge.WebApi.Data.v1.Mappings;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaltaIoChallenge.WebApi.Data.v1.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<IBGE> Ibge { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new IBGEMap());
        }
    }
}
