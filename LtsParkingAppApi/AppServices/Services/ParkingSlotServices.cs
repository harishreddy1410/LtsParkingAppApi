//---------------------------------------------------------------------------------------
// Description: crud oprations for the parking slots
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
    public class ParkingSlotServices : IParkingSlotServices
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
       
        public ParkingSlotServices(IRepository repo, IMapper mapper)
        {          
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// create new parking slot
        /// </summary>
        /// <param name="parkingSlotDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Create(ParkingSlotDtoInput parkingSlotDtoInput)
        {
            try
            {
                _repo.Create<ParkingSlot>(_mapper.Map<ParkingSlot>(parkingSlotDtoInput), "API");
                _repo.Save();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// delete any specific parking slot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedBy"></param>
        /// <returns></returns>
        public Task<bool> Delete(int id, int? deletedBy)
        {
            try
            {
                var toBeDeleted = _repo.GetById<ParkingSlot>(id);
                toBeDeleted.IsDeleted = true;
                toBeDeleted.IsActive = false;
                toBeDeleted.ModifiedBy = deletedBy;
                _repo.Update(toBeDeleted);
                return Task.FromResult(true);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return all parking slots
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public Task<List<ParkingSlotDtoOutput>> GetAll(bool includeInactive)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<ParkingSlotDtoOutput>>(_repo.GetQueryable<ParkingSlot>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return any specific parking slot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ParkingSlotDtoOutput> Get(int id = 0)
        {
            try
            {
                return Task.FromResult(_mapper.Map<ParkingSlotDtoOutput>(_repo.GetById<ParkingSlot>(id)));
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// update the parking slot
        /// </summary>
        /// <param name="parkingSlotDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Update(UpdateParkingSlotDtoInput parkingSlotDtoInput)
        {            
            try
            {
                var updated = _repo.GetById<ParkingSlot>(parkingSlotDtoInput.Id);
                if (updated != null)
                {
                    updated.IsOccupied = parkingSlotDtoInput.IsOccupied;
                    updated.ModifiedDate = DateTime.Now;
                    updated.ModifiedBy = 1;
                    _repo.Save();
                }
                
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return parking divisions with its active parking slots for the location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Task<List<ParkingDivisionDtoOutput>> GetParkingLocation(int locationId)
        {
            try
            {
                var divisions = _repo.GetQueryable<ParkingDivision>(
                    x => x.LocationId == locationId && x.IsActive == true && x.IsDeleted == false,
                                                                                    null,
                                                                                    null,
                                                                                    null,
                                                                                    z => z.ParkingSlots
                                                                                  )
                                                                                  .AsNoTracking()
                                                                                  .ToList();
                divisions.ForEach(x =>
                {
                    x.ParkingSlots = x.ParkingSlots.Where(z => z.IsActive == true).ToList();
                });

                var divisionsDto = _mapper.Map<List<ParkingDivisionDtoOutput>>(divisions);
                divisionsDto.SelectMany(x => x.ParkingSlots)
                    .ToList()
                    .ForEach(s =>
                    {
                        if (s.IsOccupied)
                        {
                            s.SlotOccupiedByUserId = _repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlotId == s.Id 
                            //&& (x.CreatedDate - DateTime.Now).Hours <= 24 
                            && x.IsExpired == false && x.IsActive == true && x.IsDeleted == false,null, null, null, y => y.UserProfile)
                                .OrderByDescending(x => x.Id)
                                .AsNoTracking()
                                .FirstOrDefault()?.UserProfileId;
                        }
                    });
                return Task.FromResult(divisionsDto);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public Task<ParkingSlotDetailOutput> GetParkingSlotDetail(int slotId)
        {
            var slotDetail = _repo.GetQueryable<ParkingSlot>(x => x.Id == slotId, null, null, 1, 
                y => y.Company)
                .Select(y => new ParkingSlotDetailOutput() {
                    CompanyName = y.Company.Name,
                    Id = y.Id,
                    IsOccupied = y.IsOccupied,
                    Type = y.Type.ToString()
                })
                .AsNoTracking()
                .FirstOrDefault();
            
            if (slotDetail != null && slotDetail.IsOccupied)
            {
                var userOccupiedSlot = _repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlotId == slotDetail.Id
                 && x.IsExpired == false && x.IsActive == true && x.IsDeleted == false, 
                    null, null, null, y => y.UserProfile)
                    .OrderByDescending(x=>x.Id)
                    .AsNoTracking()
                    .FirstOrDefault();

                if(userOccupiedSlot != null)
                {
                    slotDetail.OccupiedBy = userOccupiedSlot.UserProfile.FirstName + " " + userOccupiedSlot.UserProfile.LastName;
                    slotDetail.InTime = userOccupiedSlot.InTime;
                    slotDetail.OutTime = userOccupiedSlot.OutTime;
                    slotDetail.SlotOccupiedByUserId = userOccupiedSlot.UserProfileId;
                }
            }

            return Task.FromResult(slotDetail);

        }


        public Task<string> OccupyUnoccupySlot(UpdateParkingSlotDtoInput updateParkingSlotDtoInput)
        {
            if(updateParkingSlotDtoInput != null)
            {                
                var slot = _repo.GetById<ParkingSlot>(updateParkingSlotDtoInput.Id);

                //Check if user is releasing the occupied slot 
                var occupiedSlot = _repo.GetQueryable<ParkingTraffic>(x => x.UserProfileId == updateParkingSlotDtoInput.UserId 
                && !updateParkingSlotDtoInput.IsOccupied && x.IsExpired == false && 
                x.ParkingSlotId == updateParkingSlotDtoInput.Id,null,null,1,y=>y.ParkingSlot).FirstOrDefault();
                if(occupiedSlot != null)
                {
                    occupiedSlot.IsExpired = true;
                    occupiedSlot.ParkingSlot.IsOccupied = false;
                    occupiedSlot.OutTime = DateTime.Now;
                    _repo.Save();
                    return Task.FromResult( $"You have released slot {occupiedSlot.ParkingSlotId} successfully");
                }

                //Chec if user is trying to occupy more slots 
                var slotOfOccupied = _repo.GetQueryable<ParkingTraffic>(x => x.UserProfileId == updateParkingSlotDtoInput.UserId && x.IsExpired == false, null, null, 1)
                    .AsNoTracking();
                if (slotOfOccupied.Count() > 0)
                {
                    return Task.FromResult($"You have found occupying slot {slotOfOccupied.First().ParkingSlotId } ,please release it  before occupying other slot");
                }
                //Check who is unoccupying slot and unoccpy if user mathches
                if (slot != null && slot.IsOccupied && !updateParkingSlotDtoInput.IsOccupied)
                {
                    var userOccupiedSlot = _repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlotId == slot.Id
                                               && (x.CreatedDate - DateTime.Now).Days <= 1 && x.IsActive == true && x.IsDeleted == false,
                                                   null, null, null, y => y.UserProfile)
                                                   .OrderByDescending(x => x.Id)                                                   
                                                   .FirstOrDefault();

                    if(userOccupiedSlot != null && userOccupiedSlot.UserProfileId == updateParkingSlotDtoInput.UserId)
                    {
                        slot.IsOccupied = false;
                        userOccupiedSlot.OutTime = DateTime.Now;
                        _repo.Save();
                        return Task.FromResult("Slot is released successfully");
                    }
                    else
                    {
                        return Task.FromResult("Slot is occupied by other vehicle owner");
                    }
                }
                //Check in case user has clicked multiple times 
                if (slot != null && slot.IsOccupied && updateParkingSlotDtoInput.IsOccupied)
                {
                    var userOccupiedSlot = _repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlotId == slot.Id
                                               && (x.CreatedDate - DateTime.Now).Days <= 1 && x.IsActive == true && x.IsDeleted == false,
                                                   null, null, null, y => y.UserProfile)
                                                   .OrderByDescending(x => x.Id)
                                                   .FirstOrDefault();
                    if (userOccupiedSlot != null && userOccupiedSlot.UserProfileId == updateParkingSlotDtoInput.UserId)
                    {                       
                        return Task.FromResult("You have already occupied this slot");
                    }                    
                }

                //Check who is occupying parking slot
                if (slot != null && !slot.IsOccupied && updateParkingSlotDtoInput.IsOccupied)
                {
                    slot.IsOccupied = true;
                    _repo.Create<ParkingTraffic>(new ParkingTraffic()
                    {
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = updateParkingSlotDtoInput.UserId,
                        CreatedDate = DateTime.Now,
                        InTime = DateTime.Now,
                        ModifiedBy = null,
                        ModifiedDate = null,
                        ParkingSlotId = updateParkingSlotDtoInput.Id,
                        UserProfileId = updateParkingSlotDtoInput.UserId,
                        OutTime = DateTime.Now.AddHours(9),
                        //Using one user one vehicle based approach
                        VehicleId = _repo.GetQueryable<Vehicle>(x => x.UserProfileId == updateParkingSlotDtoInput.UserId).FirstOrDefault()?.Id ?? 1
                    });
                    _repo.Save();
                    return Task.FromResult($"You have occupied slot {slot.Id} successfully");
                }


            }

            return Task.FromResult("Invalid parking slot data");
        }
    }
}
