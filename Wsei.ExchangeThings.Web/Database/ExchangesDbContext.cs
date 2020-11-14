using Microsoft.EntityFrameworkCore;
using Wsei.ExchangeThings.Web.Entities;

namespace Wsei.ExchangeThings.Web.Database
{
    public class ExchangesDbContext : DbContext
    {
        public ExchangesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ItemEntity> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // fluent configuration ...
        }
    }
}
