using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class UserProfile : Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string PreferredName { get; set; } 
        
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public Roles Role { get; set; }

        
        public int EmployeeShiftId { get; set; }

        public virtual EmployeeShift EmployeeShift { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public virtual ParkingTraffic ParkingTraffic { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }

    public enum Roles
    {
        ADMIN,
        USER
    }
}
