﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppServices.UserService;
using Microsoft.AspNetCore.Mvc;

namespace LtsParkingAppApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IUserProfileServices _userProfileServices;
        public ValuesController(IUserProfileServices userProfileServices)
        {
            _userProfileServices = userProfileServices;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                return Json(_userProfileServices.Get(id: 2));
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
