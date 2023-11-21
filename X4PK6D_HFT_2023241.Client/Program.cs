using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;
using X4PK6D_HFT_2023241.Logic;
using X4PK6D_HFT_2023241.Models;
using X4PK6D_HFT_2023241.Repository;

namespace X4PK6D_HFT_2023241.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Repository and Logic references are added to the Client project
            var ctx = new FaMDbContext();
            var personRepo = new PersonRepository(ctx);
            var passRepo = new PassRepository(ctx);
            var entriesExitsRepo = new EntriesExitsRepository(ctx);

            var personLogic = new PersonLogic(personRepo);
            var passLogic = new PassLogic(passRepo);
            var entriesExitsLogic = new EntriesExitsLogic(entriesExitsRepo);


        }
    }
}
