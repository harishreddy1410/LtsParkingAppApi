//---------------------------------------------------------------------------------------
// 
// Description: Contains all mapping details betweeen view modela and dto
//---------------------------------------------------------------------------------------
using AppServices.Dto;
using AutoMapper;
using LtsParkingAppApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.Helpers.AutoMapperProfile
{
    public class AutoMapperProfileViewModelConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<UpdateParkingSlotDtoInput, UpdateParkingSlotViewModel>().ReverseMap().MaxDepth(1);
            CreateMap<ParkingTrafficDtoInput, ParkingTrafficViewModel>().ReverseMap();
            CreateMap<ParkingSlotDtoInput, ParkingSlotViewModel>().ReverseMap();
            CreateMap<ParkingDivisionDtoOutput, ParkingDivisionViewModel>().ReverseMap();
            CreateMap<ParkingSlotDtoOutput, ParkingSlotViewModel>().ReverseMap();
            CreateMap<CompanyDtoOutput, CompanyViewModel>().ReverseMap();
            CreateMap<LocationDtoOutput, LocationViewModel>().ReverseMap();

            CreateMap<EmailDtoInput, EmailViewModel>().ReverseMap();
        }
    }
}
