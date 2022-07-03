using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels;
using ASPLab.Data.ViewModels.OrderInfo;
using ASPLab.Services;
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
            if (UserSessionValidation.IsUserSessionValid(HttpContext,_user))
            {
                _shopCart.listCartItems = _shopCart.GetShopCartItems();
                if (_shopCart.listCartItems.Count <= 0)
                {
                    TempData["error"] = "Заказ не оформлен: корзина пуста";
                    return RedirectToAction("Index", "ShopCart");
                }
                Order order = new Order()
                {
                    User = UserSessionValidation.GetCurrentUser(HttpContext,_user)
                };
                _allOrders.CreateOrder(order);
                _shopCart.listCartItems.Clear();
                HttpContext.Session.Remove("CartID");
                TempData["message"] = "Заказ успешно оформлен";
                return RedirectToAction("Index","Home");
            }
            TempData["error"] = "Для оформления заказа необходима авторизация";
            return RedirectToAction("LoginPage","User");
        }
        public IActionResult Info(Guid orderId)
        {
            Order order = _allOrders.GetOrderById(orderId);
            if (!UserSessionValidation.IsUserSessionValid(HttpContext, _user, order.User.ID))
            {
                TempData["error"] = "Доступ запрещён";
                return RedirectToAction("LoginPage", "User");
            }
            return View(new OrderInfoViewModel(order));
        }

    }
}
