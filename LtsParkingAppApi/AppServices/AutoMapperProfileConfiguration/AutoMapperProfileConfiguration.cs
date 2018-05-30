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
            CreateMap<UserProfile, UserProfileDtoInput>();
            CreateMap<UserProfile, UserProfileDtoOutput>();
            base.Configure();
        }
    }
}
