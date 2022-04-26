using ASPLab.Data.DB.Context;
using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
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
            _db.User.Add(user);
            _db.SaveChanges();
        }
    }
}
