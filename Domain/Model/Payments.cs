using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Payments:Base
    {
        public int OrdersId { get; set; }
        public DateTime PaymentsDate { get; set; }
        public int Amount { get; set; }
        public Orders Orders { get; set; }
        
    }
}
