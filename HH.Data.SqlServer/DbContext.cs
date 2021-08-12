using HH.Core;
using Microsoft.EntityFrameworkCore;

namespace HH.Data.SqlServer
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(x => x.Id).HasColumnName("CompanyId");
            });

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new SummaryConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Summary> Summaries { get; set; }
    }
}
