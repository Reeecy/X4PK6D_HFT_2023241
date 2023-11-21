using System.Collections.Generic;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Logic
{
    public interface IPassLogic
    {
        void Create(Pass pass);
        void Delete(int id);
        Pass Read(int id);
        IEnumerable<Pass> ReadAll();
        void Update(Pass pass);
    }
}