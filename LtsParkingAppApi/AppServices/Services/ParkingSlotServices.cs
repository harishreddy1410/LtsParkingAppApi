//---------------------------------------------------------------------------------------
// Description: crud oprations for the parking slots
//---------------------------------------------------------------------------------------
using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
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
        private readonly List<ParkingSlotDtoOutput> parkingSlots = new List<ParkingSlotDtoOutput>();

        public ParkingSlotServices(IRepository repo, IMapper mapper)
        {
            
            parkingSlots.Add(new ParkingSlotDtoOutput()
            {
                Id = 1,
                Name = "1",
                IsOccupied = true,
                SequenceOrder = 1
            });
            parkingSlots.Add(new ParkingSlotDtoOutput()
            {
                Id = 2,
                Name = "2",
                IsOccupied = false,
                SequenceOrder = 2
            });
            parkingSlots.Add(new ParkingSlotDtoOutput()
            {
                Id = 3,
                Name = "3",
                IsOccupied = true,
                SequenceOrder = 3
            });
            parkingSlots.Add(new ParkingSlotDtoOutput()
            {
                Id = 4,
                Name = "4",
                IsOccupied = false,
                SequenceOrder = 4
            });
            parkingSlots.Add(new ParkingSlotDtoOutput()
            {
                Id = 5,
                Name = "5",
                IsOccupied = false,
                SequenceOrder = 5
            });
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
                //return Task.FromResult(_mapper.Map<List<ParkingSlotDtoOutput>>(_repo.GetQueryable<ParkingSlot>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));

                return Task.FromResult(parkingSlots);
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
                return Task.FromResult(parkingSlots.Where(x => x.Id == id).FirstOrDefault());
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
        /// return parking slots for the location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Task<List<ParkingDivisionDtoOutput>> GetParkingLocation(int locationId)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<ParkingDivisionDtoOutput>>(_repo.GetQueryable<ParkingDivision>(x => x.LocationId == locationId && x.IsActive == true && x.IsDeleted == false,
                                                                                    null,
                                                                                    null, 
                                                                                    null, 
                                                                                    y => y.ParkingSlots).ToList()));
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
