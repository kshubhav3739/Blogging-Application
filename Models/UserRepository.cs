using System;
using System.Collections.Generic;
using System.Linq;

namespace BloggingApp.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext _context)
        {
            context = _context;
        }

        public User AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public IEnumerable<User> AllUsers()
        {
            return context.Users;
        }

        public User DeleteUser(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
            }
            return user;
        }

        public string ForgotPassword(string email, Int64 mobile)
        {
            User user = context.Users.Single(u => u.email == email && u.mobile == mobile);
            return user.password;
        }

        public User GetUser(int id)
        {
            return context.Users.Find(id);
        }

        public User GetUser(string email)
        {
            return context.Users.First(u => u.email == email);
        }

        public bool Login(string email, string password)
        {
            var user = context.Users.Where(u => u.email == email && u.password == password);
            if (user.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User UpdateUser(User userChange)
        {
            var user = context.Users.Attach(userChange);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return userChange;
        }

        public bool Validate(string email, Int64 mobile)
        {
            var user = context.Users.Where(u => u.email == email && u.mobile == mobile);
            if (user.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}