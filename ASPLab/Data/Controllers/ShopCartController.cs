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
    public class ShopCartController:Controller
    {
        private readonly IAllDish _allDish;
        private readonly ShopCart _shopCart;
        public ShopCartController(IAllDish allDish, ShopCart shopCart)
        {
            _allDish = allDish;
            _shopCart = shopCart;
        }
        public ViewResult Index()
        {
            List<ShopCartItem> cartItems = _shopCart.GetShopCartItems();
            _shopCart.listCartItems = cartItems;
            return View(new ShopCartViewModel { ShopCart = _shopCart });
        }
        public RedirectToActionResult AddToCart(Guid dishID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("LoginPage","User");
            }
            var dish = _allDish.Dishes.FirstOrDefault(item => item.ID == dishID);
            if (dish != null)
            {
                _shopCart.AddCartItem(dish);
            }
            return RedirectToAction("Index");
        }
    }
}
