using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class ParkingTrafficServices:IParkingTrafficServices
    {
        IRepository _repo;
        IMapper _mapper;
        IParkingSlotServices _parkingSlotServices;
        IGenericServices _genericServices;
        public ParkingTrafficServices(IRepository repo, IMapper mapper, IParkingSlotServices parkingSlotServices,
            IGenericServices genericServices)
        {
            _repo = repo;
            _mapper = mapper;
            _parkingSlotServices = parkingSlotServices;
            _genericServices = genericServices;
        }

        public Task<bool> Create(ParkingTrafficDtoInput parkingTrafficDtoInput)
        {
            try
            {
                _repo.Create<ParkingTraffic>(_mapper.Map<ParkingTraffic>(parkingTrafficDtoInput), "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
                throw;
            }

        }

        public Task<bool> Delete(int id, int? DeletedBy)
        {
            var toBeDeleted = _repo.GetById<ParkingTraffic>(id);
            try
            {
                toBeDeleted.IsDeleted = true;
                toBeDeleted.IsActive = false;
                toBeDeleted.ModifiedBy = DeletedBy;
                _repo.Update(toBeDeleted);
                return Task.FromResult(true);

            }
            catch (System.Exception)
            {
                return Task.FromResult(true);
                throw;
            }
        }

        public Task<List<ParkingTrafficDtoOutput>> GetAll(bool includeInactive)
        {
            return Task.FromResult(_mapper.Map<List<ParkingTrafficDtoOutput>>(_repo.GetQueryable<ParkingTraffic>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));
        }

        public Task<ParkingTrafficDtoOutput> Get(int id = 0)
        {
            try
            {
                return Task.FromResult(_mapper.Map<ParkingTrafficDtoOutput>(_repo.Get<ParkingTraffic>(x => x.Id == id).FirstOrDefault()));
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task<bool> Update(ParkingTrafficDtoInput parkingTrafficDtoInput)
        {
            var updated = _mapper.Map<ParkingTraffic>(parkingTrafficDtoInput);
            try
            {
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(true);
                throw;
            }
        }
        public Task<List<ParkingTrafficDtoOutput>> GetVehicleTraffic(int vehicleId = 0)
        {
            return Task.FromResult(_mapper.Map<List<ParkingTrafficDtoOutput>>(_repo.Get<ParkingTraffic>(x => x.VehicleId == vehicleId)));

        }

        public Task<List<ParkingTrafficDtoOutput>> TodaysParkingTraffic(string location)
        {
            return Task.FromResult(_mapper.Map<List<ParkingTrafficDtoOutput>>(_repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlot.Location == location && x.CreatedDate.Date == DateTime.Today)));
        }


        public ParkingSlotUpdateStatus ParkVehicle(ParkUnParkVehicleDtoInput parkUnParkVehicleDtoInput)
        {
           
           

            //var test = await _parkingSlotServices.Get(parkUnParkVehicleDtoInput.PlarkingSlotId);
            var parkingSlot = _repo.GetById<ParkingSlot>(parkUnParkVehicleDtoInput.PlarkingSlotId);
            var parkingUser = _repo.GetQueryable<UserProfile>(x=>x.Id ==parkUnParkVehicleDtoInput.UserProfileId,
                null,
                null,
                null,
                y=>y.EmployeeShift            
            ).AsNoTracking().FirstOrDefault();
            if(parkingSlot == null)
            {
                return new ParkingSlotUpdateStatus() {
                    Success = false,
                    ValidationMessage = ParkingSlotUpdateStatusResponse.INVALID_SLOT.ToString()
                };
            }
            if (parkingUser == null)
            {
                return new ParkingSlotUpdateStatus()
                {
                    Success = false,
                    ValidationMessage = ParkingSlotUpdateStatusResponse.INVALID_USERID.ToString()
                };
            }

            //Check if slot is available 
            if (!parkingSlot.IsOccupied && parkUnParkVehicleDtoInput.Status == PARKINGSTATUS.ParkingVehicle)
            {
                parkingSlot.IsOccupied = true;
                _repo.Create<ParkingTraffic>(new ParkingTraffic() {
                        ParkingSlotId = parkUnParkVehicleDtoInput.PlarkingSlotId,
                        UserProfileId = parkUnParkVehicleDtoInput.UserProfileId,
                        IsActive = true,
                        VehicleId = parkUnParkVehicleDtoInput.VehicleId,
                        CreatedBy = parkUnParkVehicleDtoInput.UserProfileId,
                        InTime = _genericServices.GetCurrentTime(),
                        OutTime =  parkingUser.EmployeeShift.ShiftEndTime
                });
                _repo.Save();

                return new ParkingSlotUpdateStatus() {
                    Success = true,
                    ValidationMessage = ParkingSlotUpdateStatusResponse.PARK_SUCCESS.ToString() };
            }
            //Check if slot is not available do not allow to occupy 
            else if (parkingSlot.IsOccupied && parkUnParkVehicleDtoInput.Status == PARKINGSTATUS.ParkingVehicle)
            {
                return new ParkingSlotUpdateStatus()
                {
                    Success = false,
                    ValidationMessage = ParkingSlotUpdateStatusResponse.ALREADY_OCCUPIED.ToString()
                };
                //return slot is not available 
            }
            //If Parking slot is getting vacated by the user allow him to enter exit
            else if(parkingSlot.IsOccupied && parkUnParkVehicleDtoInput.Status == PARKINGSTATUS.UnParkingVehicle) 
            {
                var lastParkingSlotByUser =  _repo.GetQueryable<ParkingTraffic>(x =>
                    x.ParkingSlotId == parkUnParkVehicleDtoInput.PlarkingSlotId &&
                    x.UserProfileId == parkUnParkVehicleDtoInput.UserProfileId && 
                    x.IsActive == true
                ).OrderBy(x => x.Id).LastOrDefault();
                if(lastParkingSlotByUser != null)
                {
                    lastParkingSlotByUser.OutTime = _genericServices.GetCurrentTime();
                    _repo.Save();
                }
                return new ParkingSlotUpdateStatus()
                {
                    Success = true,
                    ValidationMessage = ParkingSlotUpdateStatusResponse.PARK_VACATED.ToString()
                };
            }

            return new ParkingSlotUpdateStatus()
            {
                Success = false,
                ValidationMessage = ParkingSlotUpdateStatusResponse.RECORD_NOT_UPDATED.ToString()
            };
        }
    }
}
