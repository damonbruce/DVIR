using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Command.Interface;
using RailDelivery.Core.DataContext;

namespace RailDelivery.Core.Command
{
    public class TowedCommand : BaseData, ITowedCommand
    {
        public void SaveTowed(Models.Towed towed)
        {
            var dbTowed = QueryTowed().FirstOrDefault(x => x.DVIRTowedId == towed.DVIRTowedId);

            dbTowed.BodyDoors = towed.BodyDoors;
            dbTowed.Brakes = towed.Brakes;
            dbTowed.CertifiedBy = towed.CertifiedBy;
            dbTowed.CertifyRepairsDate = towed.CertifiedRepairsDate;
            dbTowed.CreatedDateTime = towed.CreateDate;
            dbTowed.DVIRTowedId = towed.DVIRTowedId;
            dbTowed.DriverNo = towed.DriverNo;
            dbTowed.FifthWheelDolly = towed.FifthWheelDolly;
            dbTowed.KingpinUpperPlate = towed.KingpinUpperPlate;
            dbTowed.LandingGear = towed.LandingGear;
            dbTowed.Lights = towed.Lights;
            dbTowed.Mileage = towed.Mileage;
            dbTowed.NoDefects = towed.NoDefects;
            dbTowed.Other = towed.Other;
            dbTowed.OtherCoupling = towed.OtherCoupling;
            dbTowed.RONumber = towed.RONumber;
            dbTowed.RearEndProtection = towed.RearendProtection;
            dbTowed.Reflectors = towed.Reflectors;
            dbTowed.Remark1 = towed.Remark1;
            dbTowed.Remark2 = towed.Remark2;
            dbTowed.RepairsMadeFlag = towed.RepairsMadeFlag;
            dbTowed.ReviewingDriverDate = towed.ReviewingDriverDate;
            dbTowed.ReviewingDriverName = towed.ReviewingDriverName;
            dbTowed.ReviewingDriverNumber = towed.ReviewingDriverNumber;
            dbTowed.Suspension = towed.Suspension;
            dbTowed.TieDowns = towed.TieDowns;
            dbTowed.Tires = towed.Tires;
            dbTowed.TrailerChassis = towed.TrailerChassis;
            dbTowed.WheelsRimsLugs = towed.WheelsRimsLugs;
            dbTowed.Location = towed.CertifiedLocation;
            Db.SubmitChanges();
        }

        private IQueryable<DVIRTowed> QueryTowed()
        {
            return from x in Db.DVIRToweds
                   select x;
        }
    }
}
