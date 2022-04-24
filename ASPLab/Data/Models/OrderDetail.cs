using System;

namespace ASPLab.Data.Models
{
    public class OrderDetail
    {
        public Guid ID { get; set; }
        public Order Order { get; set; }
        public Dish Dish { get; set; }
    }
}
