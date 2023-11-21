using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public interface IPersonRepository
    {
        void Create(Person person);
        Person Read(int id);
        IQueryable<Person> ReadAll();
        void Update(Person person);
        void Delete(int id);
    }
}
