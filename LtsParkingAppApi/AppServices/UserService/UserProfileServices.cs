using AppDomain.Models;
using AppDomain.Models.Interfaces;
using System.Linq;

namespace AppServices.UserService
{
    public class UserProfileServices : IUserProfileServices
    { 
        public IRepository _repository;
        public UserProfileServices(IRepository repository)
        {
            _repository = repository;
        }

        public UserProfile GetUser(int id = 0, string email = "")
        {
            if(id > 0)
                return _repository.Get<UserProfile>(x => x.Id == id).FirstOrDefault();
            else
                return _repository.Get<UserProfile>(x => x.Email == email).FirstOrDefault();
        }
    }
}
