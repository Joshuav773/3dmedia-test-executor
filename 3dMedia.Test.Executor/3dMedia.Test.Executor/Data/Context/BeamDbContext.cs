using Microsoft.EntityFrameworkCore;

namespace _3dMedia.Test.Executor.Data.Context
{
    public class BeamDbContext : DbContext
    {
        public BeamDbContext(DbContextOptions<BeamDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.Test>().ToTable("Test").HasKey(t => t.Id);
        }
    }
}
