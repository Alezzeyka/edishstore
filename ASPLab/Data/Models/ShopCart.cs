using ASPLab.Data.DB.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.Models
{
    public class ShopCart
    {
        private AppDBContent _db;
        public ShopCart(AppDBContent appDBContent)
        {
            _db = appDBContent;
        }
        public Guid ShopCartID { get; set; }
        public List<ShopCartItem> listCartItems { get; set; }
        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            AppDBContent context = services.GetService<AppDBContent>();
            string shopCartID = session.GetString("CartID") ?? Guid.NewGuid().ToString();
            session.SetString("CartID", shopCartID);
            return new ShopCart(context) { ShopCartID = new Guid(shopCartID)};
        }
        public void AddCartItem(Dish dish)
        {
            _db.ShopCartItems.Add(new ShopCartItem
            {
                ShopCartID = ShopCartID,
                Dish = dish,
                Price = dish.Price
            });
            _db.SaveChanges();
        }
        public List<ShopCartItem> GetShopCartItems()
        {
            return _db.ShopCartItems
                .Where(cartItem => cartItem.ShopCartID == ShopCartID)
                .Include(cartItem => cartItem.Dish)
                .ToList();
        }
    }
}
