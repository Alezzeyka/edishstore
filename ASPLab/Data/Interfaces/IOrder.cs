using ASPLab.Data.Models;
using System;
using System.Collections.Generic;

namespace ASPLab.Data.Interfaces
{
    public interface IOrder
    {
        IEnumerable<Order> Orders { get; }
        void CreateOrder(Order order);
        List<Order> GetUserOrders(Guid userId);
        Order GetOrderById(Guid orderId);
    }
}
