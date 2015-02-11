using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailDelivery.Core.Models
{
    public class Search
    {
        public bool Powered { get; set; }
        public bool Towed { get; set; }
        public string UnitNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
