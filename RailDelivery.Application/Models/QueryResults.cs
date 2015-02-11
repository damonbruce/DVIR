using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailDelivery.DVIRApp.Models
{
    public class QueryResults
    {
        public int UniqueId { get; set; }
        public string UnitNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public double DriverNumber { get; set; }
        public string ResultType { get; set; }
    }
}
