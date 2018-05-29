using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class ParkingSlot: Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string Location { get; set; }

        public Int16 Level { get; set; }

        public string Type { get; set; }

        public bool IsOccupied { get; set; }

        public int SequenceOrder { get; set; }

        //public virtual ParkingTraffic ParkingTraffic { get; set; }
    }
}
