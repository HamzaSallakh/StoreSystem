using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseEntity
{
    public class StanderdJson
    {
        public bool Success { get; set; }
        //public HttpStatusCode code { get; set; }
        public int Code { get; set; }
        public object Message { get; set; }
        public object Data { get; set; }
    }
}
