using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Models;
using RailDelivery.Core.Query.Interface;

namespace RailDelivery.Core.Query
{
    public class TowedQuery : BaseData, ITowedQuery
    {
        public List<Models.Towed> FindTowed(Models.Search searchModel)
        {
            var towed = QueryTowed()
                    .Where(x => x.CreateDate >= searchModel.StartDate && x.CreateDate <= searchModel.EndDate && x.TrailerChassis.Contains(searchModel.UnitNumber));

            return !towed.Any() ? new List<Towed>() : towed.ToList();
        }

        public Models.Towed GetTowed(int towedId)
        {
            return QueryTowed().FirstOrDefault(x => x.DVIRTowedId == towedId);
        }

        private IQueryable<Models.Towed> QueryTowed()
        {
            return from x in Db.DVIRToweds
                select new Models.Towed()
                {
                    BodyDoors = x.BodyDoors,
                    Brakes = x.Brakes,
                    CertifiedBy = x.CertifiedBy,
                    CertifiedRepairsDate = x.CertifyRepairsDate.HasValue ? (DateTime?)x.CertifyRepairsDate.Value.Date : null,
                    CreateDate = x.CreatedDateTime.HasValue ? (DateTime?)x.CreatedDateTime.Value.Date : null,
                    DVIRTowedId = x.DVIRTowedId,
                    DriverNo = x.DriverNo,
                    FifthWheelDolly = x.FifthWheelDolly,
                    KingpinUpperPlate = x.KingpinUpperPlate,
                    LandingGear = x.LandingGear,
                    Lights = x.Lights,
                    Mileage = x.Mileage,
                    NoDefects = x.NoDefects,
                    Other = x.Other,
                    OtherCoupling = x.OtherCoupling,
                    RONumber = x.RONumber,
                    RearendProtection = x.RearEndProtection,
                    Reflectors = x.Reflectors,
                    Remark1 = x.Remark1,
                    Remark2 = x.Remark2,
                    RepairsMadeFlag = x.RepairsMadeFlag,
                    ReviewingDriverDate = x.ReviewingDriverDate.HasValue ? (DateTime?)x.ReviewingDriverDate.Value.Date : null,
                    ReviewingDriverName = x.ReviewingDriverName,
                    ReviewingDriverNumber = x.ReviewingDriverNumber,
                    Suspension = x.Suspension,
                    TieDowns = x.TieDowns,
                    Tires = x.Tires,
                    TrailerChassis = x.TrailerChassis,
                    WheelsRimsLugs = x.WheelsRimsLugs,
                    Location = x.Location,
                    CertifiedLocation = x.CertifiedLocation
                };
        } 
    }
}
