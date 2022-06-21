using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;

namespace ASPLab.Data.Controllers
{
    public class OrderController:Controller
    {
        private readonly IOrder _allOrders;
        private ShopCart _shopCart;
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
            if(_shopCart.listCartItems.Count <= 0)
            {
                ViewBag.Message = "Заказ не оформлен: корзина пуста";
                return View("Error");
            }           
            Order order = new Order()
            {
                User = _user.Users
                .Where(user => user.ID == new Guid(HttpContext.Session.GetString("UserID")))
                .FirstOrDefault(),
            };
            _allOrders.CreateOrder(order);
            ViewBag.Message = "Заказ успешно оформлен";
            _shopCart.listCartItems.Clear();
            HttpContext.Session.Remove("CartID");
            return View("Complete");
        }

    }
}
