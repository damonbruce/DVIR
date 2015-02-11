using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailDelivery.Core.Models
{
    public class CertifyRepair
    {
        public int DVIRPowerId { get; set; }
        public int DVIRTowedId { get; set; }
        public DateTime CertifyDate { get; set; }
        public string RepairsMade { get; set; }
        public string CertifiedBy { get; set; }
        public string CertifyLocation { get; set; }
        public bool CertifyRepairsMade { get; set; }
    }
}
