using AutoMapper;
using SMSAPI.DTO;
using SMSAPI.Model;

namespace SMSAPI.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ApplicationUser, SignUpDTO>().ReverseMap()
            .ForMember(f => f.UserName, t2 => t2.MapFrom(src => src.Email));
        }
    }
}
