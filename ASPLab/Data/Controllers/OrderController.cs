using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ASPLab.Data.Controllers
{
    public class OrderController:Controller
    {
        private readonly IOrder _allOrders;
        private readonly ShopCart _shopCart;
        private readonly IUser _user;
        public OrderController(IOrder allOrders, ShopCart shopCart, IUser user)
        {
            _allOrders = allOrders;
            _shopCart = shopCart;
            _user = user;
        }
        public IActionResult AddOrder()
        {
            _shopCart.listCartItems = _shopCart.GetShopCartItems();
            Order order = new Order()
            {
                User = _user.Users
                .Where(user => user.ID == new Guid(HttpContext.Session.GetString("UserID")))
                .FirstOrDefault(),
            };
            _allOrders.CreateOrder(order);
            ViewBag.Message = "Заказ успешно отправлен на обработку.";
            return View("Complete");
        }

    }
}
