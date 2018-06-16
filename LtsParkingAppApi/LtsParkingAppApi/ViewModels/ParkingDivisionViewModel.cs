//---------------------------------------------------------------------------------------
// Description: view model used for the parking division
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.ViewModels
{
    public class ParkingDivisionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SequenceOrder { get; set; }

        public int LocationId { get; set; }

        public List<ParkingSlotViewModel> ParkingSlots { get; set; }

        public string LocationDesignAttribute { get; set; }
    }
}
