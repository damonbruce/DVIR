using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Command.Interface;

namespace RailDelivery.Core.Command
{
    public class ReviewCommand : BaseData, IReviewCommand
    {
        public bool ReviewingDriver(Models.ReviewDriver driver)
        {
            if (driver == null)
                throw new ArgumentNullException("ReviewDriver");

            if (driver.DVIRPoweredId > 0) //Update the Powered Table
            {
                var powered = Db.DVIRPowers.FirstOrDefault(x => x.DVIRPowerId == driver.DVIRPoweredId);

                if(powered == null)
                    throw new ArgumentNullException("Powered entry not found");

                powered.ReviewingDriverName = driver.DriverName;
                powered.ReviewingDriverDate = driver.ReviewDate;
                powered.ReviewingDriverNumber = driver.DriverNumber;

                Db.SubmitChanges();
                return true;
            }
            else if (driver.DVIRTowedId > 0) //Update the Towed Table
            {
                var towed = Db.DVIRToweds.FirstOrDefault(x => x.DVIRTowedId == driver.DVIRTowedId);

                if(towed == null)
                    throw new ArgumentNullException("Towed entry not found.");

                towed.ReviewingDriverDate = driver.ReviewDate;
                towed.ReviewingDriverName = driver.DriverName;
                towed.ReviewingDriverNumber = driver.DriverNumber;

                Db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
