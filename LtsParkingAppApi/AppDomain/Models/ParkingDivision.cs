//---------------------------------------------------------------------------------------
// Description: entity for the ParkingDivision table in database
//---------------------------------------------------------------------------------------
using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class ParkingDivision : Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string Name { get; set; }

        public int SequenceOrder { get; set; }

        public int LocationId { get; set; }

       // public virtual Location Location { get; set; }

        public Int16 SlotCapactity { get; set; }

        public virtual ICollection<ParkingSlot> ParkingSlots { get; set; }
    }
}
