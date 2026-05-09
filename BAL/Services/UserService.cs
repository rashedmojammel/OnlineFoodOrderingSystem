using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System.Collections.Generic;

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
            return mapper.Map<List<UserDTO>>(data);
        }

        public UserDTO? Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<UserDTO>(data);
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

        public UserDTO? Login(string email, string password)
        {
            var data = repo.Login(email, password);
            if (data == null) return null;
            return mapper.Map<UserDTO>(data);
        }

        public bool Register(RegisterDTO dto)
        {
            if (repo.EmailExists(dto.Email)) return false;
            var user = mapper.Map<User>(dto);
            return repo.Create(user);
        }

        public bool EmailExists(string email)
        {
            return repo.EmailExists(email);
        }
    }
}