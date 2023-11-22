using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            optionsBuilder.UseInMemoryDatabase("fandm");
            //string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\fandm.mdf"";Integrated Security=True;MultipleActiveResultSets=True";
            //string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Visual Studio Projects\X4PK6D_HFT_2023241\X4PK6D_HFT_2023241.Repository\fandm.mdf"";Integrated Security=True;MultipleActiveResultSets=True";
            //optionsBuilder.UseSqlServer(conn);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EntriesExits>().HasOne(e => e.Person).WithMany(e => e.EntriesExits).HasForeignKey(e => e.PersonId).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Person>().HasOne(p => p.Pass).WithMany(p => p.Persons).HasForeignKey(p => p.PassId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EntriesExits>(e => e
            .HasOne(e => e.Person)
            .WithMany(person => person.EntriesExits)
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Person>(person => person
            .HasOne(person => person.Pass)
            .WithMany(pass => pass.Persons)
            .HasForeignKey(person => person.PassId)
            .OnDelete(DeleteBehavior.Cascade));

            var person1 = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1980, 1, 1),
                Address = "123 Main St, Cityville",
                PhoneNumber = "555-1234",
                Email = "john.doe@example.com",
                IsStudent = false,
                IsRetired = false,
                PassId = 1,
            };

            var person2 = new Person
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1990, 5, 15),
                Address = "456 Oak St, Townsville",
                PhoneNumber = "555-5678",
                Email = "jane.smith@example.com",
                IsStudent = true,
                IsRetired = false,
                PassId = 2,
            };

            // Create Passes
            var pass1 = new Pass
            {
                Id = 1,
                PassType = "Monthly",
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddMonths(1),
                Price = 50,
                CrossfitGymUsage = true,
                GroupTrainingUsage = true,
                PoolUsage = false,
                SaunaUsage = false,
                MassageUsage = false,
            };

            var pass2 = new Pass
            {
                Id = 2,
                PassType = "Yearly",
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now.AddYears(1),
                Price = 500,
                CrossfitGymUsage = true,
                GroupTrainingUsage = true,
                PoolUsage = true,
                SaunaUsage = true,
                MassageUsage = false,
            };

            // Create EntriesExits
            var entryExit1 = new EntriesExits
            {
                Id = 1,
                EntryTime = DateTime.Now.AddHours(-1),
                ExitTime = DateTime.Now,
                PersonId = 1,
            };

            var entryExit2 = new EntriesExits
            {
                Id = 2,
                EntryTime = DateTime.Now.AddHours(-2),
                ExitTime = DateTime.Now.AddHours(-1),
                PersonId = 2,
            };

            modelBuilder.Entity<EntriesExits>().HasData(entryExit1, entryExit2);
            modelBuilder.Entity<Person>().HasData(person1, person2);
            modelBuilder.Entity<Pass>().HasData(pass1, pass2);

            base.OnModelCreating(modelBuilder);
        }

    }
}
