//---------------------------------------------------------------------------------------
// Description: view model used for parking traffic details
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.ViewModels
{
    public class ParkingTrafficViewModel
    {
        public int UserProfileId { get; set; }

        public int ParkingSlotId { get; set; }

        public int VehicleId { get; set; }

        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }
    }
}
