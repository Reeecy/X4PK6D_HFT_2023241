using System;
using System.Linq;

namespace X4PK6D_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FaMDbContext context = new FaMDbContext();
            var items = context.Persons.ToArray();
            var any = context.Passes.ToArray();
            var szur = context.EntriesExits.ToArray();
            ;
        }
    }
}
