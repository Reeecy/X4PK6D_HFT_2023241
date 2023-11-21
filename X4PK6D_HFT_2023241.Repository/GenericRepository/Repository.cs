using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4PK6D_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected FaMDbContext _context;

        public Repository(FaMDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(Read(id));
            _context.SaveChanges();
        }

        public IQueryable<T> Readall()
        {
            return _context.Set<T>();
        }

        public abstract T Read(int id);

        public abstract void Update(T entity);
    }
}
