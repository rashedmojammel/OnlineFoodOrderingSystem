using DAL.EF;
using DAL.EF.Tables;
using System.Collections.Generic;
using System.Linq;

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

        public User? Get(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> Get()
        {
            return db.Users.ToList();
        }

        public bool Update(User u)
        {
            var exobj = Get(u.Id);
            if (exobj == null) return false;
            db.Entry(exobj).CurrentValues.SetValues(u);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            if (exobj == null) return false;
            db.Users.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public User? Login(string email, string password)
        {
            var allUsers = db.Users.ToList();
            return allUsers.FirstOrDefault(u =>
                u.Email.Trim().ToLower() == email.Trim().ToLower() &&
                u.Password.Trim() == password.Trim()
            );
        }

        public bool EmailExists(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }

        public List<User> Search(String u)
        {
            return db.Users
                .Where(user => user.Name.Contains(u) ||
                user.Email.Contains(u)||
                user.Role.Contains(u)
                ).ToList();
        }


    }
}