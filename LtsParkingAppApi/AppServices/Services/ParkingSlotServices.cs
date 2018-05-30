using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class ParkingSlotServices : IParkingSlotServices
    {
        IRepository _repo;
        IMapper _mapper;

        public ParkingSlotServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> Create(ParkingSlotDtoInput parkingSlotDtoInput)
        {
            try
            {
                _repo.Create<ParkingSlot>(_mapper.Map<ParkingSlot>(parkingSlotDtoInput), "API");
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
            var toBeDeleted = _repo.GetById<ParkingSlot>(id);
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

        public Task<List<ParkingSlotDtoOutput>> GetAll(bool includeInactive)
        {
            return Task.FromResult(_mapper.Map<List<ParkingSlotDtoOutput>>(_repo.GetQueryable<ParkingSlot>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));
        }

        public Task<ParkingSlotDtoOutput> Get(int id = 0)
        {
            try
            {
                return Task.FromResult(_mapper.Map<ParkingSlotDtoOutput>(_repo.Get<ParkingSlot>(x => x.Id == id)));
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task<bool> Update(ParkingSlotDtoInput parkingSlotDtoInput)
        {
            var updated = _mapper.Map<ParkingSlot>(parkingSlotDtoInput);
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
    }
}
