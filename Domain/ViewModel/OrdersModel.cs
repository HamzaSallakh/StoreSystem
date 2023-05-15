using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class OrdersModel
    {
        public int OrdersId { get; set; }
        public int BuyersId { get; set; }
        public DateTime OrdersDate { get; set; }
        public int OrederSum { get; set; }
        //public Buyers Buyers { get; set; }
    }
}
