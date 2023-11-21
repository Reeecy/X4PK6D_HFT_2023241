using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public interface IPassRepository
    {
        void Create(Pass pass);
        Pass Read(int id);
        IQueryable<Pass> ReadAll();
        void Update(Pass pass);
        void Delete(int id);
    }
}
