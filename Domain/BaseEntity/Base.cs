using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseEntity
{
    public class Base
    {
        public int Id { get; set; }
        public int IsActive { get; set; }
        public int IsDelete { get; set; }
        public string CreateId { get; set; }
        public DateTime CreateDate { get; set; }
        public string EditId { get; set; }
        public DateTime EditDate { get; set; }
    }
}
