using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppServices.UserService;
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
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ObjectResult UserProfile(int id)
        {
            var data = _userprofileServices.Get(id).Result;
            return Ok(data);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
