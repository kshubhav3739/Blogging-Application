using System;
using System.Collections.Generic;

namespace BloggingApp.Models
{
    public interface IUserRepository
    {
        User AddUser(User user);
        IEnumerable<User> AllUsers();
        User GetUser(int id);
        User GetUser(string email);
        User UpdateUser(User userChange);
        User DeleteUser(int id);
        bool Login(string email, string password);
        bool Validate(string email, Int64 mobile);
        string ForgotPassword(string email, Int64 mobile);

    }
}