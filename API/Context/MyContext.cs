using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //University-Education Relationship
            {
                //modelBuilder.Entity<University>()
                //    .HasMany(e => e.Education)
                //    .WithOne(u => u.University);


                modelBuilder.Entity<Education>()
                    .HasOne(u => u.University)
                    .WithMany(e => e.Education);
            }

            //Employee-Account Relationship
            {
                //modelBuilder.Entity<Employee>()
                //    .HasOne(a => a.Account)
                //    .WithOne(e => e.Employee);

                modelBuilder.Entity<Account>()
                    .HasOne(e => e.Employee)
                    .WithOne(a => a.Account)
                    .HasForeignKey<Employee>(e => e.NIK);
            }

            //Account-Profiling Relationship
            {
                //modelBuilder.Entity<Account>()
                //    .HasOne(p => p.Profiling)
                //    .WithOne(a => a.Account);

                modelBuilder.Entity<Profiling>()
                    .HasOne(a => a.Account)
                    .WithOne(p => p.Profiling)
                    .HasForeignKey<Account>(a => a.NIK);
            }

            //Profiling-Education Relationship
            {
                //modelBuilder.Entity<Profiling>()
                //    .HasOne(e => e.Education)
                //    .WithMany(p => p.Profiling);


                modelBuilder.Entity<Education>()
                    .HasMany(p => p.Profiling)
                    .WithOne(e => e.Education);
            }

            //Account-AccountRoles Relationship
            {
                modelBuilder.Entity<Account>()
                    .HasMany(ar => ar.AccountRoles)
                    .WithOne(a => a.Accounts)
                    .OnDelete(DeleteBehavior.ClientCascade);
            }

            //Role-AccountRoles Relationship
            {
                modelBuilder.Entity<Role>()
                    .HasMany(ar => ar.AccountRoles)
                    .WithOne(r => r.Roles)
                    .OnDelete(DeleteBehavior.ClientCascade);
            }
        }
    }
}
