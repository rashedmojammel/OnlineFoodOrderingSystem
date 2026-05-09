using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            //cfg.CreateMap<Product, ProductDTO>().ReverseMap();

        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }

    }
}