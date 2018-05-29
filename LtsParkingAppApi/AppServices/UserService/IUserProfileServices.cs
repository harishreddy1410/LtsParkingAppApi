using AppDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.UserService
{
    public interface IUserProfileServices
    {
        UserProfile GetUser(int id = 0, string email = "");
    }
}
