using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels;
using ASPLab.Services;
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
                TempData["error"] = "Неправильный логин или пароль";
                return View("LoginPage");
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
        public ViewResult PersonalInfo()
        {
            if (!UserSessionValidation.IsUserSessionValid(HttpContext, _user))
            {
                TempData["error"] = "Доступ запрещён";
                return View("LoginPage");
            }
            UserOrderViewModel model = new UserOrderViewModel();
            model.user = UserSessionValidation.GetCurrentUser(HttpContext,_user);
            model.listUserOrderDetails = GetUserOrderDetails(model.user.ID);  
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
                        OrderNumber = item.OrderNumber,
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
        public ViewResult Edit()
        {
            return View(UserSessionValidation.GetCurrentUser(HttpContext, _user));
        }
        public ViewResult EditPassword()
        {
            return View(UserSessionValidation.GetCurrentUser(HttpContext, _user));
        }
        public ViewResult DeleteUser()
        {
            return View(UserSessionValidation.GetCurrentUser(HttpContext, _user));
        }
        public IActionResult ChangePassword(string oldPassword, string newPasswordFirst, string newPasswordSecond)
        {
            User user = UserSessionValidation.GetCurrentUser(HttpContext, _user);
            if(oldPassword != user.Password)
            {
                TempData["error"] = "Неправильный пароль";
                return RedirectToAction("EditPassword");
            }
            if(newPasswordFirst != newPasswordSecond)
            {
                TempData["error"] = "Пароли не совпадают";
                return RedirectToAction("EditPassword");
            }
            if (user.Password == newPasswordSecond)
            {
                TempData["error"] = "Новый пароль совпадает со старым";
                return RedirectToAction("EditPassword");
            }
            TempData["message"] = "Пароль изменен успешно";
            user.Password = newPasswordSecond;
            _user.UpdateUserInfo(user);
            return RedirectToAction("PersonalInfo", new { userID = new Guid(HttpContext.Session.GetString("UserID")) });

        }
        public IActionResult ApplyChanges(User user)
        {
            if (ModelState.IsValid)
            {
                user.ID = UserSessionValidation.GetCurrentUser(HttpContext, _user).ID;
                _user.UpdateUserInfo(user);
                TempData["message"] = $"Пользователь {user.Name} успешно изменен";
                return RedirectToAction("PersonalInfo", new { userID = new Guid(HttpContext.Session.GetString("UserID")) });
            }
            else
            {
                return View("Edit", user);
            }            
        }
        public IActionResult DeleteUserAccount(string password)
        {
            User user = UserSessionValidation.GetCurrentUser(HttpContext, _user);
            if (user.Password == password && user != null)
            {
                _user.DeleteUser(user);
                HttpContext.Session.Clear();
                TempData["message"] = $"Пользователь {user.Name} успешно удален";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Невозможно удалить аккаунт: неправильный пароль или пользователя не существует";
                return RedirectToAction("DeleteUser");
            }
        }
    }
}
