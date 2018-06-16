//---------------------------------------------------------------------------------------
// Description: dto for retrieving parking division details
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingDivisionDtoOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SequenceOrder { get; set; }

        public int LocationId { get; set; }
        
        public virtual ICollection<ParkingSlotDtoOutput> ParkingSlots { get; set; }

        public string LocationDesignAttribute { get; set; }
    }
}
