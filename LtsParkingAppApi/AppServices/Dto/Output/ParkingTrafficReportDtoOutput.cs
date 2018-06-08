using AppDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingTrafficReportDtoOutput
    {
        /*
         
         
         UserName = x.UserProfile.PreferredName ?? string.Concat( x.UserProfile.FirstName," ",x.UserProfile.LastName ),
                        ParkingSlot = x.ParkingSlotId,
                        VehicleNumber = x.Vehicle.RegNumber,
                        InTime = x.InTime,
                        OutTime = x.OutTime,
                        ParkingSlotType = x.Type,
                        VehicleType = x.Vehicle.Type
         */
         [StringLength(100)]
         public string UserName { get; set; }

        public int ParkingSlotId { get; set; }

        public string VehicleNumber { get; set; }

        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public string VehicleType { get; set; }
        public string ParkingSlotType { get; set; }

    }
}
