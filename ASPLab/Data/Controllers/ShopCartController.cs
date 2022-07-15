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
            return View(new ShopCartViewModel (_shopCart));
        }

        private bool AddItem(Guid dishID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return false;
            }
            var dish = _allDish.Dishes.FirstOrDefault(item => item.ID == dishID);
            if (dish != null)
            {
                _shopCart.AddCartItem(dish);
                return true;
            }

            return false;
        }
        public RedirectToActionResult AddToCart(Guid dishID)
        {
            if (AddItem(dishID))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("LoginPage", "User");
        }

        public RedirectToActionResult AddToCartWithoutRedirection(Guid dishID)
        {
            if (!AddItem(dishID))
            {
                return RedirectToAction("LoginPage", "User");
            }

            TempData["message"] = $"Товар {_allDish.GetDish(dishID).Name} успешно добавлен в корзину";
            return RedirectToAction("Index", "Home");
        }
        public RedirectToActionResult AddOneItemToCart(Guid dishID)
        {
            AddItem(dishID);
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveOneItemFromCart(Guid cartItemId)
        {
            _shopCart.listCartItems = _shopCart.GetShopCartItems();
            _shopCart.RemoveCartItem(cartItemId);
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveAllItemsFromCart()
        {
            _shopCart.listCartItems = _shopCart.GetShopCartItems();
            _shopCart.RemoveAllShopCartItems();
            return RedirectToAction("Index");
        }
    }
}
