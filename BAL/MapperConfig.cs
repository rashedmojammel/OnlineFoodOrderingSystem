using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;

namespace BAL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<RegisterDTO, User>()
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Customer"))
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}