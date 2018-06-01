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
        // GET: api/ParkingTraffic
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/ParkingTraffic/5
        [HttpGet("{id}", Name = "ParkingTraffic")]
        public IActionResult ParkingTraffic(int id)
        {
            var parkingTrafic = _parkingTrafficServices.Get(id).Result;
            return Ok(parkingTrafic);
        }

        // GET: https://localhost:44391/api/parkingtraffic/vehicleparkingtraffic/1
        [HttpGet]
        [Route("VehicleParkingTraffic/{vehicleNo}")]
        public IActionResult VehicleParkingTraffic(int vehicleNo)
        {
            var vehicleParkingTrafic = _parkingTrafficServices.GetVehicleTraffic(vehicleNo).Result;
            return Ok(vehicleParkingTrafic);
        }

        // GET: /api/parkingtraffic/TodaysParkingTraffic/blr
        [HttpGet]
        [Route("TodaysParkingTraffic/{location}")]
        public IActionResult TodaysParkingTraffic(string location)
        {
            var vehicleParkingTrafic = _parkingTrafficServices.TodaysParkingTraffic(location).Result;
            return Ok(vehicleParkingTrafic);
        }


        // POST: api/ParkingTraffic
        [HttpPost]
        public bool Post([FromBody]ParkingTrafficViewModel  parkingTrafficViewModel)
        {
            return _parkingTrafficServices.Create(_mapper.Map<ParkingTrafficDtoInput>(parkingTrafficViewModel)).Result;
        }

        // POST: api/ParkingTraffic
        [HttpPut]
        public bool Put([FromBody]ParkingTrafficViewModel parkingTrafficViewModel)
        {
            return _parkingTrafficServices.Update(_mapper.Map<ParkingTrafficDtoInput>(parkingTrafficViewModel)).Result;
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
