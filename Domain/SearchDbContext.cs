using Microsoft.EntityFrameworkCore;
using SearchWedApi.Domain.Configuration;
using SearchWedApi.Interfaces;

namespace SearchWedApi.Domain
{
    public sealed class SearchDbContext : DbContext, ISearchDbContext
    {
        public DbSet<Metrics> Metrics { get; set; }

        public SearchDbContext(DbContextOptions<SearchDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MetricsConf());
            base.OnModelCreating(builder);
        }
    }
}
