using System;
using System.Collections.Generic;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels.OrderInfo;

namespace ASPLab.Data.ViewModels.ShopCartInfo
{
    public class ShopCartPosition:Position
    {
        public List<Guid> ShopCartItemId { get; } = new List<Guid>();

        public ShopCartPosition(List<Guid> shopCartItemId ,Dish dish, int quantity) :base(dish, quantity)
        {
            ShopCartItemId = shopCartItemId;
        }
    }
}
