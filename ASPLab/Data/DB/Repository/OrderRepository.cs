using ASPLab.Data.DB.Context;
using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.DB.Repository
{
    public class OrderRepository: IOrder
    {
        private readonly AppDBContent _db;
        private readonly ShopCart shopCart;

        public OrderRepository(AppDBContent db, ShopCart shopCart)
        {
            _db = db;
            this.shopCart = shopCart;
        }
        public IEnumerable<Order> Orders => _db.Order;

        public void CreateOrder(Order order)
        {
            order.orderTime = DateTime.Now;
            _db.Order.Add(order);
            _db.SaveChanges();
            List<ShopCartItem> items = shopCart.listCartItems;
            foreach (ShopCartItem cartItem in items)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Dish = cartItem.Dish,
                    Order = order
                };
                _db.OrderDetail.Add(orderDetail);
            }
            _db.SaveChanges();
        }

        public List<Order> GetUserOrders(Guid userId)
        {
            List<Order> order = _db.Order
                .Where(order => order.User.ID == userId)
                .Select(order => order)
                .ToList();
            foreach (Order item in order)
            {
                item.OrgerDetails = _db.OrderDetail
                    .Where(od => od.Order.ID == item.ID)
                    .Include(od => od.Dish)
                    .Include(od => od.Order)
                    .Select(od => od)
                    .ToList();
            }
            return order;
        }
    }
}
