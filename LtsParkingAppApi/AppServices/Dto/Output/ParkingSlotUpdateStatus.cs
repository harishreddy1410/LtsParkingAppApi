//---------------------------------------------------------------------------------------
// Description: dto for updating the parking slot
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppServices.Dto
{
    public class ParkingSlotUpdateStatus
    {
        public bool Success { get; set; }

        [StringLength(50)]
        public string ValidationMessage { get; set; }
    }


    public enum ParkingSlotUpdateStatusResponse
    {
        PARK_SUCCESS,
        ALREADY_OCCUPIED,
        PARK_VACATED,
        INVALID_SLOT,
        INVALID_USERID,
        RECORD_NOT_UPDATED
    }
}
