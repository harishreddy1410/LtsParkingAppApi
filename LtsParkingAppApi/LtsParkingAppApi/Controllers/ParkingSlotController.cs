//---------------------------------------------------------------------------------------
// Description: Contains API related to user vehicles
//---------------------------------------------------------------------------------------
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

        /// <summary>
        /// return all the parking slots
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// return specific parking slot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// return specific parking slot with user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetparkingSlotDetails/{id}")]
        public IActionResult GetparkingSlotDetails(int id)
        {
            try
            {
                var _slot = _parkingSlotServices.GetParkingSlotDetail(id).Result;
                return Ok(_slot);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// update the parking slot details
        /// </summary>
        /// <param name="updateParkingSlotViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// create new parking slot
        /// </summary>
        /// <param name="parkingSlotViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ParkingSlot([FromBody]ParkingSlotViewModel parkingSlotViewModel)
        {
            return Ok( _parkingSlotServices.Create(_mapper.Map<ParkingSlotDtoInput>(parkingSlotViewModel)).Result);            
        }

        /// <summary>
        /// delete the parking slot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public string Delete(int id)
        {
            return "parking slot marked deleted";
           //return _parkingSlotServices.Delete(id:id,deletedBy:null).Result;
        }

        /// <summary>
        /// return parking slots for the location id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLocationParkingSlots/{locationId}")]
        public IActionResult GetLocationParkingSlots(int locationId)
        {
            var _slots = _mapper.Map<List<ParkingDivisionViewModel>>(_parkingSlotServices.GetParkingLocation(locationId).Result);
            return Ok(_slots);
        }


        /// <summary>
        /// Use to occupy or unoccpy the parking slot by user  
        /// </summary>
        /// <param name="parkingTrafficViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("OccupyUnoccupySlot")]
        public IActionResult OccupyUnoccupySlot([FromBody]UpdateParkingSlotDtoInput parkingSlotViewModel)
        {
            return Ok(_parkingSlotServices.OccupyUnoccupySlot(_mapper.Map<UpdateParkingSlotDtoInput>(parkingSlotViewModel)).Result);
        }
    }
}