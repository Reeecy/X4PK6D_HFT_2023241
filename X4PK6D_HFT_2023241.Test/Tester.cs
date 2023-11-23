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
        PersonLogic logic;
        Mock<IRepository<Person>> mockPersonRepo;

        [SetUp]
        public void Init()
        {
            var inputData = new List<Person>()
            {
                new Person() {Id = 1, FirstName = "Test", LastName = "Test", DateOfBirth = DateTime.Now, Address = "Test", PhoneNumber = "Test", Email = "Test", IsStudent = true, IsRetired = false, PassId = 1, Pass = new Pass(){ }, EntriesExits = new List<EntriesExits>() },
                new Person() {Id = 2, FirstName = "Test2", LastName = "Test2", DateOfBirth = DateTime.Now, Address = "Test2", PhoneNumber = "Test2", Email = "Test", IsStudent = false, IsRetired = true, PassId = 2, Pass = new Pass(){ }, EntriesExits = new List<EntriesExits>() },
                new Person() {Id = 3, FirstName = "Test3", LastName = "Test3", DateOfBirth = DateTime.Now, Address = "Test3", PhoneNumber = "Test3", Email = "Test", IsStudent = true, IsRetired = false, PassId = 2, Pass = new Pass() { }, EntriesExits = new List < EntriesExits >()},
                new Person() {Id = 4, FirstName = "Test4", LastName = "Test4", DateOfBirth = DateTime.Now, Address = "Test4", PhoneNumber = "Test4", Email = "Test", IsStudent = false, IsRetired = true, PassId = 2, Pass = new Pass() { }, EntriesExits = new List < EntriesExits >()},
                new Person() {Id = 5, FirstName = "Test5", LastName = "Test5", DateOfBirth = DateTime.Now, Address = "Test5", PhoneNumber = "Test5", Email = "Test", IsStudent = false, IsRetired = false, PassId = 1, Pass = new Pass() { }, EntriesExits = new List < EntriesExits >()},
            }.AsQueryable();

            mockPersonRepo = new Mock<IRepository<Person>>();
            mockPersonRepo.Setup(x => x.ReadAll()).Returns(inputData);
            logic = new PersonLogic(mockPersonRepo.Object);
        }

        [Test]
        public void PersonCreateTest()
        {
            var person = new Person() {Id = 6, FirstName = "Test6", LastName = "Test6", DateOfBirth = DateTime.Now, Address = "Test6", PhoneNumber = "Test6", Email = "Test", IsStudent = false, IsRetired = false, PassId = 1, Pass = new Pass() { }, EntriesExits = new List<EntriesExits>() };

            // Act
            logic.Create(person);

            // Assert
            mockPersonRepo.Verify(x => x.Create(person), Times.Once);
        }

        [Test]
        public void PersonCreateTestWithIncorrectData()
        {
            var person = new Person() { Id = 6, FirstName = "Test6", LastName = "Test6", DateOfBirth = DateTime.Now, Address = "Test6", PhoneNumber = "Test6", Email = "Test", IsStudent = false, IsRetired = false, PassId = 1 };
            try
            {
                // Act
                logic.Create(person);
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
            var result = logic.ReadAll();

            // Assert
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void PersonsWithEntriesExitsTest()
        {
            // Act
            var result = logic.PersonsWithEntriesExits();

            // Assert
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void PersonsWithExpiredPassesTest()
        {
            // Act
            var result = logic.PersonsWithExpiredPasses();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void StudentsWithActivePassesTest()
        {
            // Act
            var result = logic.StudentsWithActivePasses();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void PersonsWithMonthlyPassesAndTotalUsageDurationTest()
        {
            // Act
            var result = logic.PersonsWithMonthlyPassesAndTotalUsageDuration();

            // Assert
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void PersonsWithoutPassesTest()
        {
            // Act
            var result = logic.PersonsWithoutPasses();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

    }
}
