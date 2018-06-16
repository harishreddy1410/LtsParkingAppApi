﻿//---------------------------------------------------------------------------------------
// Description: entity for the Location table in database
//---------------------------------------------------------------------------------------
using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class Location : Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<ParkingDivision> ParkingDivisions { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        [StringLength(100)]
        public string LocationDesignAttribute { get; set; }
    }
}
