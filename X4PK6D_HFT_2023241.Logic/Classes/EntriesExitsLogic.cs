using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;
using X4PK6D_HFT_2023241.Repository;

namespace X4PK6D_HFT_2023241.Logic
{
    public class EntriesExitsLogic : IEntriesExitsLogic
    {
        IRepository<EntriesExits> _repo;

        public EntriesExitsLogic(IRepository<EntriesExits> repository)
        {
            _repo = repository;
        }

        public void Create(EntriesExits entriesExits)
        {
            if (entriesExits == null)
                throw new ArgumentNullException(nameof(entriesExits));
            else
                _repo.Create(entriesExits);
        }

        public EntriesExits Read(int id)
        {
            var entriesExits = _repo.Read(id);
            if (entriesExits == null)
                throw new ArgumentNullException(nameof(entriesExits));
            else
                return entriesExits;
        }

        public void Delete(int id)
        {
            var entriesExits = _repo.Read(id);
            if (entriesExits == null)
                throw new ArgumentNullException(nameof(entriesExits));
            else
                _repo.Delete(id);
        }

        public IEnumerable<EntriesExits> ReadAll()
        {
            return _repo.ReadAll();
        }

        public void Update(EntriesExits entriesExits)
        {
            if (entriesExits == null)
                throw new ArgumentNullException(nameof(entriesExits));
            else
                _repo.Update(entriesExits);
        }
    }
}
