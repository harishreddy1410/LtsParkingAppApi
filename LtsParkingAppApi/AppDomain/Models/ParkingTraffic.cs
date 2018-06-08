//---------------------------------------------------------------------------------------
// Description: entity for the ParkingTraffic table in database
//---------------------------------------------------------------------------------------
using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class ParkingTraffic:Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public int UserProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public int ParkingSlotId { get; set; }

        public virtual ParkingSlot ParkingSlot { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }
    }
}
