using Domain;
using Domain.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T :Base
    {
        public AppDbContext Db { get; }
        private DbSet<T> enties;
        public Repository(AppDbContext _DbContext)
        {
            Db = _DbContext;
            enties = Db.Set<T>();
        }

        public List<T> View()
        {
            return enties.ToList();
        }

        public T Find(int Id)
        {
            return enties.SingleOrDefault(x => x.Id == Id);
        }

        public void Add(T entity)
        {
            enties.Add(entity);
            Db.SaveChanges();
        }

        public void Update(T entity)
        {
            enties.Update(entity);
            Db.SaveChanges();
        }

        public void Delete(T entity)
        {
            enties.Remove(entity);
            Db.SaveChanges();
        }
    }
}
