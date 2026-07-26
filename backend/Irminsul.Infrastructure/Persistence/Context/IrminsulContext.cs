using Microsoft.EntityFrameworkCore;
using Irminsul.Domain.Entities;

namespace Irminsul.Infrastructure.Persistence.Context
{
    public class IrminsulContext : DbContext
    {
        public IrminsulContext(DbContextOptions<IrminsulContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IrminsulContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}