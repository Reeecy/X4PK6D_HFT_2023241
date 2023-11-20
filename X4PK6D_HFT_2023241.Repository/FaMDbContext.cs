using Microsoft.EntityFrameworkCore;
using System;

namespace X4PK6D_HFT_2023241.Repository
{
    public class FaMDbContext : DbContext
    {
        public FaMDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
