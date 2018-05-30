using AppDomain.Models;
using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.UserService
{
    public interface IUserProfileServices
    {
        Task<UserProfileDtoOutput> Get(int id = 0, string email = "");

        Task<bool> Create(UserProfileDtoInput userProfileDtoInput);

        Task<bool> Update(UserProfileDtoInput userProfileDtoInput);

        Task<bool> Delete(int id, int? DeletedBy);

        Task<List<UserProfileDtoOutput>> GetAll(bool includeInactive);

    }
}
