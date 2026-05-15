using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;

namespace BAL.Services
{


    public class CategoryService
    {
        CategoryRepo repo;
        Mapper mapper;

        public CategoryService(CategoryRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<CategoryDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<CategoryDTO>>(data);
        }
        public CategoryDTO ? Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<CategoryDTO>(data);
        }
        public bool Create(CategoryDTO dto)
        {
            var converted = mapper.Map<Category>(dto);
            return repo.Create(converted);
        }
        public bool Update(CategoryDTO dto)
        {
            var converted = mapper.Map<Category>(dto);
                return repo.Update(converted);
        }
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
        public List<CategoryDTO> Search(string keyword)
        {
            var data = repo.Search(keyword);
            return mapper.Map<List<CategoryDTO>>(data);
        }

    }

}
