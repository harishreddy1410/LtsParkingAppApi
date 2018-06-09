using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IParkingAreaService
    {
        Task<List<LocationDtoOutput>> GetParkingLocations();

        Task<List<CompanyDtoOutput>> GetCompaniesAtLocation(int locationId);

        Task<List<ParkingDivisionDtoOutput>> GetParkingDivisionsAtLocation(int locationId);
    }
}
