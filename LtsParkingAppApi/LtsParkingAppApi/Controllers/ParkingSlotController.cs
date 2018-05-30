using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppServices.Dto;
using AppServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web;
using AutoMapper;
using LtsParkingAppApi.ViewModels;
using AppServices.Interfaces;

namespace LtsParkingAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ParkingSlotController : Controller
    {
        private readonly IParkingSlotServices _parkingSlotServices;
        private readonly IMapper _mapper;
        public ParkingSlotController(IParkingSlotServices parkingSlotServices, IMapper mapper)
        {
            _parkingSlotServices = parkingSlotServices;
            _mapper = mapper;

        }
        [HttpGet]        
        public IActionResult ParkingSlots()
        {
            try
            {
                var _slots = _parkingSlotServices.GetAll(false).Result;
                return Ok(_slots);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ParkingSlot(int id)
        {
            try
            {
                var _slot = _parkingSlotServices.Get(id).Result;
                return Ok(_slot);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]                
        public IActionResult ParkingSlot([FromBody]UpdateParkingSlotViewModel updateParkingSlot)
        {
            try
            {
                var _slot = _parkingSlotServices.Update( _mapper.Map<UpdateParkingSlotDtoInput>(updateParkingSlot));
                return Ok(_slot);                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}