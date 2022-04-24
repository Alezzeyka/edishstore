using System;
using System.Collections.Generic;

namespace ASPLab.Data.Models
{
    public class Order
    {
        public Guid ID { get; set; }
        public User User { get; set; }
        public DateTime orderTime { get; set; }
        public List<OrderDetail> OrgerDetails { get; set; }
    }
}
