//---------------------------------------------------------------------------------------
// Description: dto for creating new parking slot
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingSlotDtoInput
    {
        public int Id { get; set; }

        public VehicleTypeDto Type { get; set; }

        public int Level { get; set; }

        public string Location { get; set; }

        public int SequenceOrder { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public int CompanyId { get; set; }
        
        public int ParkingDivisionId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
    public enum VehicleTypeDto
    {
        TwoWheeler = 0,
        FourWheeler = 1
    }
}
