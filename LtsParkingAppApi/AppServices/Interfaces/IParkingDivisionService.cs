using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IParkingDivisionService
    {
        Task<List<ParkingDivisionDtoOutput>> GetLocationParkingArea(int locationId);
    }
}
