using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public interface IEntriesExitsRepository
    {
        void Create(EntriesExits entriesExits);
        EntriesExits Read(int id);
        IQueryable<EntriesExits> ReadAll();
        void Update(EntriesExits entriesExits);
        void Delete(int id);
    }
}
