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

        // Retrieve Persons with their Entry-Exit Information
        public IEnumerable<object> PersonsWithEntriesExits()
        {
            var persons = _repo.ReadAll();
            var personWithEntriesExits = from x in persons
                                         select new
                                         {
                                             FullName = $"{x.FirstName} {x.LastName}",
                                             Entries = x.EntriesExits.Select(x => x.EntryTime),
                                             Exits = x.EntriesExits.Select(x => x.ExitTime)
                                         };
            return personWithEntriesExits;
        }

        // Retrieve Persons with Expired Passes
        public IEnumerable<object> PersonsWithExpiredPasses()
        {
            var persons = _repo.ReadAll();
            var personWithExpiredPasses = from x in persons
                                          where x.Pass.EndDate < DateTime.Now
                                          select new
                                          {
                                              FullName = $"{x.FirstName} {x.LastName}",
                                              PassType = x.Pass.PassType,
                                              EndDate = x.Pass.EndDate
                                          };
            return personWithExpiredPasses;
        }


        // Retrieve Students with Active Passes and Their Entry-Exit Information
        public IEnumerable<object> StudentsWithActivePasses()
        {
            var persons = _repo.ReadAll();
            var studentsWithActivePasses = from x in persons
                                           where x.Pass.EndDate > DateTime.Now && x.Pass.PassType == "Student"
                                           select new
                                           {
                                               FullName = $"{x.FirstName} {x.LastName}",
                                               PassType = x.Pass.PassType,
                                               EndDate = x.Pass.EndDate,
                                               Entries = x.EntriesExits.Select(x => x.EntryTime),
                                               Exits = x.EntriesExits.Select(x => x.ExitTime)
                                           };
            return studentsWithActivePasses;
        }

        // Retrieve Persons with Monthly Passes and Their Total Usage Duration
        public IEnumerable<object> PersonsWithMonthlyPassesAndTotalUsageDuration()
        {
            var persons = _repo.ReadAll();
            var personWithMonthlyPasses = from x in persons
                                          where x.Pass.PassType == "Monthly"
                                          select new
                                          {
                                              FullName = $"{x.FirstName} {x.LastName}",
                                              PassType = x.Pass.PassType,
                                              StartDate = x.Pass.StartDate,
                                              EndDate = x.Pass.EndDate,
                                              TotalUsageDuration = x.EntriesExits.Select(x => x.ExitTime.Hour - x.EntryTime.Hour).Sum()
                                          };
            return personWithMonthlyPasses;
        }

        // Retrieve Persons without Passes
        public IEnumerable<object> PersonsWithoutPasses()
        {
            var persons = _repo.ReadAll();
            var personWithoutPasses = from x in persons
                                      where x.PassId < 1 || x.PassId > 10
                                      select new
                                      {
                                          FullName = $"{x.FirstName} {x.LastName}"
                                      };
            return personWithoutPasses;
        }
    }
}
