using Microsoft.EntityFrameworkCore;
using SearchWedApi.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SearchWedApi.Interfaces
{
    public interface ISearchDbContext
    {
        DbSet<Metrics> Metrics { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
