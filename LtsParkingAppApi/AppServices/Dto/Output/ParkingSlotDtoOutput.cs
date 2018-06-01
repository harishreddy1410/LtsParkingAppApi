﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingSlotDtoOutput
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public Int16 Level { get; set; }

        public string Type { get; set; }

        public bool IsOccupied { get; set; }

        public int SequenceOrder { get; set; }

    }
}