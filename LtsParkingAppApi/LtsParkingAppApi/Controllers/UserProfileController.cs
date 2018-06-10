//---------------------------------------------------------------------------------------
// Description: Contains API related to user profiles
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LtsParkingAppApi.Controllers
{
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileServices _userprofileServices;

        public UserProfileController(IUserProfileServices userProfileServices)
        {
            _userprofileServices = userProfileServices;
        }

        
        /// <summary>
        /// retrieve user profile for the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ObjectResult UserProfile(int id = 0)
        {
            var data = _userprofileServices.Get(id).Result;
            return Ok(data);
        }

        /// <summary>
        /// retrieve user profile for the email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("ByEmail/{email}")]
        public ObjectResult UserProfile(string email = "")
        {
            var data = _userprofileServices.Get(email:email).Result;
            return Ok(data);
        }

        /// <summary>
        /// update user profile data
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// insert new user profile data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// delete user profile for the id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// retrieve parking slots based on the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserWithParkingArea/{userId}")]
        public IActionResult GetUserWithParkingArea(int userId)
        {
            try
            {
                var _slots = (_userprofileServices.GetUserWithParkingArea(userId).Result, new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented });
                return Ok(_slots);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
