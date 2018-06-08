//---------------------------------------------------------------------------------------
// Description: dto for unoccupying parking slot
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkUnParkVehicleDtoInput
    {
        public int PlarkingSlotId { get; set; }

        public int UserProfileId { get; set; }

        public int VehicleId { get; set; }        

        public PARKINGSTATUS Status { get; set; }
    }

    public enum PARKINGSTATUS
    {
        ParkingVehicle,
        UnParkingVehicle
    }
}
