using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repos
{
    public class FoodRepo
    {
        OnlineFoodOrderingSystemDbContext db;
        public FoodRepo(OnlineFoodOrderingSystemDbContext db)
        {
            this.db = db;
        }
        public bool Create(Food f)
        {
            db.Foods.Add(f);
            return db.SaveChanges() > 0;
        }
        public Food? Get(int id)
        {
            return db.Foods.Find(id);
        }
        public List<Food> Get()
        {
            return db.Foods.ToList();
        }
        public List<Food> GetbyCategory(int categoryID)
        {
            return db.Foods.Where(f => f.CategoryId == categoryID).ToList();
        }
        public bool Update(Food f)
        {
            var exobj = db.Foods.Find(f.Id);
            if (exobj == null) return false;
            db.Entry(exobj).CurrentValues.SetValues(f);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var exobj = db.Foods.Find(id);
            if (exobj == null) return false;
            db.Foods.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public List<Food> Search(string keyword)
        {
            return db.Foods
                .Include(f => f.Category)
                .Where(f => f.Name.Contains(keyword) ||
                            (f.Description != null &&
                             f.Description.Contains(keyword)))
                .ToList();
        }

    }
}
