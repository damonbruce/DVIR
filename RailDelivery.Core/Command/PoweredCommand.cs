using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Command.Interface;
using RailDelivery.Core.DataContext;
using RailDelivery.Core.Models;
using RailDelivery.Core.Query;

namespace RailDelivery.Core.Command
{
    public class PoweredCommand : BaseData, IPoweredCommand
    {
        public void SavePowered(Models.Powered powered)
        {
            var dbPowered = QueryPowered().FirstOrDefault(x => x.DVIRPowerId == powered.DVIRPowerId);

            if (dbPowered == null || powered == null)
                return;

            dbPowered.AirLines = powered.AirLines;
            dbPowered.Battery = powered.Battery;
            dbPowered.Belts = powered.Belts;
            dbPowered.BodyDoors = powered.BodyDoors;
            dbPowered.Brakes = powered.Brakes;
            dbPowered.CabDoorsWindows = powered.CabDoorsWindows;
            dbPowered.CertifiedBy = powered.CertifiedBy;
            dbPowered.CertifiedLocation = powered.CertifiedLocation;
            dbPowered.CertifyRepairsDate = powered.CertifiedRepairsDate;
            dbPowered.Clutch = powered.Clutch;
            dbPowered.CoolantLeak = powered.CoolantLeak;
            dbPowered.CoolantLevel = powered.CoolantLevel;
            dbPowered.CreatedDateTime = powered.CreateDate;
            dbPowered.DVIRPowerId = powered.DVIRPowerId;
            dbPowered.DriverNo = powered.DriverNo;
            dbPowered.FuelLeak = powered.FuelLeak;
            dbPowered.FireExtinguisher = powered.FireExtinguisher;
            dbPowered.FifthWheel = powered.FifthWheel;
            dbPowered.Exhaust = powered.Exhaust;
            dbPowered.EmergencyBrake = powered.EmergencyBrake;
            dbPowered.GreaseLeak = powered.GreaseLeak;
            dbPowered.GaugesWarning = powered.GuagesWarning;
            dbPowered.HeaterDefroster = powered.HeaterDefroster;
            dbPowered.Horns = powered.Horns;
            dbPowered.Lights = powered.Lights;
            dbPowered.Location = powered.Location;
            dbPowered.Mileage = powered.Mileage;
            dbPowered.Mirrors = powered.Mirrors;
            dbPowered.NoDefects = powered.NoDefects;
            dbPowered.OilLeak = powered.OilLeak;
            dbPowered.OilLevel = powered.OilLevel;
            dbPowered.OtherCoupling = powered.OtherCoupling;
            dbPowered.OtherEngine = powered.OtherEngine;
            dbPowered.OtherExterior = powered.OtherExterior;
            dbPowered.OtherGeneral = powered.OtherGeneral;
            dbPowered.OtherInCab = powered.OtherInCab;
            dbPowered.OtherSafetyEquipment = powered.OtherSafteyEquipment;
            dbPowered.ParkingBrake = powered.ParkingBrake;
            dbPowered.RONumber = powered.RONumber;
            dbPowered.Reflectors = powered.Reflectors;
            dbPowered.RepairsMadeFlag = powered.RepairsMadeFlag;
            dbPowered.ReviewingDriverDate = powered.ReviewingDriverDate;
            dbPowered.ReviewingDriverName = powered.ReviewingDriverName;
            dbPowered.ReviewingDriverNumber = powered.ReviewingDriverNumber;
            dbPowered.SeatBelts = powered.SeatBelts;
            dbPowered.ServiceBrakes = powered.ServiceBrakes;
            dbPowered.SpareFuses = powered.SpareFuses;
            dbPowered.Steering = powered.Steering;
            dbPowered.Suspension = powered.Suspension;
            dbPowered.TieDowns = powered.TieDowns;
            dbPowered.Tires = powered.Tires;
            dbPowered.TractorId = powered.TractorId;
            dbPowered.Triangles = powered.Triangles;
            dbPowered.WheelsRimsLugs = powered.WheelsRimsLugs;
            dbPowered.WindshieldWipers = powered.WinshieldWipers;
            dbPowered.GeneralRemark = powered.GeneralRemark;
            dbPowered.EngineRemark = powered.EngineRemark;
            dbPowered.InCabRemark = powered.InCabRemark;
            dbPowered.ExteriorRemark = powered.ExteriorRemark;

            Db.SubmitChanges();
        }

        private IQueryable<DVIRPower> QueryPowered()
        {
            return from x in Db.DVIRPowers
                   select x;
        }
    }
}
