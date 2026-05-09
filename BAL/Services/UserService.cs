using AutoMapper;
using BAL.DTOs;
using BLL;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Services
{
    public class UserService
    {
        UserRepo repo;
        Mapper mapper;

        public UserService(UserRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<UserDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<UserDTO>>(data);
            return res;
        }

        public UserDTO Get(int id)
        {
            var data = repo.Get(id);
            var res = mapper.Map<UserDTO>(data);
            return res;
        }

        public bool Create(UserDTO u)
        {
            var converted = mapper.Map<User>(u);
            return repo.Create(converted);
        }

        public bool Update(UserDTO u)
        {
            var converted = mapper.Map<User>(u);
            return repo.Update(converted);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}