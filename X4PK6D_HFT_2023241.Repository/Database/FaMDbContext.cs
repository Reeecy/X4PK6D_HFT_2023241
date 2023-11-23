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
            // to use cstring: string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\fandm.mdf"";Integrated Security=True;MultipleActiveResultSets=True";
            // to see tables: string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Visual Studio Projects\X4PK6D_HFT_2023241\X4PK6D_HFT_2023241.Repository\fandm.mdf"";Integrated Security=True;MultipleActiveResultSets=True";
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


            List<Person> personList = new List<Person>
            {
            new Person { Id = 1,FirstName = "John",LastName = "Doe",DateOfBirth = new DateTime(1980, 1, 1),Address = "123 Main St, Cityville",PhoneNumber = "555-1234",Email = "john.doe@example.com",IsStudent = false,IsRetired = false,PassId = 1},
            new Person { Id = 2,FirstName = "Jane",LastName = "Smith",DateOfBirth = new DateTime(1990, 5, 15),Address = "456 Oak St, Townsville",PhoneNumber = "555-5678",Email = "jane.smith@example.com",IsStudent = true,IsRetired = false,PassId = 2},
            new Person { Id = 3,FirstName = "Bob",LastName = "Jones",DateOfBirth = new DateTime(1950, 12, 25),Address = "789 Pine St, Villageton",PhoneNumber = "555-9012",Email = "bob.jones@example.com",IsStudent = false, IsRetired=true,PassId=2 },
            new Person { Id = 4, FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1990, 5, 15), Address = "456 Oak St, Townsville", PhoneNumber = "555-7890", Email = "alice.smith@example.com", IsStudent = true, IsRetired = false, PassId = 3 },
            new Person { Id = 5, FirstName = "Charlie", LastName = "Brown", DateOfBirth = new DateTime(1985, 8, 10), Address = "101 Elm St, Groveton", PhoneNumber = "555-1111", Email = "charlie.brown@example.com", IsStudent = false, IsRetired = false, PassId = 1 },
            new Person { Id = 6, FirstName = "Emma", LastName = "Davis", DateOfBirth = new DateTime(1993, 3, 20), Address = "202 Maple St, Citytown", PhoneNumber = "555-2222", Email = "emma.davis@example.com", IsStudent = true, IsRetired = false, PassId = 2 },
            new Person { Id = 7, FirstName = "Frank", LastName = "Miller", DateOfBirth = new DateTime(1978, 11, 5), Address = "303 Birch St, Villagetown", PhoneNumber = "555-3333", Email = "frank.miller@example.com", IsStudent = false, IsRetired = true, PassId = 3 },
            new Person { Id = 8, FirstName = "Grace", LastName = "Turner", DateOfBirth = new DateTime(1982, 6, 25), Address = "404 Pine St, Suburbia", PhoneNumber = "555-4444", Email = "grace.turner@example.com", IsStudent = false, IsRetired = false, PassId = 4 },
            new Person { Id = 9, FirstName = "Henry", LastName = "Wilson", DateOfBirth = new DateTime(1990, 9, 15), Address = "505 Cedar St, Townsville", PhoneNumber = "555-5555", Email = "henry.wilson@example.com", IsStudent = true, IsRetired = false, PassId = 5 },
            new Person { Id = 10, FirstName = "Ivy", LastName = "Adams", DateOfBirth = new DateTime(1987, 2, 8), Address = "606 Oak St, Countryside", PhoneNumber = "555-6666", Email = "ivy.adams@example.com", IsStudent = false, IsRetired = false, PassId = 6 },
            new Person { Id = 11, FirstName = "Jack", LastName = "Taylor", DateOfBirth = new DateTime(1975, 7, 12), Address = "707 Elm St, Cityville", PhoneNumber = "555-7777", Email = "jack.taylor@example.com", IsStudent = false, IsRetired = false, PassId = 7 },
            new Person { Id = 12, FirstName = "Katherine", LastName = "Brown", DateOfBirth = new DateTime(1984, 4, 30), Address = "808 Birch St, Suburbville", PhoneNumber = "555-8888", Email = "katherine.brown@example.com", IsStudent = true, IsRetired = false, PassId = 8 },
            new Person { Id = 13, FirstName = "Leo", LastName = "Martin", DateOfBirth = new DateTime(1973, 10, 18), Address = "909 Cedar St, Villagetown", PhoneNumber = "555-9999", Email = "leo.martin@example.com", IsStudent = false, IsRetired = true, PassId = 9 },
            new Person { Id = 14, FirstName = "Mia", LastName = "Garcia", DateOfBirth = new DateTime(1992, 1, 5), Address = "1010 Pine St, Suburbia", PhoneNumber = "555-0000", Email = "mia.garcia@example.com", IsStudent = false, IsRetired = false, PassId = 10 },
            new Person { Id = 15, FirstName = "Eva", LastName = "Smith", DateOfBirth = new DateTime(1980, 1, 1), Address = "123 Main St, Cityville", PhoneNumber = "555-1234", Email = "eva.smith@example.com", IsStudent = false, IsRetired = false, PassId = 1 },
            new Person { Id = 16, FirstName = "Michael", LastName = "Brown", DateOfBirth = new DateTime(1995, 5, 10), Address = "456 Oak St, Townsville", PhoneNumber = "555-5678", Email = "michael.brown@example.com", IsStudent = true, IsRetired = false, PassId = 2 },
            new Person { Id = 17, FirstName = "Sophia", LastName = "Johnson", DateOfBirth = new DateTime(1972, 8, 15), Address = "789 Pine St, Villageton", PhoneNumber = "555-9012", Email = "sophia.johnson@example.com", IsStudent = false, IsRetired = true, PassId = 3 },
            new Person { Id = 18, FirstName = "Oliver", LastName = "Davis", DateOfBirth = new DateTime(1988, 3, 20), Address = "101 Elm St, Groveton", PhoneNumber = "555-3456", Email = "oliver.davis@example.com", IsStudent = true, IsRetired = false, PassId = 4 },
            new Person { Id = 19, FirstName = "Ava", LastName = "Miller", DateOfBirth = new DateTime(1965, 11, 5), Address = "202 Maple St, Citytown", PhoneNumber = "555-7890", Email = "ava.miller@example.com", IsStudent = false, IsRetired = false, PassId = 5 },
            new Person { Id = 20, FirstName = "Liam", LastName = "Wilson", DateOfBirth = new DateTime(1980, 6, 25), Address = "303 Birch St, Villagetown", PhoneNumber = "555-1234", Email = "liam.wilson@example.com", IsStudent = true, IsRetired = false, PassId = 6 },
            new Person { Id = 21, FirstName = "Isabella", LastName = "Turner", DateOfBirth = new DateTime(1990, 9, 15), Address = "404 Pine St, Suburbia", PhoneNumber = "555-5678", Email = "isabella.turner@example.com", IsStudent = false, IsRetired = true, PassId = 7 },
            new Person { Id = 22, FirstName = "Mason", LastName = "Smith", DateOfBirth = new DateTime(1985, 4, 30), Address = "505 Cedar St, Townsville", PhoneNumber = "555-9012", Email = "mason.smith@example.com", IsStudent = false, IsRetired = false, PassId = 8 },
            new Person { Id = 23, FirstName = "Ella", LastName = "Wilson", DateOfBirth = new DateTime(1993, 9, 1), Address = "606 Oak St, Countryside", PhoneNumber = "555-3456", Email = "ella.wilson@example.com", IsStudent = true, IsRetired = false, PassId = 9 },
            new Person { Id = 24, FirstName = "James", LastName = "Adams", DateOfBirth = new DateTime(1978, 2, 8), Address = "707 Elm St, Cityville", PhoneNumber = "555-7890", Email = "james.adams@example.com", IsStudent = false, IsRetired = false, PassId = 10 },
            new Person { Id = 25, FirstName = "Sophie", LastName = "Williams", DateOfBirth = new DateTime(1983, 4, 12), Address = "808 Pine St, Countryside", PhoneNumber = "555-2468", Email = "sophie.williams@example.com", IsStudent = true, IsRetired = false, PassId = 1 },
            new Person { Id = 26, FirstName = "Ethan", LastName = "Anderson", DateOfBirth = new DateTime(1992, 8, 22), Address = "909 Birch St, Suburbia", PhoneNumber = "555-1357", Email = "ethan.anderson@example.com", IsStudent = false, IsRetired = false, PassId = 2 },
            new Person { Id = 27, FirstName = "Scarlett", LastName = "Thomas", DateOfBirth = new DateTime(1976, 2, 5), Address = "1010 Elm St, Cityville", PhoneNumber = "555-9876", Email = "scarlett.thomas@example.com", IsStudent = false, IsRetired = true, PassId = 3 },
            new Person { Id = 28, FirstName = "Noah", LastName = "Harris", DateOfBirth = new DateTime(1989, 12, 18), Address = "111 Pine St, Groveton", PhoneNumber = "555-6543", Email = "noah.harris@example.com", IsStudent = true, IsRetired = false, PassId = 4 },
            new Person { Id = 29, FirstName = "Aria", LastName = "Moore", DateOfBirth = new DateTime(1970, 6, 7), Address = "121 Oak St, Villagetown", PhoneNumber = "555-7890", Email = "aria.moore@example.com", IsStudent = false, IsRetired = false, PassId = 5 },
            new Person { Id = 30, FirstName = "William", LastName = "Clark", DateOfBirth = new DateTime(1984, 11, 15), Address = "131 Birch St, Townsville", PhoneNumber = "555-1234", Email = "william.clark@example.com", IsStudent = true, IsRetired = false, PassId = 6 },
            new Person { Id = 31, FirstName = "Zoe", LastName = "Wright", DateOfBirth = new DateTime(1998, 3, 30), Address = "141 Cedar St, Countryside", PhoneNumber = "555-5678", Email = "zoe.wright@example.com", IsStudent = false, IsRetired = true, PassId = 7 },
            new Person { Id = 32, FirstName = "Owen", LastName = "Roberts", DateOfBirth = new DateTime(1972, 7, 9), Address = "151 Maple St, Suburbville", PhoneNumber = "555-9012", Email = "owen.roberts@example.com", IsStudent = false, IsRetired = false, PassId = 8 },
            new Person { Id = 33, FirstName = "Grace", LastName = "Perry", DateOfBirth = new DateTime(1995, 1, 25), Address = "161 Oak St, Villagetown", PhoneNumber = "555-3456", Email = "grace.perry@example.com", IsStudent = true, IsRetired = false, PassId = 9 },
            new Person { Id = 34, FirstName = "Elijah", LastName = "Fisher", DateOfBirth = new DateTime(1979, 9, 3), Address = "171 Cedar St, Citytown", PhoneNumber = "555-6789", Email = "elijah.fisher@example.com", IsStudent = false, IsRetired = false, PassId = 10 },
            new Person { Id = 35, FirstName = "Eva", LastName = "Miller", DateOfBirth = new DateTime(2003, 7, 10), Address = "789 Elm St, Suburbia", PhoneNumber = "555-9876", Email = "eva.miller@example.com", IsStudent = true, IsRetired = false, PassId = 0 },
            new Person { Id = 36, FirstName = "Charlie", LastName = "Davis", DateOfBirth = new DateTime(2007, 11, 5), Address = "123 Maple St, Cityville", PhoneNumber = "555-3456", Email = "charlie.davis@example.com", IsStudent = true, IsRetired = false, PassId = 0 }
            };

            List<Pass> passesList = new List<Pass>
            {
                new Pass { Id = 1, PassType = "Monthly", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Price = 50, CrossfitGymUsage = true, GroupTrainingUsage = true, PoolUsage = true, SaunaUsage = true, MassageUsage = false },
                new Pass { Id = 2, PassType = "Yearly", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), Price = 500, CrossfitGymUsage = true, GroupTrainingUsage = true, PoolUsage = true, SaunaUsage = true, MassageUsage = true },
                new Pass { Id = 4,PassType = "Daily",StartDate = DateTime.Now.AddDays(-2),EndDate = DateTime.Now.AddDays(1),Price = 30,CrossfitGymUsage = true,GroupTrainingUsage = true,PoolUsage = false,SaunaUsage = false,MassageUsage = true},
                new Pass { Id = 5,PassType = "Weekly",StartDate = DateTime.Now.AddDays(-14),EndDate = DateTime.Now.AddDays(-7),Price = 100,CrossfitGymUsage = true,GroupTrainingUsage = true,PoolUsage = false,SaunaUsage = true,MassageUsage = false},
                new Pass { Id = 6,PassType = "10-Day Pool",StartDate = DateTime.Now.AddDays(-3),EndDate = DateTime.Now.AddDays(10),Price = 75,CrossfitGymUsage = false,GroupTrainingUsage = false,PoolUsage = true,SaunaUsage = false,MassageUsage = false},
                new Pass { Id = 8, PassType = "One-Day Pool", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Price = 15, CrossfitGymUsage = false, GroupTrainingUsage = false, PoolUsage = true, SaunaUsage = false, MassageUsage = false },
                new Pass { Id = 9, PassType = "One-Day Sauna", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Price = 20, CrossfitGymUsage = false, GroupTrainingUsage = false, PoolUsage = false, SaunaUsage = true, MassageUsage = false },
                new Pass { Id = 10, PassType = "One-Day Massage", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Price = 25, CrossfitGymUsage = false, GroupTrainingUsage = false, PoolUsage = false, SaunaUsage = false, MassageUsage = true },
            };

            List<EntriesExits> entriesExitsList = new List<EntriesExits>();
            Random random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                DateTime entryTime = DateTime.Now.AddHours(-random.Next(1, 8));
                DateTime exitTime = entryTime.AddHours(random.Next(1, 8));

                EntriesExits entryExit = new EntriesExits
                {
                    Id = i,
                    EntryTime = entryTime,
                    ExitTime = exitTime,
                    PersonId = random.Next(1, 35)
                };

                entriesExitsList.Add(entryExit);
            }

            modelBuilder.Entity<EntriesExits>().HasData(entriesExitsList);
            modelBuilder.Entity<Person>().HasData(personList);
            modelBuilder.Entity<Pass>().HasData(passesList);

            base.OnModelCreating(modelBuilder);
        }

    }
}
