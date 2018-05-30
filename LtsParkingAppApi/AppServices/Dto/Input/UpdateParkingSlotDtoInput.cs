using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class UpdateParkingSlotDtoInput
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool IsOccupied { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
    }
}
