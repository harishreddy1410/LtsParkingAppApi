using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class ParkingAreaService : IParkingAreaService
    {
        IRepository _repo;
        IMapper _mapper;
        public ParkingAreaService(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        public Task<List<LocationDtoOutput>> GetParkingLocations()
        {
            return Task.FromResult<List<LocationDtoOutput>>(_repo.GetQueryable<Location>(x => x.IsActive == true 
                                            && x.IsDeleted == false)
                                            .AsNoTracking()
                                            .Select(y => new LocationDtoOutput()
                                                                        {
                                                                            Id = y.Id,
                                                                            Name = y.Name
                                                                        }).ToList());
        }

        public Task<List<CompanyDtoOutput>> GetCompaniesAtLocation(int locationId)
        {

            var companies =  _mapper.Map<List<CompanyDtoOutput>>( _repo.GetQueryable<Location>(x => x.IsActive == true
                                                                    && x.IsDeleted == false
                                                                    && x.Id == locationId,
                                                                    null,
                                                                    null,
                                                                    null,
                                                                    y => y.Companies
                                                                    )                                                                    
                                                                    .SelectMany(x => x.Companies).ToList());
            return Task.FromResult(companies);
                                                                    
        }



        public Task<List<ParkingDivisionDtoOutput>> GetParkingDivisionsAtLocation(int locationId)
        {
            return Task.FromResult(
                _repo.GetQueryable<ParkingDivision>(x => x.IsActive == true && x.IsDeleted == false && x.LocationId == locationId)
              
                                        .AsNoTracking()
                                        .Select(x => new ParkingDivisionDtoOutput() {
                                             Id = x.Id,
                                             Name = x.Name,
                                             LocationId = x.LocationId,
                                             SequenceOrder = x.SequenceOrder
                                        })
                                        .ToList());
        }

    }
}
