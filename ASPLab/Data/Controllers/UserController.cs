using ASPLab.Data.Interfaces;
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
    }
}
