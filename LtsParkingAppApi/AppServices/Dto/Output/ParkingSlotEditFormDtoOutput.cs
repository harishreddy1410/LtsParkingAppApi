using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingSlotEditFormDtoOutput
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public bool IsActive { get; set; }

        public string Type { get; set; }
    }
}
