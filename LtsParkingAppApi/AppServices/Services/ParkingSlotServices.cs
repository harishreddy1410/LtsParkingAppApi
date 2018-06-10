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
                                                                                  .ToList();
                divisions.ForEach(x =>
                {
                    x.ParkingSlots = x.ParkingSlots.Where(z => z.IsActive == true).ToList();
                });
                return Task.FromResult(_mapper.Map<List<ParkingDivisionDtoOutput>>(divisions));
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

                }).AsNoTracking()
                .ToList();
            
            if (slotDetail.Count() > 0 && slotDetail.First().IsOccupied)
            {
             var userOccupiedSlot = _repo.GetQueryable<ParkingTraffic>(x => x.ParkingSlotId == slotDetail.First().Id
             && (x.CreatedDate - DateTime.Now).Days <= 1 && x.IsActive == true && x.IsDeleted == false, 
                 null, null, null, y => y.UserProfile)
                 .AsNoTracking();
                if(userOccupiedSlot.Count() > 0)
                {
                    slotDetail.First().OccupiedBy = userOccupiedSlot.First().UserProfile.FirstName;
                    slotDetail.First().InTime = userOccupiedSlot.First().InTime;
                    slotDetail.First().OutTime = userOccupiedSlot.First().OutTime;
                }
            }

            return Task.FromResult(slotDetail.FirstOrDefault());

        }

    }
}
