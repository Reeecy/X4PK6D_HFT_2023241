using System.Collections.Generic;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Logic
{
    public interface IPersonLogic
    {
        void Create(Person person);
        void Delete(int id);
        Person Read(int id);
        IEnumerable<Person> ReadAll();
        void Update(Person person);

        // NON CRUD
        int? PersonCount();
        IEnumerable<object> PersonsWithEntriesExits();
        IEnumerable<object> PersonsWithExpiredPasses();
        IEnumerable<object> StudentsWithActivePasses();
        IEnumerable<object> PersonsWithMonthlyPassesAndTotalUsageDuration();
        IEnumerable<object> PersonsWithoutPasses();
    }
}