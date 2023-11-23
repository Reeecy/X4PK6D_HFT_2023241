using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;
using X4PK6D_HFT_2023241.Repository;

namespace X4PK6D_HFT_2023241.Logic
{
    public class PassLogic : IPassLogic
    {
        IRepository<Pass> _repo;
        public PassLogic(IRepository<Pass> repository)
        {
            _repo = repository;
        }

        public void Create(Pass pass)
        {
            if (pass == null)
                throw new ArgumentNullException(nameof(pass));
            else if (pass.Id < 1 && pass.Id > 8)
                throw new ArgumentOutOfRangeException(nameof(pass));
            else if (pass.PassType == "")
                throw new ArgumentNullException(nameof(pass));
            else if (pass.Price < 0)
                throw new ArgumentOutOfRangeException(nameof(pass));
            else
                _repo.Create(pass);
        }

        public Pass Read(int id)
        {
            var pass = _repo.Read(id);
            if (pass == null)
                throw new ArgumentNullException(nameof(pass));
            else
                return pass;
        }

        public void Delete(int id)
        {
            var pass = _repo.Read(id);
            if (pass == null)
                throw new ArgumentNullException(nameof(pass));
            else
                _repo.Delete(id);
        }

        public IEnumerable<Pass> ReadAll()
        {
            return _repo.ReadAll();
        }

        public void Update(Pass pass)
        {
            _repo.Update(pass);
        }
    }
}
