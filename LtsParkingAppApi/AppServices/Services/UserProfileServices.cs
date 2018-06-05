using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServices.UserService
{
    public class UserProfileServices : IUserProfileServices
    { 
        IRepository _repo;
        IMapper _mapper;

        public UserProfileServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> Create(UserProfileDtoInput userProfileDtoInput)
        {
            try
            {
                _repo.Create<UserProfile>(_mapper.Map<UserProfile>(userProfileDtoInput), "API");
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
            var toBeDeleted = _repo.GetById<UserProfile>(id);
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
      
        public Task<List<UserProfileDtoOutput>> GetAll(bool includeInactive)
        {
            return Task.FromResult(_mapper.Map<List<UserProfileDtoOutput>>(_repo.GetQueryable<UserProfile>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));
        }

        public Task<UserProfileDtoOutput>Get(int id = 0, string email = "")
        {            
            if (id > 0)
                return Task.FromResult( _mapper.Map<UserProfileDtoOutput>(_repo.Get<UserProfile>(x => x.Id == id).FirstOrDefault()));
            else
                return Task.FromResult(_mapper.Map<UserProfileDtoOutput>(_repo.Get<UserProfile>(x => x.Email == email).FirstOrDefault()));
        }

        public Task<bool> Update(UserProfileDtoInput userProfileDtoInput)
        {
            var updated = _mapper.Map<UserProfile>(userProfileDtoInput);
            try
            {
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                return Task.FromResult(true);
            }
        }

        public Task<UserProfile> GetUserWithParkingArea(int userId)
        {
            var userProfile = _repo.GetQueryable<UserProfile>(x => x.Id == userId && x.IsActive == true && x.IsDeleted == false,
                null,
                null,
                1,
                y => y.Location).FirstOrDefault();
            if (userProfile != null)
            {
                //userProfile.Location.ParkingDivisions.ToList().ForEach(x =>
                //{
                //    x.ParkingSlots = _repo.GetQueryable<ParkingSlot>(y => y.ParkingDivisionId == x.Id).ToList();

                //});
                userProfile.Location.ParkingDivisions = _repo.GetQueryable<ParkingDivision>(x =>
                    x.LocationId == userProfile.LocationId,
                    null,
                    null,
                    null,
                    y => y.ParkingSlots).ToList();
                userProfile.Location.ParkingDivisions.ToList().ForEach(
                    x =>
                        {
                            x.ParkingSlots = _repo.GetQueryable<ParkingSlot>(y => y.ParkingDivisionId == x.Id, null, null, null, z => z.Company).ToList();
                        }
                    );
            }

            return Task.FromResult(userProfile);
        }

    }
}
