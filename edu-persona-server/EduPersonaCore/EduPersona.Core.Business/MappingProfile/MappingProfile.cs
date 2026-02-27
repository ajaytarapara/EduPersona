using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Shared.Models.Request;
using EduPersona.Core.Shared.Models.Response;

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

            CreateMap<UserProfile, UserProfileResponse>();

            // UserDesignation → ProfileVersionResponse
            CreateMap<UserDesignation, ProfileVersionResponse>()
            .ForMember(
                d => d.DesignationVersionId,
                o => o.MapFrom(s => s.Id)
            )
            .ForMember(
                d => d.ProfessionId,
                o => o.MapFrom(s => s.Profession != null ? s.Profession.Name : string.Empty)
            )
            .ForMember(
                d => d.CurrentDesignation,
                o => o.MapFrom(s => s.CurrentDesignation != null ? s.CurrentDesignation.Name : string.Empty)
            )
            .ForMember(
                d => d.TargetDesignation,
                o => o.MapFrom(s => s.TargetDesignation != null ? s.TargetDesignation.Name : string.Empty)
            )
            .ForMember(
                d => d.Skills,
                o => o.Ignore() // handled manually
            );
        }
    }
}