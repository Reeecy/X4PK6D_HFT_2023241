using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.Repository
{
    public class EntriesExitsRepository : Repository<EntriesExits>, IRepository<EntriesExits>
    {
        public EntriesExitsRepository(FaMDbContext context) : base(context)
        { }

        public override EntriesExits Read(int id)
        {
            return _context.EntriesExits.First(p => p.Id == id);
        }

        public override void Update(EntriesExits entity)
        {
            var entriesExits = Read(entity.Id);
            foreach (var property in typeof(EntriesExits).GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    property.SetValue(entriesExits, property.GetValue(entity));
            }
            _context.SaveChanges();
        }
    }
}