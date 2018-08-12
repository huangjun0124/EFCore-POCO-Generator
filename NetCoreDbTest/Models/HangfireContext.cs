using Microsoft.EntityFrameworkCore;
using NetCoreDbTest.Entity;

namespace NetCoreDbTest
{
    public class HangfireContext : DbContext
    {
        public DbSet<MyTable> MyTables { get; set; }
        public DbSet<JobClass> JobClass { get; set; }

        public HangfireContext(DbContextOptions<HangfireContext> options)
            : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=hangfire;Integrated Security=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobClass>().HasKey(x => x.JobId);
            modelBuilder.Entity<MyTable>().HasKey(x => x.MyId);
        }
    }
}
