using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IParkingSlotServices
    {
        Task<ParkingSlotDtoOutput> Get(int id = 0);

        Task<bool> Create(ParkingSlotDtoInput userProfileDtoInput);
                                 
        Task<bool> Update(UpdateParkingSlotDtoInput userProfileDtoInput);

        Task<bool> Delete(int id, int? DeletedBy);

        Task<List<ParkingSlotDtoOutput>> GetAll(bool includeInactive);
    }
}
