using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System.Collections.Generic;

namespace BAL.Services
{
    public class FoodService
    {
        FoodRepo repo;
        Mapper mapper;

        public FoodService(FoodRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<FoodDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<FoodDTO>>(data);
        }

        public FoodDTO? Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<FoodDTO>(data);
        }

        public List<FoodDTO> GetByCategory(int categoryId)
        {
            var data = repo.GetByCategory(categoryId);
            return mapper.Map<List<FoodDTO>>(data);
        }

        public bool Create(FoodDTO dto)
        {
            var converted = mapper.Map<Food>(dto);
            return repo.Create(converted);
        }

        public bool Update(FoodDTO dto)
        {
            var converted = mapper.Map<Food>(dto);
            return repo.Update(converted);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        public List<FoodDTO> Search(string keyword)
        {
            var data = repo.Search(keyword);
            return mapper.Map<List<FoodDTO>>(data);
        }
    }
}