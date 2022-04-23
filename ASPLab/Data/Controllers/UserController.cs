﻿using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASPLab.Data.Controllers
{
    public class UserController:Controller
    {
        private IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        public IActionResult Login(string login, string password)
        {
            User user = _user.Users
                .Where(user=>user.Login == login && user.Password == password)
                .FirstOrDefault();
            if(user == null)
            {
                return View("Error");
            }
            HttpContext.Session.SetString("UserID", user.ID.ToString());
            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddUser(User user)
        {
            _user.AddUser(user);
            Login(user.Login,user.Password);
            return RedirectToAction("Index", "Home");
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
    }
}