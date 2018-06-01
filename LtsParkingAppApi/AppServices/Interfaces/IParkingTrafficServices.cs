using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IParkingTrafficServices
    {
        Task<ParkingTrafficDtoOutput> Get(int id = 0);        

        Task<bool> Create(ParkingTrafficDtoInput userProfileDtoInput);

        Task<bool> Update(ParkingTrafficDtoInput userProfileDtoInput);

        Task<bool> Delete(int id, int? DeletedBy);

        Task<List<ParkingTrafficDtoOutput>> GetAll(bool includeInactive);

        Task<List<ParkingTrafficDtoOutput>> GetVehicleTraffic(int vehicleId = 0);

        Task<List<ParkingTrafficDtoOutput>> TodaysParkingTraffic(string location);

        ParkingSlotUpdateStatus ParkVehicle(ParkUnParkVehicleDtoInput parkUnParkVehicleDtoInput);
    }
}
