using System;
using System.Collections.Generic;

namespace ASPLab.Data.Models
{
    public class UserRoles
    {
        public Guid ID { get; set; }
        public List<User> Users { get; set; }
        public string Role { get; set; }
    }
}
