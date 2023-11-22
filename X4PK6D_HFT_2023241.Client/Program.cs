using ConsoleTools;
using Microsoft.EntityFrameworkCore.Migrations;
using NUnit.Framework;
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
                Console.Write($"New name [old: {person.FirstName} {person.LastName}] ");
                string name = Console.ReadLine();
                person.FirstName = name.Split(" ")[0];
                person.LastName = name.Split(" ")[1];
                rest.Put(person, "person");
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
        }

        private static void Create(string v)
        {
            if (v == "Person")
            {
                Console.Write("Enter Person name: ");
                string name = Console.ReadLine();
                var person = name.Split(" ");
                rest.Post(new Person { FirstName = person[0], LastName = person[1] }, "person");
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

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Persons", () => personSubMenu.Show())
                .Add("Passes", () => passSubMenu.Show())
                .Add("Exits and Entries", () => entriesExitsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
