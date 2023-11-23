using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using X4PK6D_HFT_2023241.Logic;
using X4PK6D_HFT_2023241.Models;
using X4PK6D_HFT_2023241.Repository;

namespace X4PK6D_HFT_2023241.Test
{
    [TestFixture]
    public class Tester
    {
        PersonLogic personLogic;
        PassLogic passLogic;
        EntriesExitsLogic entriesExitsLogic;

        Mock<IRepository<Person>> mockPersonRepo;
        Mock<IRepository<Pass>> mockPassRepo;
        Mock<IRepository<EntriesExits>> mockEntriesExitsRepo;

        IQueryable<Pass> passesList;
        IQueryable<EntriesExits> entriesExitsList;
        IQueryable<Person> personsList;

        [SetUp]
        public void Init()
        {
            #region Create Lists
            passesList = new List<Pass>
            {
                new Pass { Id = 1, PassType = "Monthly", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Price = 50, CrossfitGymUsage = true, GroupTrainingUsage = true, PoolUsage = true, SaunaUsage = true, MassageUsage = false },
                new Pass { Id = 2, PassType = "Yearly", StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), Price = 500, CrossfitGymUsage = true, GroupTrainingUsage = true, PoolUsage = true, SaunaUsage = true, MassageUsage = true },
                new Pass { Id = 3,PassType = "Daily",StartDate = DateTime.Now.AddDays(-2),EndDate = DateTime.Now.AddDays(1),Price = 30,CrossfitGymUsage = true,GroupTrainingUsage = true,PoolUsage = false,SaunaUsage = false,MassageUsage = true},
                new Pass { Id = 4,PassType = "Weekly",StartDate = DateTime.Now.AddDays(-14),EndDate = DateTime.Now.AddDays(-7),Price = 100,CrossfitGymUsage = true,GroupTrainingUsage = true,PoolUsage = false,SaunaUsage = true,MassageUsage = false},
                new Pass { Id = 5,PassType = "10-Day Pool",StartDate = DateTime.Now.AddDays(-3),EndDate = DateTime.Now.AddDays(10),Price = 75,CrossfitGymUsage = false,GroupTrainingUsage = false,PoolUsage = true,SaunaUsage = false,MassageUsage = false},
                new Pass { Id = 6, PassType = "One-Day Pool", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Price = 15, CrossfitGymUsage = false, GroupTrainingUsage = false, PoolUsage = true, SaunaUsage = false, MassageUsage = false },
                new Pass { Id = 7, PassType = "One-Day Sauna", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Price = 20, CrossfitGymUsage = false, GroupTrainingUsage = false, PoolUsage = false, SaunaUsage = true, MassageUsage = false },
                new Pass { Id = 8, PassType = "One-Day Massage", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Price = 25, CrossfitGymUsage = false, GroupTrainingUsage = false, PoolUsage = false, SaunaUsage = false, MassageUsage = true },
            }.AsQueryable();

            var entriesExits = new List<EntriesExits>();
            Random random = new Random();
            for (int i = 1; i <= 20; i++)
            {
                DateTime entryTime = DateTime.Now.AddDays(-random.Next(1, 10)).AddHours(-random.Next(1, 8));
                DateTime exitTime = entryTime.AddHours(random.Next(1, 8));

                EntriesExits entryExit = new EntriesExits
                {
                    Id = i,
                    EntryTime = entryTime,
                    ExitTime = exitTime,
                    PersonId = random.Next(1, 36)
                };

                entriesExits.Add(entryExit);
            }
            entriesExitsList = entriesExits.AsQueryable();


