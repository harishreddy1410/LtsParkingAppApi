using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class CompanyDtoOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ParkingDivisionDtoOutput> ParkingDivisions { get; set; }
    }
}
