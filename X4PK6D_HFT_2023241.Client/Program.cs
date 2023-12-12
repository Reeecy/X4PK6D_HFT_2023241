using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Client
{
    public class Program
    {
        static RestService rest;

        private static void Update(string v)
        {
            if (v == "Person")
            {
                Console.Write("Enter Person's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Person person = rest.Get<Person>(id, "person");
                Console.Write($"New Full name [old: {person.FirstName} {person.LastName}] ");
                string name = Console.ReadLine();
                person.FirstName = name.Split(" ")[0];
                person.LastName = name.Split(" ")[1];
                Console.Write($"New date of birth [old: {person.DateOfBirth}] ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                Console.Write($"New Phone Number [old: {person.PhoneNumber}] ");
                string phoneNumber = Console.ReadLine();
                Console.Write($"New Is Student [old: {person.IsStudent}] ");
                bool isStudent = bool.Parse(Console.ReadLine());
                Console.Write($"New Is Retired [old: {person.IsRetired}] ");
                bool isRetired = bool.Parse(Console.ReadLine());
                Console.Write($"New Address [old: {person.Address}] ");
                string address = Console.ReadLine();
                Console.Write($"New Email [old: {person.Email}] ");
                string email = Console.ReadLine();
                Console.Write($"New Pass Id [old: {person.PassId}] ");
                int passId = int.Parse(Console.ReadLine());
                rest.Put(new Person { Id = id, FirstName = name.Split(" ")[0], LastName = name.Split(" ")[1], DateOfBirth = dateOfBirth, PhoneNumber = phoneNumber, IsStudent = isStudent, IsRetired = isRetired, Address = address, Email = email, PassId = passId }, "person");
            }

            if (v == "Pass")
            {
                Console.Write("Enter Pass's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Pass pass = rest.Get<Pass>(id, "pass");
                Console.Write($"New Pass type [old: {pass.PassType}] ");
                string passType = Console.ReadLine();
                Console.Write($"New Pass start date [old: {pass.StartDate}] ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write($"New Pass end date [old: {pass.EndDate}] ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());
                Console.Write($"New Pass price [old: {pass.Price}] ");
                int price = int.Parse(Console.ReadLine());
                Console.Write($"New Pass Crossfit Gym Usage [old: {pass.CrossfitGymUsage}] ");
                bool crossfitGymUsage = bool.Parse(Console.ReadLine());
                Console.Write($"New Pass Group Training Usage [old: {pass.GroupTrainingUsage}] ");
                bool groupTrainingUsage = bool.Parse(Console.ReadLine());
                Console.Write($"New Pass Pool Usage [old: {pass.PoolUsage}] ");
                bool poolUsage = bool.Parse(Console.ReadLine());
                Console.Write($"New Pass Sauna Usage [old: {pass.SaunaUsage}] ");
                bool saunaUsage = bool.Parse(Console.ReadLine());
                Console.Write($"New Pass Massage Usage [old: {pass.MassageUsage}] ");
                bool massageUsage = bool.Parse(Console.ReadLine());
                rest.Put(new Pass { Id = id, PassType = passType, StartDate = startDate, EndDate = endDate, Price = price, CrossfitGymUsage = crossfitGymUsage, GroupTrainingUsage = groupTrainingUsage, PoolUsage = poolUsage, SaunaUsage = saunaUsage, MassageUsage = massageUsage }, "pass");
            }

            if (v == "EntriesExits")
            {
                Console.Write("Enter EntriesExits's id to update: ");
                int id = int.Parse(Console.ReadLine());
                EntriesExits entriesExits = rest.Get<EntriesExits>(id, "entriesexits");
                Console.Write($"New Entry time [old: {entriesExits.EntryTime}] ");
                DateTime entryTime = DateTime.Parse(Console.ReadLine());
                Console.Write($"New Exit time [old: {entriesExits.ExitTime}] ");
                DateTime exitTime = DateTime.Parse(Console.ReadLine());
                Console.Write($"New Person id [old: {entriesExits.PersonId}] ");
                int personId = int.Parse(Console.ReadLine());
                rest.Put(new EntriesExits { Id = id, EntryTime = entryTime, ExitTime = exitTime, PersonId = personId }, "entriesexits");
            }
        }

        private static void Delete(string v)
        {
            if (v == "Person")
            {
                Console.Write("Enter Person's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "person");
            }

            if (v == "Pass")
            {
                Console.Write("Enter Pass's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "pass");
            }

            if (v == "EntriesExits")
            {
                Console.Write("Enter EntriesExits's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "entriesexits");
            }
        }

        private static void Create(string v)
        {
            if (v == "Person")
            {
                Console.Write("Enter Person name: ");
                string name = Console.ReadLine();
                var person = name.Split(" ");
                Console.Write("Enter Person's date of birth: ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Person's Phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Is the person a student? (true/false) ");
                bool isStudent = bool.Parse(Console.ReadLine());
                Console.Write("Is the person retired? (true/false) ");
                bool isRetired = bool.Parse(Console.ReadLine());
                Console.Write("Enter Person's Address: ");
                string address = Console.ReadLine();
                Console.Write("Enter Person's Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Person's Pass Id: ");
                int passId = int.Parse(Console.ReadLine());
                rest.Post(new Person { FirstName = person[0], LastName = person[1], DateOfBirth = dateOfBirth, PhoneNumber = phoneNumber, IsStudent = isStudent, IsRetired = isRetired, Address = address, Email = email, PassId = passId }, "person");
            }

            if (v == "Pass")
            {
                Console.Write("Enter Pass type: ");
                string passType = Console.ReadLine();
                Console.Write("Enter Pass start date: ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Pass end date: ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Pass price: ");
                int price = int.Parse(Console.ReadLine());
                Console.Write("Enter Pass Crossfit Gym Usage (true/false): ");
                bool crossfitGymUsage = bool.Parse(Console.ReadLine());
                Console.Write("Enter Pass Group Training Usage (true/false): ");
                bool groupTrainingUsage = bool.Parse(Console.ReadLine());
                Console.Write("Enter Pass Pool Usage (true/false): ");
                bool poolUsage = bool.Parse(Console.ReadLine());
                Console.Write("Enter Pass Sauna Usage (true/false): ");
                bool saunaUsage = bool.Parse(Console.ReadLine());
                Console.Write("Enter Pass Massage Usage (true/false): ");
                bool massageUsage = bool.Parse(Console.ReadLine());
                rest.Post(new Pass { PassType = passType, StartDate = startDate, EndDate = endDate, Price = price, CrossfitGymUsage = crossfitGymUsage, GroupTrainingUsage = groupTrainingUsage, PoolUsage = poolUsage, SaunaUsage = saunaUsage, MassageUsage = massageUsage }, "pass");
            }

            if (v == "EntriesExits")
            {
                Console.Write("Enter Entry time: ");
                DateTime entryTime = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Exit time: ");
                DateTime exitTime = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Person id: ");
                int personId = int.Parse(Console.ReadLine());
                rest.Post(new EntriesExits { EntryTime = entryTime, ExitTime = exitTime, PersonId = personId }, "entriesexits");
            }
        }

        private static void ShowThem(string v)
        {
            if (v == "Person")
            {
                List<Person> items = rest.Get<Person>("person");
                foreach (Person person in items) { Console.WriteLine(person.Id + " " + person.FirstName + " " + person.LastName); }
            }
            else if (v == "Pass")
            {
                List<Pass> items = rest.Get<Pass>("pass");
                foreach (var item in items)
                {
                    Console.WriteLine(item.Id + " " + item.PassType + " " + item.StartDate + " " + item.EndDate + " " + item.Price + " " + item.CrossfitGymUsage + " " + item.GroupTrainingUsage + " " + item.PoolUsage + " " + item.SaunaUsage + " " + item.MassageUsage);
                }
            }
            else if (v == "EntriesExits")
            {
                List<EntriesExits> items = rest.Get<EntriesExits>("entriesexits");
                foreach (var item in items)
                {
                    Console.WriteLine(item.Id + " " + item.EntryTime + " " + item.ExitTime + " " + item.PersonId);
                }
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:20677/", "person");

            var entriesExitsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ShowThem("EntriesExits"))
                .Add("Create", () => Create("EntriesExits"))
                .Add("Delete", () => Delete("EntriesExits"))
                .Add("Update", () => Update("EntriesExits"))
                .Add("Exit", ConsoleMenu.Close);

            var passSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ShowThem("Pass"))
                .Add("Create", () => Create("Pass"))
                .Add("Delete", () => Delete("Pass"))
                .Add("Update", () => Update("Pass"))
                .Add("Exit", ConsoleMenu.Close);

            var personSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => ShowThem("Person"))
                .Add("Create", () => Create("Person"))
                .Add("Delete", () => Delete("Person"))
                .Add("Update", () => Update("Person"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCruds = new ConsoleMenu(args, level: 1)
                .Add("Show how many persons are in the database", () => ShowPersonCount())
                .Add("Show persons with their entries and exits", ()=>ShowPersonsWithEntriesExits())
                .Add("Show persons with expired passes", ()=> ShowPersonsWithExpiredPasses())
                .Add("Show students with active passes", ()=> ShowStudentsWithActivePasses())
                .Add("Show persons with monthly passes and total usage duration", ()=> ShowPWithMonthlyPsAndTUD())
                .Add("Show persons without passes", ()=> ShowPersonsWithoutPasses())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Persons", () => personSubMenu.Show())
                .Add("Passes", () => passSubMenu.Show())
                .Add("Exits and Entries", () => entriesExitsSubMenu.Show())
                .Add("Others", () => nonCruds.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        private static void ShowPersonCount()
        {
            var personCount = rest.GetSingle<int>("/api/Stat/PersonCount");
            Console.WriteLine($"{personCount} persons are in the database.");
            Console.ReadLine();
        }

        private static void ShowPersonsWithoutPasses()
        {
            var persons = rest.GetSingle<List<FullNameClass>>("/api/Stat/PersonsWithoutPasses");
            foreach (var person in persons)
            {
                Console.WriteLine(person.FullName);
            }
            Console.ReadLine();
        }

        private static void ShowPWithMonthlyPsAndTUD()
        {
            var persons = rest.GetSingle<List<PWithMonthlyPsAndTUD>>("/api/Stat/PersonsWithMonthlyPassesAndTotalUsage");
            foreach (var person in persons)
            {
                Console.WriteLine(person.FullName + "\tStart: " + person.StartDate + "\tEnd: " + person.EndDate + "\tTotal: " + person.TotalUsageDuration);
            }
            Console.ReadLine();
        }

        private static void ShowStudentsWithActivePasses()
        {
            var students = rest.GetSingle<List<ActiveStudents>>("/api/Stat/StudentsWithActivePasses");
            foreach (var student in students)
            {
                Console.WriteLine(student.FullName + "\tPass type: " + student.PassType + "\nEnd date: " + student.EndDate);
                Console.WriteLine("Entries and exits:");

                for (int i = 0; i < student.Entries.Count; i++)
                {
                    Console.WriteLine("Entry: " + student.Entries[i] + "\tExit: " + student.Exits[i]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        private static void ShowPersonsWithExpiredPasses()
        {
            var persons = rest.GetSingle<List<ExpiredPasses>>("/api/Stat/PersonsWithExpiredPasses");
            foreach (var person in persons)
            {
                Console.WriteLine(person.FullName + "\tPass type: " + person.PassType + "\tEnd date: " + person.EndDate);
            }
            Console.ReadLine();
        }

        private static void ShowPersonsWithEntriesExits()
        {
            var persons = rest.GetSingle<List<AllEntriesAndExits>>("/api/Stat/PersonsWithEntriesAndExits");
            foreach (var person in persons)
            {
                Console.WriteLine(person.FullName);
                Console.WriteLine("Entries and exits:");

                for (int i = 0; i < person.Entries.Count; i++)
                {
                    Console.WriteLine("Entry: " + person.Entries[i] + "\tExit: " + person.Exits[i]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
