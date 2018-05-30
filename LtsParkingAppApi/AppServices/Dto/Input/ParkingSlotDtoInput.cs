using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingSlotDtoInput
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public int Level { get; set; }

        public string Location { get; set; }

        public int SequenceOrder { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

    }
}
