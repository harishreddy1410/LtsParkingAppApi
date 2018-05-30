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

        public string Location { get; set; }
    }
}
