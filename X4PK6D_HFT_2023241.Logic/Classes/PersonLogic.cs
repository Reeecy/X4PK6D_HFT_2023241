using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;
using X4PK6D_HFT_2023241.Repository;

namespace X4PK6D_HFT_2023241.Logic
{
    public class PersonLogic : IPersonLogic
    {
        IRepository<Person> _repo;
        public PersonLogic(IRepository<Person> repository)
        {
            _repo = repository;
        }

        public void Create(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            else
                _repo.Create(person);
        }

        public Person Read(int id)
        {
            var person = _repo.Read(id);
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            else
                return person;
        }

        public void Delete(int id)
        {
            var person = _repo.Read(id);
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            else
                _repo.Delete(id);
        }

        public IEnumerable<Person> ReadAll()
        {
            return _repo.ReadAll();
        }

        public void Update(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            else
                _repo.Update(person);
        }

        //NON CRUD
        public int? PersonCount()
        {
            return _repo.ReadAll().Count();
        }
    }
}
