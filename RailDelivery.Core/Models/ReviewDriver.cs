using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailDelivery.Core.Models
{
    public class ReviewDriver
    {
        public int DVIRPoweredId { get; set; }
        public int DVIRTowedId { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
