using ConsoleTools;
using Microsoft.EntityFrameworkCore.Migrations;
using NUnit.Framework;
using System;
using System.Linq;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Client
{
    public class Program
    {
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
