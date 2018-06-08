//---------------------------------------------------------------------------------------
// Description: dto for creating new employee shift
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class EmployeeShiftDtoInput
    {
        public int Id { get; set; }

        public string ShiftName { get; set; }

        public DateTime ShiftStartTime { get; set; }

        public DateTime ShiftEndTime { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
