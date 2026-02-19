using AutoMapper;
using IdentityProvider.Data.Entities;
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

            CreateMap<User, UserInfo>()
            .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.FirstName + " " + s.LastName))
            .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.Name));

        }
    }
}
