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
            CreateMap<UserProfileRequest, UserProfile>();
            CreateMap<UpdateUserProfileRequest, UserProfile>();

            CreateMap<UserProfileRequest, UserDesignation>()
                .ForMember(d => d.CurrentDesignationId, o => o.MapFrom(s => s.CurrentDesignationId))
                .ForMember(d => d.TargetDesignationId, o => o.MapFrom(s => s.TargetDesignationId))
                .ForMember(d => d.ProfessionId, o => o.MapFrom(s => s.ProfessionId));
        }
    }
}