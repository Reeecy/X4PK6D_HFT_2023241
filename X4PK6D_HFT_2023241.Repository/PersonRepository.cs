using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public class PersonRepository : Repository<Person>, IRepository<Person>
    {
        public PersonRepository(FaMDbContext context) : base(context)
        {}

        public override Person Read(int id)
        {
            return _context.Persons.First(p => p.Id == id);
        }

        public override void Update(Person entity)
        {
            var person = Read(entity.Id);
            foreach (var property in typeof(Person).GetProperties())
            {
                property.SetValue(person, property.GetValue(entity));
            }
        }
    }
}
