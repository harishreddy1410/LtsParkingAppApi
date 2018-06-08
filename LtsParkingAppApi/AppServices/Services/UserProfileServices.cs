//---------------------------------------------------------------------------------------
// Description: crud oprations for the user profiles
//---------------------------------------------------------------------------------------
using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServices.UserService
{
    public class UserProfileServices : IUserProfileServices
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public UserProfileServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// create new user profile
        /// </summary>
        /// <param name="userProfileDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Create(UserProfileDtoInput userProfileDtoInput)
        {
            try
            {
                _repo.Create<UserProfile>(_mapper.Map<UserProfile>(userProfileDtoInput), "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// delete any specific user profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="DeletedBy"></param>
        /// <returns></returns>
        public Task<bool> Delete(int id, int? DeletedBy)
        {
            try
            {
                var toBeDeleted = _repo.GetById<UserProfile>(id);
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
        /// return all the user profiles
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public Task<List<UserProfileDtoOutput>> GetAll(bool includeInactive)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<UserProfileDtoOutput>>(_repo.GetQueryable<UserProfile>(x => x.IsActive == (includeInactive == false ? true : x.IsActive) && x.IsDeleted == false)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return any specific user profile for id or email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<UserProfileDtoOutput> Get(int id = 0, string email = "")
        {
            try
            {
                if (id > 0)
                    return Task.FromResult(_mapper.Map<UserProfileDtoOutput>(_repo.Get<UserProfile>(x => x.Id == id).FirstOrDefault()));
                else
                    return Task.FromResult(_mapper.Map<UserProfileDtoOutput>(_repo.Get<UserProfile>(x => x.Email == email).FirstOrDefault()));
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// update the user profiel
        /// </summary>
        /// <param name="userProfileDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Update(UserProfileDtoInput userProfileDtoInput)
        {
            try
            {
                var updated = _mapper.Map<UserProfile>(userProfileDtoInput);
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return the parking slots for the specific user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<UserProfile> GetUserWithParkingArea(int userId)
        {
            try
            {
                var userProfile = _repo.GetQueryable<UserProfile>(x => x.Id == userId && x.IsActive == true && x.IsDeleted == false,
                                                                    null,
                                                                    null,
                                                                    1,
                                                                    y => y.Location).FirstOrDefault();
                if (userProfile != null)
                {
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
            catch (Exception)
            {
                throw;
            }
        }

    }
}
