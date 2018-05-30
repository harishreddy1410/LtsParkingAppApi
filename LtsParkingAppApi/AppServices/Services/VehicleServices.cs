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
    public class VehicleServices : IVehicleServices
    {
        IRepository _repo;
        IMapper _mapper;

        public VehicleServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> Create(VehicleDtoInput vehicleDtoInput)
        {
            try
            {
                _repo.Create<Vehicle>(_mapper.Map<Vehicle>(vehicleDtoInput), "API");
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
            var toBeDeleted = _repo.GetById<Vehicle>(id);
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

        public Task<List<VehicleDtoOutput>> GetAll(bool includeInactive)
        {
            return Task.FromResult(_mapper.Map<List<VehicleDtoOutput>>(_repo.GetQueryable<Vehicle>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));
        }

        public Task<VehicleDtoOutput> Get(int id = 0)
        {
            try
            {
                return Task.FromResult(_mapper.Map<VehicleDtoOutput>(_repo.Get<Vehicle>(x => x.Id == id)));
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public Task<bool> Update(VehicleDtoInput vehicleDtoInput)
        {
            var updated = _mapper.Map<Vehicle>(vehicleDtoInput);
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
