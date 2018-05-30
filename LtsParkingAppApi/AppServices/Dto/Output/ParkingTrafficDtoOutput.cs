using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingTrafficDtoOutput
    {
        public int Id { get; set; }

        public int UserProfileId { get; set; }

        public int ParkingSlotId { get; set; }

        public int VehicleId { get; set; }

        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }

        public bool IsActive { get; set; }

        public bool isDeleted { get; set; }

    }
}
