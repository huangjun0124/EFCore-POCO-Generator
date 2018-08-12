using Microsoft.EntityFrameworkCore;
using NetCoreDbTest.Entity;

namespace NetCoreDbTest
{
    public class HangfireContext : DbContext
    {
		#region DbSet fields definition
        		public DbSet<Schema> Schemata { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<JobParameter> JobParameters { get; set; }
		public DbSet<JobQueue> JobQueues { get; set; }
		public DbSet<Server> Servers { get; set; }
		public DbSet<List> Lists { get; set; }
		public DbSet<Set> Sets { get; set; }
		public DbSet<Counter> Counters { get; set; }
		public DbSet<Hash> Hashes { get; set; }
		public DbSet<AggregatedCounter> AggregatedCounters { get; set; }
		public DbSet<JobParamView> JobParamViews { get; set; }
		public DbSet<View2> View2 { get; set; }
		public DbSet<MyTable> MyTables { get; set; }
		public DbSet<YourTable> YourTables { get; set; }
		public DbSet<Testtypenotnull> Testtypenotnulls { get; set; }
		public DbSet<Testtypenull> Testtypenulls { get; set; }

		#endregion

        public HangfireContext(DbContextOptions<HangfireContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			#region Entity HasKey Mapping
            			modelBuilder.Entity<Schema>().HasKey(x => x.Version);
			modelBuilder.Entity<Job>().HasKey(x => x.Id);
			modelBuilder.Entity<State>().HasKey(x => x.Id);
			modelBuilder.Entity<JobParameter>().HasKey(x => x.Id);
			modelBuilder.Entity<JobQueue>().HasKey(x => x.Id);
			modelBuilder.Entity<Server>().HasKey(x => x.Id);
			modelBuilder.Entity<List>().HasKey(x => x.Id);
			modelBuilder.Entity<Set>().HasKey(x => x.Id);
			modelBuilder.Entity<Counter>().HasKey(x => x.Id);
			modelBuilder.Entity<Hash>().HasKey(x => x.Id);
			modelBuilder.Entity<AggregatedCounter>().HasKey(x => x.Id);
			modelBuilder.Entity<JobParamView>().HasKey(x => new { x.InvocationData, x.Arguments, x.CreatedAt, x.ParamName });
			modelBuilder.Entity<View2>().HasKey(x => new { x.Id, x.InvocationData, x.Arguments, x.CreatedAt, x.Expr1, x.JobId, x.Name });
			modelBuilder.Entity<MyTable>().HasKey(x => x.MyId);
			modelBuilder.Entity<YourTable>().HasKey(x => x.YourTableId);
			modelBuilder.Entity<Testtypenotnull>().HasKey(x => x.Pkid);
			modelBuilder.Entity<Testtypenull>().HasKey(x => x.Pkid);

			#endregion
        }
    }
}
