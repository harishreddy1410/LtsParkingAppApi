//---------------------------------------------------------------------------------------
// Description: entity for the Company table in database
//---------------------------------------------------------------------------------------
using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class Company:Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string Name { get; set; }

        public int ParkingLocationId { get; set; }

        public virtual Location ParkingLocation { get; set; }
    }
}
