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
    public class OrdersService : IOrders<Orders>
    {
        public IRepository<Orders> Repository { get; }
        public OrdersService(IRepository<Orders> repository)
        {
            Repository = repository;
        }

        public void Add(Orders entity)
        {
            Repository.Add(entity);
        }

        public void Delete(Orders entity)
        {
            Repository.Delete(entity);
        }
        public void Update(Orders entity)
        {
            Repository.Update(entity);
        }
        public Orders Find(int Id)
        {
            return Repository.Find(Id);
        }

        public List<Orders> View()
        {
            return Repository.View();
        }
    }
}
