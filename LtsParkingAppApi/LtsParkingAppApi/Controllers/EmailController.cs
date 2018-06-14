//---------------------------------------------------------------------------------------
// Description: api used for sending emails
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper.Mappers;
using LtsParkingAppApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LtsParkingAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Email")]
    public class EmailController : Controller
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly IEmailServices _emailServices;
        private IConfiguration _config;

        public EmailController(AutoMapper.IMapper mapper,IEmailServices emailServices, IConfiguration config)
        {
            _mapper = mapper;
            _emailServices = emailServices;
            _config = config;
        }

        /// <summary>
        /// api used to send emails
        /// </summary>
        /// <param name="emailDetails"></param>
        [HttpPost]
        public void Email([FromBody] EmailViewModel emailDetails)
        {
            try
            {
                _emailServices.SendEmailAsync(_mapper.Map<EmailDtoInput>(emailDetails),_config);                
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}