using ASPLab.Data.Models;
using System.Collections.Generic;

namespace ASPLab.Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> Users { get; }
        void AddUser(User user);
    }
}
