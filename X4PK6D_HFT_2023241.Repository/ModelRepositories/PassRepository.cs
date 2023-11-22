using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public class PassRepository : Repository<Pass>, IRepository<Pass>
    {
        public PassRepository(FaMDbContext context) : base(context)
        { }
        public override Pass Read(int id)
        {
            return _context.Passes.First(p => p.Id == id);
        }

        public override void Update(Pass entity)
        {
            var pass = Read(entity.Id);
            foreach (var property in typeof(Pass).GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    property.SetValue(pass, property.GetValue(entity));
            }
            _context.SaveChanges();
        }
    }
}
