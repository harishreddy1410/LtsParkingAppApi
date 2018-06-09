//---------------------------------------------------------------------------------------
// Description: Contains API related to user parking area
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

    public class ParkingAreaController : Controller
    {
        private readonly IParkingAreaService _parkingAreaService;
        private readonly IMapper _mapper;

        public ParkingAreaController(IParkingAreaService parkingAreaService, IMapper mapper)
        {
            _parkingAreaService = parkingAreaService;
            _mapper = mapper;
        }

        [Route("GetParkingLocations")]
        public IActionResult GetParkingLocations()
        {
            return Ok(_mapper.Map<List<LocationViewModel>>(_parkingAreaService.GetParkingLocations().Result));
        }

        [Route("GetLocationCompanies/{locationId}")]
        public IActionResult GetLocationCompanies(int locationId)
        {
            return Ok(_mapper.Map<List<CompanyViewModel>>(_parkingAreaService.GetCompaniesAtLocation(locationId).Result));
        }

        [Route("GetLocationParkingDivisions/{locationId}")]
        public IActionResult GetLocationParkingDivisions(int locationId)
        {
            return Ok(_mapper.Map<List<ParkingDivisionViewModel>>(_parkingAreaService.GetParkingDivisionsAtLocation(locationId).Result));
        }

    }
}