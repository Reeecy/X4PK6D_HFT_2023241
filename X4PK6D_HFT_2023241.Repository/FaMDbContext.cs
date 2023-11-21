using Microsoft.EntityFrameworkCore;
using System;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public class FaMDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Pass> Passes { get; set; }
        public DbSet<EntriesExits> EntriesExits { get; set; }

        public FaMDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasOne(p => p.Pass).WithMany(p => p.Persons).HasForeignKey(p => p.PassId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EntriesExits>().HasOne(e => e.Person).WithMany(e => e.EntriesExits).HasForeignKey(e => e.PersonId).OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

    }
}
