//---------------------------------------------------------------------------------------
// Description: Contains API related to parking traffic details
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
using LtsParkingAppApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LtsParkingAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ParkingTraffic")]
    public class ParkingTrafficController : Controller
    {
        private readonly IParkingTrafficServices _parkingTrafficServices;
        private readonly IMapper _mapper;

        public ParkingTrafficController(IParkingTrafficServices parkingTrafficServices,
            IMapper mapper)
        {
            _parkingTrafficServices = parkingTrafficServices;
            _mapper = mapper;

        }

        
        /// <summary>
        /// retrieve specific parking traffic details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "ParkingTraffic")]
        public IActionResult ParkingTraffic(int id)
        {
            var parkingTrafic = _parkingTrafficServices.Get(id).Result;
            return Ok(parkingTrafic);
        }

        /// <summary>
        /// retrieve parking traffic details for the vehicle
        /// </summary>
        /// <param name="vehicleNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("VehicleParkingTraffic/{vehicleNo}")]
        public IActionResult VehicleParkingTraffic(int vehicleNo)
        {
            var vehicleParkingTrafic = _parkingTrafficServices.GetVehicleTraffic(vehicleNo).Result;
            return Ok(vehicleParkingTrafic);
        }

        /// <summary>
        /// retrieve parking traffic details for the current date
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TodaysParkingTraffic/{location}")]
        public IActionResult TodaysParkingTraffic(string location)
        {
            var vehicleParkingTrafic = _parkingTrafficServices.TodaysParkingTraffic(location).Result;
            return Ok(vehicleParkingTrafic);
        }

        [HttpGet]
        [Route("Report/{fromDate}/{toDate}/{locationId}")]
        public IActionResult ParkingTrafficReport(DateTime fromDate,DateTime toDate,int locationId)
        {
            var vehicleParkingTrafic = _parkingTrafficServices.ParkingTrafficReport(fromDate,toDate,locationId).Result;
            return Ok(vehicleParkingTrafic);
        }


        /// <summary>
        /// update parking traffic details
        /// </summary>
        /// <param name="parkingTrafficViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post([FromBody]ParkingTrafficViewModel  parkingTrafficViewModel)
        {
            return _parkingTrafficServices.Create(_mapper.Map<ParkingTrafficDtoInput>(parkingTrafficViewModel)).Result;
        }

        /// <summary>
        /// insert new parkign traffic details
        /// </summary>
        /// <param name="parkingTrafficViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Put([FromBody]ParkingTrafficViewModel parkingTrafficViewModel)
        {
            return _parkingTrafficServices.Update(_mapper.Map<ParkingTrafficDtoInput>(parkingTrafficViewModel)).Result;
        }

        [HttpGet]
        [Route("ParkingLocations")]
        public IActionResult ParkingLocations()
        {
            return Ok(_parkingTrafficServices.ParkingLocations().Result);
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