            personsList = new List<Person>()
            {
                new Person {Id = 1, FirstName = "Test1", LastName = "Test1", DateOfBirth = new DateTime(2003,01,26), Address = "Test", PhoneNumber = "Test1", Email = "test1@gmail.com", IsStudent = true, IsRetired = false, PassId = 1, Pass = passesList.ToList()[0], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 2, FirstName = "Test2", LastName = "Test2", DateOfBirth = new DateTime(1995, 07, 12), Address = "456 Oak St", PhoneNumber = "555-5678", Email = "test2@gmail.com", IsStudent = true, IsRetired = false, PassId = 2, Pass = passesList.ToList()[1], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 3, FirstName = "Test3", LastName = "Test3", DateOfBirth = new DateTime(1980, 04, 18), Address = "789 Pine St", PhoneNumber = "555-9876", Email = "test3@gmail.com", IsStudent = false, IsRetired = true, PassId = 3, Pass = passesList.ToList()[2], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 4, FirstName = "Test4", LastName = "Test4", DateOfBirth = new DateTime(1992, 10, 8), Address = "123 Elm St", PhoneNumber = "555-4321", Email = "test4@gmail.com", IsStudent = true, IsRetired = false, PassId = 1, Pass = passesList.ToList()[0], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 5, FirstName = "Test5", LastName = "Test5", DateOfBirth = new DateTime(1985, 6, 20), Address = "789 Birch St", PhoneNumber = "555-8765", Email = "test5@gmail.com", IsStudent = false, IsRetired = true, PassId = 2, Pass = passesList.ToList()[1], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 6, FirstName = "6", LastName = "Test6", DateOfBirth = new DateTime(2000, 3, 15), Address = "456 Maple St", PhoneNumber = "555-9876", Email = "test6@gmail.com", IsStudent = true, IsRetired = false, PassId = 0, Pass = passesList.ToList()[0], EntriesExits = null },
                new Person { Id = 7, FirstName = "Test7", LastName = "Test7", DateOfBirth = new DateTime(1998, 8, 30), Address = "789 Oak St", PhoneNumber = "555-5432", Email = "test7@gmail.com", IsStudent = false, IsRetired = true, PassId = 4, Pass = passesList.ToList()[3], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 8, FirstName = "Test8", LastName = "Test8", DateOfBirth = new DateTime(1982, 12, 5), Address = "123 Pine St", PhoneNumber = "555-6789", Email = "test8@gmail.com", IsStudent = true, IsRetired = false, PassId = 5, Pass = passesList.ToList()[4], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 9, FirstName = "Test9", LastName = "Test9", DateOfBirth = new DateTime(1995, 2, 25), Address = "456 Cedar St", PhoneNumber = "555-9876", Email = "test9@gmail.com", IsStudent = false, IsRetired = true, PassId = 6, Pass = passesList.ToList()[5], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 10, FirstName = "Test10", LastName = "Test10", DateOfBirth = new DateTime(1987, 11, 10), Address = "789 Walnut St", PhoneNumber = "555-1234", Email = "test10@gmail.com", IsStudent = true, IsRetired = false, PassId = 7, Pass = passesList.ToList()[6], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 11, FirstName = "Test11", LastName = "Test11", DateOfBirth = new DateTime(1993, 4, 3), Address = "123 Spruce St", PhoneNumber = "555-5678", Email = "test11@gmail.com", IsStudent = false, IsRetired = true, PassId = 8, Pass = passesList.ToList()[7], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 12, FirstName = "Test12", LastName = "Test12", DateOfBirth = new DateTime(2001, 9, 15), Address = "456 Fir St", PhoneNumber = "555-8765", Email = "test12@gmail.com", IsStudent = true, IsRetired = false, PassId = 2, Pass = passesList.ToList()[1], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() },
                new Person { Id = 13, FirstName = "Test13", LastName = "Test13", DateOfBirth = new DateTime(1996, 7, 20), Address = "789 Sycamore St", PhoneNumber = "555-4321", Email = "test13@gmail.com", IsStudent = false, IsRetired = true, PassId = 1, Pass = passesList.ToList()[0], EntriesExits = entriesExitsList.OrderBy(x => Guid.NewGuid()).Take(5).ToList() }
            }.AsQueryable();
            #endregion

            mockPersonRepo = new Mock<IRepository<Person>>();
            mockPersonRepo.Setup(x => x.ReadAll()).Returns(personsList);
            personLogic = new PersonLogic(mockPersonRepo.Object);

            mockPassRepo = new Mock<IRepository<Pass>>();
            mockPassRepo.Setup(x => x.ReadAll()).Returns(passesList);
            passLogic = new PassLogic(mockPassRepo.Object);

            mockEntriesExitsRepo = new Mock<IRepository<EntriesExits>>();
            mockEntriesExitsRepo.Setup(x => x.ReadAll()).Returns(entriesExitsList);
            entriesExitsLogic = new EntriesExitsLogic(mockEntriesExitsRepo.Object);

        }

        [Test]
        public void PersonCreateTest()
        {
            var person = personsList.ToList()[0];

            // Act
            personLogic.Create(person);

            // Assert
            mockPersonRepo.Verify(x => x.Create(person), Times.Once);
        }

        [Test]
        public void PassCreateTest()
        {
            var pass = passesList.ToList()[0];

            // Act
            passLogic.Create(pass);

            // Assert
            mockPassRepo.Verify(x => x.Create(pass), Times.Once);
        }

        [Test]
        public void EntriesExitsCreateTest()
        {
            var entriesExits = entriesExitsList.ToList()[0];

            // Act
            entriesExitsLogic.Create(entriesExits);

            // Assert
            mockEntriesExitsRepo.Verify(x => x.Create(entriesExits), Times.Once);
        }


        [Test]
        public void PersonCreateTestWithIncorrectData()
        {
            var person = personsList.ToList()[5];
            try
            {
                // Act
                personLogic.Create(person);
            }
            catch
            {

            }

            // Assert
            mockPersonRepo.Verify(x => x.Create(person), Times.Never);
        }

        [Test]
        public void PersonReadAllTest()
        {
            // Act
            var result = personLogic.ReadAll();

            // Assert

            Assert.AreEqual(personsList.Count(), result.Count());
        }

        [Test]
        public void PersonsWithEntriesExitsTest()
        {
            // Act
            var result = personLogic.PersonsWithEntriesExits();

            // Assert
            Assert.AreEqual(personsList.Where(x => x.EntriesExits != null).Count(), result.Count());
        }

        [Test]
        public void PersonsWithExpiredPassesTest()
        {
            // Act
            var result = personLogic.PersonsWithExpiredPasses();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void StudentsWithActivePassesTest()
        {
            // Act
            var result = personLogic.StudentsWithActivePasses();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void PersonsWithMonthlyPassesAndTotalUsageDurationTest()
        {
            // Act
            var result = personLogic.PersonsWithMonthlyPassesAndTotalUsageDuration();

            // Assert
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void PersonsWithoutPassesTest()
        {
            // Act
            var result = personLogic.PersonsWithoutPasses();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

    }
}
