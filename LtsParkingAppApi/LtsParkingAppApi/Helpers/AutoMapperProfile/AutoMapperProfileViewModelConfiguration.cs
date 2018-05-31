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
        }
    }
}
