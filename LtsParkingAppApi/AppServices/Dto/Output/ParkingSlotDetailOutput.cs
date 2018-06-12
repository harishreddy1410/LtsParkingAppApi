using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingSlotDetailOutput
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Type { get; set; }

        public bool IsOccupied { get; set; }

        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }

        public string OccupiedBy { get; set; }

        public int SlotOccupiedByUserId { get; set; }
    }
}
