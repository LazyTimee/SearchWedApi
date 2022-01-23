using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SearchWedApi.Domain.Configuration
{
    public class MetricsConf : IEntityTypeConfiguration<Metrics>
    {
        public void Configure(EntityTypeBuilder<Metrics> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id).IsUnique();
        }
    }
}
