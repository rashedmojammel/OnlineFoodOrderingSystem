using DAL.EF;
using DAL.EF.Tables;
using System.Collections.Generic;

namespace DAL.Repos
{
    public class UserRepo
    {
        OnlineFoodOrderingSystemDbContext db;

        public UserRepo(OnlineFoodOrderingSystemDbContext db)
        {
            this.db = db;
        }

        public bool Create(User u)
        {
            db.Users.Add(u);
            return db.SaveChanges() > 0;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public List<User> Get()
        {
            return db.Users.ToList();
        }

        public bool Update(User u)
        {
            var exobj = Get(u.Id);
            db.Entry(exobj).CurrentValues.SetValues(u);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.Users.Remove(exobj);
            return db.SaveChanges() > 0;
        }
        public User? Login(string name, string role)
        {
            return db.Users.FirstOrDefault(u => u.Name == name && u.Role == role);
        }
    }
}