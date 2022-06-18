using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.Controllers
{
    public class UserController:Controller
    {
        private IUser _user;
        private IOrder _order;
        public UserController(IUser user, IOrder order)
        {
            _user = user;
            _order = order;
        }
        public IActionResult Login(string login, string password)
        {
            User user = _user.Users
                .Where(user=>user.Login == login && user.Password == password)
                .FirstOrDefault();
            if(user == null)
            {
                ViewBag.Message = "Неправильный логин или пароль";
                return View("Error");
            }
            HttpContext.Session.SetString("UserID", user.ID.ToString());
            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {

                if (_user.Users.Where(u => u.Login == user.Login).FirstOrDefault() != null)
                {
                    user.Login = null;
                    return RedirectToAction("Register",user);
                }
                _user.AddUser(user);
                return RedirectToAction("LoginPage");
            }
            return View("Register",user);   
            
        }
        public ViewResult Register()
        {
            User user = new User();
            return View(user);
        }
        public ViewResult LoginPage()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
        public ViewResult PersonalInfo(Guid userID)
        {
            UserOrderViewModel model = new UserOrderViewModel();

            model.user = _user.Users
                .Where(user => user.ID==userID)
                .FirstOrDefault();
            model.listUserOrderDetails = GetUserOrderDetails(model.user.ID);  
            if (model.user == null)
            {
                ViewBag.Message = "Неправильный логин или пароль";
                return View("Error");
            }
            HttpContext.Session.SetString("UserID", model.user.ID.ToString());
            HttpContext.Session.SetString("UserName", model.user.Name);
            return View(model);
        }
        private List<UserOrderDetails> GetUserOrderDetails(Guid userId)
        {
            try
            {
                List<UserOrderDetails> userOrderDetails = new List<UserOrderDetails>();
                List<Order> order = _order.GetUserOrders(userId);
                foreach (Order item in order)
                {
                    userOrderDetails.Add(new UserOrderDetails
                    {
                        OrderId = item.ID,
                        orderDate = item.orderTime,
                        orderSum = GetOrderSum(item)
                    });
                }
                return userOrderDetails;
            }
            catch
            {
                return null;
            }
        }
        private double GetOrderSum(Order order)
        {
            double sum = 0;
            foreach(OrderDetail item in order.OrgerDetails)
            {
                sum += item.Dish.Price;
            }
            return Math.Round(sum,2);
        }
        
    }
}
