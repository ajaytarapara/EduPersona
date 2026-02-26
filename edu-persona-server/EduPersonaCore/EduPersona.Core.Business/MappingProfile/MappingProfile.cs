using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Shared.Models.Request;

namespace EduPersona.Core.Business.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfileRequest, UserProfile>()
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
            .ForMember(d => d.Birthdate, o => o.MapFrom(s => s.Birthdate))
            .ForMember(d => d.PhoneNo, o => o.MapFrom(s => s.PhoneNo));

            CreateMap<UserProfileRequest, UserDesignation>()
           .ForMember(d => d.CurrentDesignationId, o => o.MapFrom(s => s.CurrentDesignationId))
           .ForMember(d => d.TargetDesignationId, o => o.MapFrom(s => s.TargetDesignationId))
           .ForMember(d => d.IsActive, o => o.MapFrom(_ => true));
        }
    }
}