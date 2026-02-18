using AutoMapper;
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Helper;
using IdentityProvider.Shared.Models.Request;

namespace IdentityProvider.Business.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>(MemberList.None)
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(_ => 2));

        }
    }
}
