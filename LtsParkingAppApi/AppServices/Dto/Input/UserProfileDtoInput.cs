using AppDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class UserProfileDtoInput
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PreferredName { get; set; }

        public int? LocationId { get; set; }

        public Roles Role { get; set; }

        public int EmployeeShiftId { get; set; }

        public string Email { get; set; }

        public int CompanyId { get; set; }
    }
}
