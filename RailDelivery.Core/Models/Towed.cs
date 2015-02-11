using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailDelivery.Core.Models
{
    public class Towed
    {
        public int DVIRTowedId { get; set; } //[DVIRTowedId] [int] IDENTITY(1,1) NOT NULL,
        public int? DriverNo { get; set; }    //[DriverNo] [int] NULL,
        public string TrailerChassis { get; set; }    //[TractorId] [varchar](11) NULL,
        public string Mileage { get; set; }    //[Mileage] [varchar](10) NULL,
        public bool? NoDefects { get; set; }    //[NoDefects] [bit] NULL,
        public bool? BodyDoors { get; set; }    //[BodyDoors] [bit] NULL,
        public bool? TieDowns { get; set; }    //[TieDowns] [bit] NULL,
        public bool? Lights { get; set; }    //[Lights] [bit] NULL,
        public bool? Reflectors { get; set; }    //[Reflectors] [bit] NULL,
        public bool? Suspension { get; set; }    //[Suspension] [bit] NULL,
        public bool? Tires { get; set; }    //[Tires] [bit] NULL,
        public bool? WheelsRimsLugs { get; set; }    //[WheelsRimsLugs] [bit] NULL,
        public bool? Brakes { get; set; }
        public bool? LandingGear { get; set; }
        public bool? KingpinUpperPlate { get; set; }
        public bool? FifthWheelDolly { get; set; }
        public bool? OtherCoupling { get; set; }
        public bool? RearendProtection { get; set; }
        public string Other { get; set; }
        public DateTime? CreateDate { get; set; }    //[CreatedDateTime] [datetime] NULL,
        public DateTime? CertifiedRepairsDate { get; set; }    //[CertifyRepairsDate] [datetime] NULL,
        public bool? RepairsMadeFlag { get; set; }    //[RepairsMadeFlag] [bit] NULL,
        public string RONumber { get; set; }    //[RONumber] [varchar](20) NULL,
        public string CertifiedBy { get; set; }    //[CertifiedBy] [varchar](30) NULL,
        public string CertifiedLocation { get; set; }    //[CertifiedLocation] [varchar](30) NULL,
        public DateTime? ReviewingDriverDate { get; set; }    //[ReviewingDriverDate] [datetime] NULL,
        public string ReviewingDriverName { get; set; }   //[ReviewingDriverName] [varchar](30) NULL,
        public string ReviewingDriverNumber { get; set; }    //[ReviewingDriverNumber] [varchar](10) NULL,
        public string Location { get; set; }
        public string Remark1 { get; set; }    //[Remark1] [varchar](40) NULL,
        public string Remark2 { get; set; }    //[Remark2] [varchar](40) NULL,
    }
}
