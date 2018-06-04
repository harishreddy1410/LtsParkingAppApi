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

        [HttpPut]        
        public UpdateParkingSlotViewModel ParkingSlot([FromBody]UpdateParkingSlotViewModel updateParkingSlotViewModel)
        {
            try
            {
                //var _slot = _parkingSlotServices.Update( _mapper.Map<UpdateParkingSlotDtoInput>(updateParkingSlotViewModel));
                //return true;                
                return updateParkingSlotViewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public bool ParkingSlot([FromBody]ParkingSlotViewModel parkingSlotViewModel)
        {
            //return _parkingSlotServices.Create(_mapper.Map<ParkingSlotDtoInput>(parkingSlotViewModel)).Result;
            return true;
        }

        [HttpDelete]
        [Route("{id}")]
        public string Delete(int id)
        {
            return "parking slot marked deleted";
           //return _parkingSlotServices.Delete(id:id,deletedBy:null).Result;
        }
    }
}