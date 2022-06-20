using System;

namespace ASPLab.Data.Models
{
    public class UserOrderDetails
    {
        public Guid OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public double orderSum { get; set; }
    }
}
