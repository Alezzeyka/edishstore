using ASPLab.Data.DB.Context;
using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.DB.Repository
{
    public class UserRepository : IUser
    {
        private AppDBContent _db;
        public UserRepository(AppDBContent appDBContent)
        {
            _db = appDBContent;
        }
        public IEnumerable<User> Users => _db.User;

        public void AddUser(User user)
        {
            user.UserRoles = _db.UserRole.Where(ur => ur.Role == "User").ToList();
            _db.User.Add(user);
            _db.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _db.User.Remove(user);
            _db.SaveChanges();
        }

        public User GetUserById(Guid userId)
        {
            return _db.User.Find(userId);
        }

        public bool IsPasswordValid(Guid userId,string password)
        {
            if(password != null)
            {
                User user = _db.User.Find(userId);
                return user.Password == password;
            }
            return false;
        }

        public void UpdateUserInfo(User user)
        {
            User newUser = _db.User.Find(user.ID);
            newUser.Name = user.Name;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.Email = user.Email;
            _db.SaveChanges();
        }
    }
}
