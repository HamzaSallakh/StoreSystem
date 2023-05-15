using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Buyers : Base
    {
        public string BuyersName { get; set; }
        public string BuyersAddress { get; set; }
    }
}
