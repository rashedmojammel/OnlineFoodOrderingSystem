using AutoMapper;
using BAL.DTOs;
using BAL.DTOs.BAL.DTOs;
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
            cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
            cfg.CreateMap<Food, FoodDTO>()
               .ForMember(dest => dest.CategoryName,
                          opt => opt.MapFrom(src => src.Category != null
                                             ? src.Category.Name : ""));
            cfg.CreateMap<FoodDTO, Food>()
               .ForMember(dest => dest.Category, opt => opt.Ignore());
            cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            cfg.CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}