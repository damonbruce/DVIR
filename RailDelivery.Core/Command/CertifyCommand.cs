using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Command.Interface;

namespace RailDelivery.Core.Command
{
    public class CertifyCommand : BaseData, ICertifyCommand
    {
        public bool CertifyRepairs(Models.CertifyRepair repair)
        {
            if(repair == null)
                throw new ArgumentNullException("certifyRepair");

            if (repair.DVIRPowerId > 0)
            {
                var powered = Db.DVIRPowers.FirstOrDefault(x => x.DVIRPowerId == repair.DVIRPowerId);

                if(powered == null)
                    throw new ArgumentNullException("Powered certify repair not found.");

                powered.CertifiedBy = repair.CertifiedBy;
                powered.CertifiedLocation = repair.CertifyLocation;
                powered.CertifyRepairsDate = repair.CertifyDate;
                powered.RONumber = repair.RepairsMade;
                powered.RepairsMadeFlag = repair.CertifyRepairsMade;
                Db.SubmitChanges();
                return true;
            }
            else if (repair.DVIRTowedId > 0)
            {
                var towed = Db.DVIRToweds.FirstOrDefault(x => x.DVIRTowedId == repair.DVIRTowedId);

                if (towed == null)
                    throw new ArgumentNullException("Towed certify repair not found.");

                towed.CertifiedBy = repair.CertifiedBy;
                towed.CertifiedLocation = repair.CertifyLocation;
                //towed.Location = repair.CertifyLocation;
                towed.CertifyRepairsDate = repair.CertifyDate;
                towed.RepairsMadeFlag = repair.CertifyRepairsMade;
                towed.RONumber = repair.RepairsMade;
                Db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
