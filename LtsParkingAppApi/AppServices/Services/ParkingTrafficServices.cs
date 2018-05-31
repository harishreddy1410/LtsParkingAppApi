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
    public class ParkingTrafficServices:IParkingTrafficServices
    {
        IRepository _repo;
        IMapper _mapper;

        public ParkingTrafficServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
    }
}
