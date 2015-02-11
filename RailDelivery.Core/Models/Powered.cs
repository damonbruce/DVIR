using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailDelivery.Core.Models
{
    public class Powered
    {
        public int DVIRPowerId { get; set; } //[DVIRPowerId] [int] IDENTITY(1,1) NOT NULL,
        public int? DriverNo { get; set; }    //[DriverNo] [int] NULL,
        public string TractorId { get; set; }    //[TractorId] [varchar](11) NULL,
        public string Location { get; set; }    //[Location] [varchar](20) NULL,
        public string Mileage { get; set; }    //[Mileage] [varchar](10) NULL,
        public bool? NoDefects { get; set; }    //[NoDefects] [bit] NULL,
        public bool? CabDoorsWindows { get; set; }    //[CabDoorsWindows] [bit] NULL,
        public bool? BodyDoors { get; set; }    //[BodyDoors] [bit] NULL,
        public bool? OilLeak { get; set; }    //[OilLeak] [varchar](20) NULL,
        public bool? GreaseLeak { get; set; }   //[GreaseLeak] [varchar](20) NULL,
        public bool? CoolantLeak { get; set; }    //[CoolantLeak] [bit] NULL,
        public bool? FuelLeak { get; set; }    //[FuelLeak] [bit] NULL,
        public string OtherGeneral { get; set; }    //[OtherGeneral] [varchar](20) NULL,
        public bool? OilLevel { get; set; }    //[OilLevel] [bit] NULL,
        public bool? CoolantLevel { get; set; }    //[CoolantLevel] [bit] NULL,
        public bool? Belts { get; set; }    //[Belts] [varchar](20) NULL,
        public string OtherEngine { get; set; }   //[OtherEngine] [varchar](20) NULL,
        public bool? GuagesWarning { get; set; }    //[GaugesWarning] [bit] NULL,
        public bool? WinshieldWipers { get; set; }    //[WindshieldWipers] [bit] NULL,
        public bool? Horns { get; set; }    //[Horns] [bit] NULL,
        public bool? HeaterDefroster { get; set; }    //[HeaterDefroster] [bit] NULL,
        public bool? Mirrors { get; set; }    //[Mirrors] [bit] NULL,
        public bool? Steering { get; set; }    //[Steering] [bit] NULL,
        public bool? Clutch { get; set; }    //[Clutch] [bit] NULL,
        public bool? ServiceBrakes { get; set; }    //[ServiceBrakes] [bit] NULL,
        public bool? ParkingBrake { get; set; }    //[ParkingBrake] [bit] NULL,
        public bool? EmergencyBrake { get; set; }   //[EmergencyBrake] [bit] NULL,
        public bool? Triangles { get; set; }    //[Triangles] [bit] NULL,
        public bool? FireExtinguisher { get; set; }    //[FireExtinguisher] [bit] NULL,
        public bool? OtherSafteyEquipment { get; set; }    //[OtherSafetyEquipment] [bit] NULL,
        public bool? SpareFuses { get; set; }    //[SpareFuses] [bit] NULL,
        public bool? SeatBelts { get; set; }    //[SeatBelts] [bit] NULL,
        public string OtherInCab { get; set; }    //[OtherInCab] [bit] NULL,
        public bool? Lights { get; set; }    //[Lights] [bit] NULL,
        public bool? Reflectors { get; set; }    //[Reflectors] [bit] NULL,
        public bool? Suspension { get; set; }    //[Suspension] [bit] NULL,
        public bool? Tires { get; set; }    //[Tires] [bit] NULL,
        public bool? WheelsRimsLugs { get; set; }    //[WheelsRimsLugs] [bit] NULL,
        public bool? Battery { get; set; }    //[Battery] [bit] NULL,
        public bool? Exhaust { get; set; }    //[Exhaust] [bit] NULL,
        public bool? Brakes { get; set; }    //[Brakes] [bit] NULL,
        public bool? AirLines { get; set; }    //[AirLines] [bit] NULL,
        public bool? FifthWheel { get; set; }    //[FifthWheel] [bit] NULL,
        public bool? OtherCoupling { get; set; }    //[OtherCoupling] [bit] NULL,
        public bool? TieDowns { get; set; }    //[TieDowns] [bit] NULL,
        public string OtherExterior { get; set; }    //[OtherExterior] [varchar](20) NULL,
        public DateTime? CreateDate { get; set; }    //[CreatedDateTime] [datetime] NULL,
        public DateTime? CertifiedRepairsDate { get; set; }    //[CertifyRepairsDate] [datetime] NULL,
        public bool? RepairsMadeFlag { get; set; }    //[RepairsMadeFlag] [bit] NULL,
        public string RONumber { get; set; }    //[RONumber] [varchar](20) NULL,
        public string CertifiedBy { get; set; }    //[CertifiedBy] [varchar](30) NULL,
        public string CertifiedLocation { get; set; }    //[CertifiedLocation] [varchar](30) NULL,
        public DateTime? ReviewingDriverDate { get; set; }    //[ReviewingDriverDate] [datetime] NULL,
        public string ReviewingDriverName { get; set; }   //[ReviewingDriverName] [varchar](30) NULL,
        public string ReviewingDriverNumber { get; set; }    //[ReviewingDriverNumber] [varchar](10) NULL,
        public string GeneralRemark { get; set; }
        public string EngineRemark { get; set; }
        public string InCabRemark { get; set; }
        public string ExteriorRemark { get; set; }
    }
}
