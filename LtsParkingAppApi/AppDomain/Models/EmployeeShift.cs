using AppDomain.Models.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppDomain.Models
{
    public class EmployeeShift : Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string ShiftName { get; set; }

        public DateTime ShiftBeginTime { get; set; }

        public DateTime ShiftEndTime { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
