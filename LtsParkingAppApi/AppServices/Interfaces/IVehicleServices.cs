using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IVehicleServices
    {
        Task<VehicleDtoOutput> Get(int id = 0);

        Task<bool> Create(VehicleDtoInput userProfileDtoInput);

        Task<bool> Update(VehicleDtoInput userProfileDtoInput);

        Task<bool> Delete(int id, int? DeletedBy);

        Task<List<VehicleDtoOutput>> GetAll(bool includeInactive);
    }
}
