using Microsoft.EntityFrameworkCore;
using NetCoreDbTest.Entity;

namespace NetCoreDbTest
{
    public class HangfireContext : DbContext
    {
		#region DbSet fields definition
		public DbSet<Job> Jobs { get; set; }
		public DbSet<JobParameter> JobParameters { get; set; }
		public DbSet<Server> Servers { get; set; }
		public DbSet<List> Lists { get; set; }
		public DbSet<Set> Sets { get; set; }
		public DbSet<Hash> Hashes { get; set; }
		public DbSet<AggregatedCounter> AggregatedCounters { get; set; }

		#endregion

        public HangfireContext(DbContextOptions<HangfireContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			#region Entity HasKey Mapping
			modelBuilder.Entity<Job>().HasKey(x => x.Id);
			modelBuilder.Entity<JobParameter>().HasKey(x => x.Id);
			modelBuilder.Entity<Server>().HasKey(x => x.Id);
			modelBuilder.Entity<List>().HasKey(x => x.Id);
			modelBuilder.Entity<Set>().HasKey(x => x.Id);
			modelBuilder.Entity<Hash>().HasKey(x => x.Id);
			modelBuilder.Entity<AggregatedCounter>().HasKey(x => x.Id);

			#endregion
        }
    }
}
