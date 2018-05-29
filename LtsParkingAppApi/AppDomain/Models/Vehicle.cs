using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class Vehicle:Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public int UserProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public string Type { get; set; }

        public string RegNumber { get; set; }

        public virtual ParkingTraffic ParkingTraffic { get; set; }

    }
}
