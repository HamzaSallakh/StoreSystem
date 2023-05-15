using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class PaymentsModel
    {
        public int PaymentsId { get; set; }
        public int OrdersId { get; set; }
        public DateTime PaymentsDate { get; set; }
        public int Amount { get; set; }
        //public Orders Orders { get; set; }
    }
}
