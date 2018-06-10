//---------------------------------------------------------------------------------------
// Description: view model used for parkign slot details
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.ViewModels
{
    public class ParkingSlotViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public Int16 Level { get; set; }

        public VehicleTypeVM Type { get; set; }

        public bool IsOccupied { get; set; }

        public int SequenceOrder { get; set; }

        public int CompanyId { get; set; }

        public int ParkingDivisionId { get; set; }

        public int CreatedBy { get; set; }
    }
    public enum VehicleTypeVM
    {
        TwoWheeler = 0,
        FourWheeler = 1
    }
}
