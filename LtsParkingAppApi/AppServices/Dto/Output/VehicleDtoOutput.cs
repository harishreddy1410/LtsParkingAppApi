using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class VehicleDtoOutput
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string RegNumber { get; set; }

        public int UserProfileId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
