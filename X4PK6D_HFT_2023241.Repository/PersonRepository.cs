using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    // static method?
    public class PersonRepository : IPersonRepository
    {
        FaMDbContext _context;

        public PersonRepository(FaMDbContext context)
        {
            _context = context;
        }

        public void Create(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Persons.Remove(Read(id));
            _context.SaveChanges();
        }

        public Person Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Person> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
