using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppServices.Dto
{
    public class LocationDtoOutput
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public ICollection<ParkingDivisionDtoOutput> ParkingDivisions { get; set; }

    }
}
