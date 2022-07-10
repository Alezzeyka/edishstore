using System;
using System.Collections.Generic;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels.OrderInfo;
using ASPLab.Data.ViewModels.ShopCartInfo;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace ASPLab.Data.ViewModels
{
    public class ShopCartViewModel
    {
        private ShopCart ShopCart { get;}
        public List<ShopCartPosition> Positions { get; } = new List<ShopCartPosition>();
        public double TotalSum { get; }

        public ShopCartViewModel(ShopCart shopCart)
        {
            ShopCart = shopCart;
            SetPositions();
            TotalSum = SetSum();
        }
        private double SetSum()
        {
            double totalSum = 0;
            foreach (var position in Positions)
            {
                totalSum += position.Sum;
            }

            return totalSum;
        }
        private void SetPositions()
        {
            
            Dictionary<Dish, CartQuantity_Id> dishQuantity = new Dictionary<Dish, CartQuantity_Id>();
            foreach (ShopCartItem item in ShopCart.listCartItems)
            {
                if (dishQuantity.ContainsKey(item.Dish))
                {
                    dishQuantity[item.Dish].Quantity++;
                    dishQuantity[item.Dish].ShopCartItemIdList.Add(item.ShopCartID);
                }
                else
                {
                    dishQuantity.Add(item.Dish, new CartQuantity_Id(item.ShopCartID,1) );
                }
            }
            foreach (KeyValuePair<Dish, CartQuantity_Id> item in dishQuantity)
            {
                Positions.Add(new ShopCartPosition(item.Value.ShopCartItemIdList,item.Key,item.Value.Quantity));
            }
        }

        private sealed class CartQuantity_Id
        {
            public List<Guid> ShopCartItemIdList { get; } = new List<Guid>();
            public int Quantity { get; set; }

            public CartQuantity_Id(Guid cartItemId, int quantity)
            {
                ShopCartItemIdList.Add(cartItemId);
                Quantity= quantity;
            }
        }
    }
}
