using System.Collections.Generic;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Logic
{
    public interface IEntriesExitsLogic
    {
        void Create(EntriesExits entriesExits);
        void Delete(int id);
        EntriesExits Read(int id);
        IEnumerable<EntriesExits> ReadAll();
        void Update(EntriesExits entriesExits);
    }
}