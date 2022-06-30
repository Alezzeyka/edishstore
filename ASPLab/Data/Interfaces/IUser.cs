using ASPLab.Data.Models;
using System;
using System.Collections.Generic;

namespace ASPLab.Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> Users { get; }
        void AddUser(User user);
        void UpdateUserInfo(User user);
        void DeleteUser(User user);
        User GetUserById(Guid userId);
        bool IsPasswordValid(Guid userId,string password);
    }
}
