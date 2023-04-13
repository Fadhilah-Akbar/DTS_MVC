using DTS_WebApplication.Models;
using DTS_WebApplication.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DTS_WebApplication.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        { 
        }

        //set entity In MyContext
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        //fluet API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //cardinality on entities
            modelBuilder.Entity<University>()
                    .HasMany(u => u.Educations)
                    .WithOne(e => e.University)
                    .HasForeignKey(u => u.UniversityId)
                    .OnDelete(DeleteBehavior.NoAction);

            // one profiling has one education
            modelBuilder.Entity<Profiling>()
                        .HasOne(p => p.Education)
                        .WithOne(e => e.Profiling)
                        .HasForeignKey<Profiling>(p => p.EducationId)
                        .OnDelete(DeleteBehavior.NoAction);

            // one employee has one profiling
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Profiling)
                        .WithOne(p => p.Employee)
                        .HasForeignKey<Profiling>(p => p.EmployeeNIK)
                        .OnDelete(DeleteBehavior.NoAction);

            // one employee has one account
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Account)
                        .WithOne(a => a.Employee)
                        .HasForeignKey<Account>(a => a.EmployeeNIK)
                        .OnDelete(DeleteBehavior.NoAction);

            // one account has many account roles
            modelBuilder.Entity<Account>()
                        .HasMany(a => a.AccountRoles)
                        .WithOne(ar => ar.Account)
                        .HasForeignKey(a => a.EmployeeNIK)
                        .OnDelete(DeleteBehavior.NoAction);

            // one role has many account roles
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.AccountRoles)
                        .WithOne(ar => ar.Role)
                        .HasForeignKey(r => r.RoleId)
                        .OnDelete(DeleteBehavior.NoAction);

            /*modelBuilder.Entity<RegisterVM>()
                    .HasNoKey();*/
            /*modelBuilder.Entity<LoginVM>()
                    .HasNoKey();*/
        }

        //fluet API
        //public DbSet<DTS_WebApplication.ViewModels.LoginVM>? LoginVM { get; set; }

        //fluet API
        //public DbSet<DTS_WebApplication.ViewModels.RegisterVM>? RegisterVM { get; set; }
    }
}
