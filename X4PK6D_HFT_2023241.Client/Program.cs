using ConsoleTools;
using Microsoft.EntityFrameworkCore.Migrations;
using NUnit.Framework;
using System;
using System.Linq;
using X4PK6D_HFT_2023241.Logic;
using X4PK6D_HFT_2023241.Models;
using X4PK6D_HFT_2023241.Repository;

namespace X4PK6D_HFT_2023241.Client
{
    public class Program
    {
        static PersonLogic personLogic;
        static PassLogic passLogic;
        static EntriesExitsLogic entriesExitsLogic;

        private static void Update(string v)
        {
            Console.WriteLine(v + " update");
            Console.ReadLine();
        }

        private static void Delete(string v)
        {
            Console.WriteLine(v + " delete");
            Console.ReadLine();
        }

        private static void Create(string v)
        {
            Console.WriteLine(v + " create");
            Console.ReadLine();
        }

        private static void ShowThem(string v)
        {
            if (v == "Person")
            {
                var items = personLogic.ReadAll();
                foreach (var item in items)
                {
                    Console.WriteLine(item.Id + " " + item.FirstName + " " + item.LastName + " " + item.DateOfBirth + " " + item.Address + " " + item.PhoneNumber + " " + item.Email + " " + item.IsStudent + " " + item.IsRetired + " " + item.PassId);
                }
            }
            else if (v == "Pass")
            {
                var items = passLogic.ReadAll();
                foreach (var item in items)
                {
                    Console.WriteLine(item.Id + " " + item.PassType + " " + item.StartDate + " " + item.EndDate + " " + item.Price + " " + item.CrossfitGymUsage + " " + item.GroupTrainingUsage + " " + item.PoolUsage + " " + item.SaunaUsage + " " + item.MassageUsage);
                }
            }
            else if (v == "EntriesExits")
            {
                var items = entriesExitsLogic.ReadAll();
                foreach (var item in items)
                {
                    Console.WriteLine(item.Id + " " + item.EntryTime + " " + item.ExitTime + " " + item.PersonId);
                }
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            // Repository and Logic references are added to the Client project
            var ctx = new FaMDbContext();
            var personRepo = new PersonRepository(ctx);
            var passRepo = new PassRepository(ctx);
            var entriesExitsRepo = new EntriesExitsRepository(ctx);

            personLogic = new PersonLogic(personRepo);
            passLogic = new PassLogic(passRepo);
            entriesExitsLogic = new EntriesExitsLogic(entriesExitsRepo);

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

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Persons", () => personSubMenu.Show())
                .Add("Passes", () => passSubMenu.Show())
                .Add("Exits and Entries", () => entriesExitsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
