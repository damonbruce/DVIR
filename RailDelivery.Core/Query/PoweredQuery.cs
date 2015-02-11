using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Models;
using RailDelivery.Core.Query.Interface;

namespace RailDelivery.Core.Query
{
    public class PoweredQuery : BaseData, IPowerQuery
    {
        public List<Models.Powered> FindPowered(Models.Search searchModel)
        {
            var results =
                QueryPowered()
                    .Where(
                        x =>
                            x.CreateDate.GetValueOrDefault().Date >= searchModel.StartDate.Date && x.CreateDate.GetValueOrDefault().Date <= searchModel.EndDate.Date &&
                            x.TractorId.ToLower().Contains(searchModel.UnitNumber.ToLower()));

            return !results.Any() ? new List<Powered>() : results.ToList();
        }

        public Models.Powered GetPowered(int powerId)
        {
            return QueryPowered().FirstOrDefault(x => x.DVIRPowerId == powerId);
        }

        private IQueryable<Models.Powered> QueryPowered()
        {
            return from x in Db.DVIRPowers
                select new Powered()
                {
                    AirLines = x.AirLines,
                    Battery = x.Battery,
                    Belts = x.Belts,
                    BodyDoors = x.BodyDoors,
                    Brakes = x.Brakes,
                    CabDoorsWindows = x.CabDoorsWindows,
                    CertifiedBy = x.CertifiedBy,
                    CertifiedLocation = x.CertifiedLocation,
                    CertifiedRepairsDate = x.CertifyRepairsDate.HasValue ? (DateTime?)x.CertifyRepairsDate.Value.Date : null,
                    Clutch = x.Clutch,
                    CoolantLeak = x.CoolantLeak,
                    CoolantLevel = x.CoolantLevel,
                    CreateDate = x.CreatedDateTime,
                    DVIRPowerId = x.DVIRPowerId,
                    DriverNo = x.DriverNo,
                    FuelLeak = x.FuelLeak,
                    FireExtinguisher = x.FireExtinguisher,
                    FifthWheel = x.FifthWheel,
                    Exhaust = x.Exhaust,
                    EmergencyBrake = x.EmergencyBrake,
                    GreaseLeak = x.GreaseLeak,
                    GuagesWarning = x.GaugesWarning,
                    HeaterDefroster = x.HeaterDefroster,
                    Horns = x.Horns,
                    Lights = x.Lights,
                    Location = x.Location,
                    Mileage = x.Mileage,
                    Mirrors = x.Mirrors,
                    NoDefects = x.NoDefects,
                    OilLeak = x.OilLeak,
                    OilLevel = x.OilLevel,
                    OtherCoupling = x.OtherCoupling,
                    OtherEngine = x.OtherEngine,
                    OtherExterior = x.OtherExterior,
                    OtherGeneral = x.OtherGeneral,
                    OtherInCab = x.OtherInCab,
                    OtherSafteyEquipment = x.OtherSafetyEquipment,
                    ParkingBrake = x.ParkingBrake,
                    RONumber = x.RONumber,
                    Reflectors = x.Reflectors,
                    RepairsMadeFlag = x.RepairsMadeFlag,
                    ReviewingDriverDate = x.ReviewingDriverDate.HasValue ? (DateTime?)x.ReviewingDriverDate.Value.Date : null,
                    ReviewingDriverName = x.ReviewingDriverName,
                    ReviewingDriverNumber = x.ReviewingDriverNumber,
                    SeatBelts = x.SeatBelts,
                    ServiceBrakes = x.ServiceBrakes,
                    SpareFuses = x.SpareFuses,
                    Steering = x.Steering,
                    Suspension = x.Suspension,
                    TieDowns = x.TieDowns,
                    Tires = x.Tires,
                    TractorId = x.TractorId,
                    Triangles = x.Triangles,
                    WheelsRimsLugs = x.WheelsRimsLugs,
                    WinshieldWipers = x.WindshieldWipers,
                    GeneralRemark = x.GeneralRemark,
                    EngineRemark = x.EngineRemark,
                    ExteriorRemark = x.ExteriorRemark,
                    InCabRemark = x.InCabRemark
                };
        }
    }
}
