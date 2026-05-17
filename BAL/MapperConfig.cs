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
            cfg.CreateMap<RegisterDTO, User>();
               
            cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
            cfg.CreateMap<Food, FoodDTO>();
           
            cfg.CreateMap<FoodDTO, Food>();
    
            cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            cfg.CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            //cfg.CreateMap<Notification, NotificationDTO>().ReverseMap();

        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}