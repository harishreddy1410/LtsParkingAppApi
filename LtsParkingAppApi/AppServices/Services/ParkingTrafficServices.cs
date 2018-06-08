//---------------------------------------------------------------------------------------
// Description: crud oprations for the parking traffic
//---------------------------------------------------------------------------------------
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
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        private readonly IParkingSlotServices _parkingSlotServices;
        private readonly IGenericServices _genericServices;

        public ParkingTrafficServices(IRepository repo, IMapper mapper, IParkingSlotServices parkingSlotServices,
            IGenericServices genericServices)
        {
            _repo = repo;
            _mapper = mapper;
            _parkingSlotServices = parkingSlotServices;
            _genericServices = genericServices;
        }

        /// <summary>
        /// create new parking traffic entry
        /// </summary>
        /// <param name="parkingTrafficDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Create(ParkingTrafficDtoInput parkingTrafficDtoInput)
        {
            try
            {
                _repo.Create<ParkingTraffic>(_mapper.Map<ParkingTraffic>(parkingTrafficDtoInput), "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// delete the parking traffic entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DeletedBy"></param>
        /// <returns></returns>
        public Task<bool> Delete(int id, int? DeletedBy)
        {
            try
            {
                var toBeDeleted = _repo.GetById<ParkingTraffic>(id);
                toBeDeleted.IsDeleted = true;
                toBeDeleted.IsActive = false;
                toBeDeleted.ModifiedBy = DeletedBy;
                _repo.Update(toBeDeleted);
                return Task.FromResult(true);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get all parking traffic entries
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public Task<List<ParkingTrafficDtoOutput>> GetAll(bool includeInactive)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<ParkingTrafficDtoOutput>>(_repo.GetQueryable<ParkingTraffic>(x => x.IsActive == (includeInactive == false ? true : x.IsActive) && x.IsDeleted == false)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get any specific parking traffic entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// update the parking traffic entry
        /// </summary>
        /// <param name="parkingTrafficDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Update(ParkingTrafficDtoInput parkingTrafficDtoInput)
        {
            try
            {
                var updated = _mapper.Map<ParkingTraffic>(parkingTrafficDtoInput);
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get traffic details for the vehicle id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public Task<List<ParkingTrafficDtoOutput>> GetVehicleTraffic(int vehicleId = 0)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<ParkingTrafficDtoOutput>>(_repo.Get<ParkingTraffic>(x => x.VehicleId == vehicleId)));
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// return parking traffic details for the today's date
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Task<List<ParkingTrafficDtoOutput>> TodaysParkingTraffic(string location)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<ParkingTrafficDtoOutput>>(_repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlot.Location == location && x.CreatedDate.Date == DateTime.Today)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// used to occupy the parking slot
        /// </summary>
        /// <param name="parkUnParkVehicleDtoInput"></param>
        /// <returns></returns>
        public ParkingSlotUpdateStatus ParkVehicle(ParkUnParkVehicleDtoInput parkUnParkVehicleDtoInput)
        {
            try
            {
                var parkingSlot = _repo.GetById<ParkingSlot>(parkUnParkVehicleDtoInput.PlarkingSlotId);
                var parkingUser = _repo.GetQueryable<UserProfile>(x => x.Id == parkUnParkVehicleDtoInput.UserProfileId,
                    null,
                    null,
                    null,
                    y => y.EmployeeShift
                ).AsNoTracking().FirstOrDefault();
                if (parkingSlot == null)
                {
                    return new ParkingSlotUpdateStatus()
                    {
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
                    _repo.Create<ParkingTraffic>(new ParkingTraffic()
                    {
                        ParkingSlotId = parkUnParkVehicleDtoInput.PlarkingSlotId,
                        UserProfileId = parkUnParkVehicleDtoInput.UserProfileId,
                        IsActive = true,
                        VehicleId = parkUnParkVehicleDtoInput.VehicleId,
                        CreatedBy = parkUnParkVehicleDtoInput.UserProfileId,
                        InTime = _genericServices.GetCurrentTime(),
                        OutTime = parkingUser.EmployeeShift.ShiftEndTime
                    });
                    _repo.Save();

                    return new ParkingSlotUpdateStatus()
                    {
                        Success = true,
                        ValidationMessage = ParkingSlotUpdateStatusResponse.PARK_SUCCESS.ToString()
                    };
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
                else if (parkingSlot.IsOccupied && parkUnParkVehicleDtoInput.Status == PARKINGSTATUS.UnParkingVehicle)
                {
                    var lastParkingSlotByUser = _repo.GetQueryable<ParkingTraffic>(x =>
                       x.ParkingSlotId == parkUnParkVehicleDtoInput.PlarkingSlotId &&
                       x.UserProfileId == parkUnParkVehicleDtoInput.UserProfileId &&
                       x.IsActive == true
                    ).OrderBy(x => x.Id).LastOrDefault();
                    if (lastParkingSlotByUser != null)
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
            catch (Exception)
            {
                throw;
            }
            
            
        }
    }
}
