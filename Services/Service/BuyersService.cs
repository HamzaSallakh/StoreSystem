using Domain.Model;
using Repository.Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class BuyersService : IBuyers<Buyers>
    {
        public IRepository<Buyers> Repository { get; }
        public BuyersService(IRepository<Buyers> repository)
        {
            Repository = repository;
        }

        public void Add(Buyers entity)
        {
            Repository.Add(entity);
        }

        public void Delete(Buyers entity)
        {
            Repository.Delete(entity);
        }
        public void Update(Buyers entity)
        {
            Repository.Update(entity);
        }
        public Buyers Find(int Id)
        {
           return Repository.Find(Id);
        }

        public List<Buyers> View()
        {
            return Repository.View();
        }
    }
}
