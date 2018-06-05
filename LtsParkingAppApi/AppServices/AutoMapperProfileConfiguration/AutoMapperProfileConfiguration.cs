using AppDomain.Models;
using AppServices.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.AutoMapperProfileConfiguration
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserProfile, UserProfileDtoInput>().ReverseMap();
            CreateMap<UserProfile, UserProfileDtoOutput>().ReverseMap();

            CreateMap<Vehicle, VehicleDtoInput>().ReverseMap();
            CreateMap<Vehicle, VehicleDtoOutput>().ReverseMap();

            CreateMap<EmployeeShift, EmployeeShiftDtoInput>().ReverseMap();
            CreateMap<EmployeeShift, EmployeeShiftDtoOutput>().ReverseMap();

            CreateMap<ParkingSlot, ParkingSlotDtoInput>().ReverseMap();
            CreateMap<ParkingSlot, ParkingSlotDtoOutput>().ReverseMap();
            CreateMap<ParkingSlot, UpdateParkingSlotDtoInput>().ReverseMap();


            CreateMap<ParkingTraffic, ParkingTrafficDtoInput>().ReverseMap();
            CreateMap<ParkingTraffic, ParkingTrafficDtoOutput>().ReverseMap();

            CreateMap<ParkingDivision, ParkingDivisionDtoOutput>().ReverseMap();
            base.Configure();
        }
    }
}
