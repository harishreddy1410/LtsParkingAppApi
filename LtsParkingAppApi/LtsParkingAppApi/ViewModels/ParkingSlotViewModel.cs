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

        public string Type { get; set; }

        public bool IsOccupied { get; set; }

        public int SequenceOrder { get; set; }
    }
}
