using System;
using System.Collections.Generic;
using System.Text;

namespace LtsParkingAppApi.ViewModels
{
    public class UpdateParkingSlotViewModel
    {
        public int Id { get; set; }

        public bool IsOccupied { get; set; }

        //public DateTime InTime { get; set; }

        //public DateTime OutTime { get; set; }
    }
}
