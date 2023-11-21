﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4PK6D_HFT_2023241.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
