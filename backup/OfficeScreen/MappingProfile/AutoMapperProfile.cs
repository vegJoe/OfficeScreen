using AutoMapper;
using OfficeScreen.Models.Dtos;
using OfficeScreen.Models.Entities;

namespace OfficeScreen.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ReverseMap();

            CreateMap<Webcomic, ComicDto>().ReverseMap();
        }
    }
}
