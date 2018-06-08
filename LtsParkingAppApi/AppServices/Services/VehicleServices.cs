//---------------------------------------------------------------------------------------
// Description: crud operations for the vehicles
//---------------------------------------------------------------------------------------
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
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public VehicleServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// create new vehicle
        /// </summary>
        /// <param name="vehicleDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Create(VehicleDtoInput vehicleDtoInput)
        {
            try
            {
                _repo.Create<Vehicle>(_mapper.Map<Vehicle>(vehicleDtoInput), "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// delete the vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DeletedBy"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return all the vehicles
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public Task<List<VehicleDtoOutput>> GetAll(bool includeInactive)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<VehicleDtoOutput>>(_repo.GetQueryable<Vehicle>(x => x.IsActive == (includeInactive == false ? true : x.IsActive) && x.IsDeleted == false)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return any specific vehicle from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// update the vehicle details
        /// </summary>
        /// <param name="vehicleDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Update(VehicleDtoInput vehicleDtoInput)
        {
            var updated = _mapper.Map<Vehicle>(vehicleDtoInput);
            try
            {
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
