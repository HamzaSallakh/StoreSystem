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
    public class PaymentsService : IPayments<Payments>
    {
        public IRepository<Payments> Repository { get; }
        public PaymentsService(IRepository<Payments> repository)
        {
            Repository = repository;
        }

        public void Add(Payments entity)
        {
            Repository.Add(entity);
        }

        public void Delete(Payments entity)
        {
            Repository.Delete(entity);
        }
        public void Update(Payments entity)
        {
            Repository.Update(entity);
        }
        public Payments Find(int Id)
        {
            return Repository.Find(Id);
        }

        public List<Payments> View()
        {
            return Repository.View();
        }
    }
}
