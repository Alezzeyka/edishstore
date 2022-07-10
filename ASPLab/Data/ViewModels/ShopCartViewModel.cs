using System.Collections.Generic;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels.OrderInfo;

namespace ASPLab.Data.ViewModels
{
    public class ShopCartViewModel
    {
        private ShopCart ShopCart { get;}
        public List<Position> Positions { get; } = new List<Position>();
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
            Dictionary<Dish, int> dishQuantity = new Dictionary<Dish, int>();
            foreach (ShopCartItem item in ShopCart.listCartItems)
            {
                if (dishQuantity.ContainsKey(item.Dish))
                {
                    dishQuantity[item.Dish]++;
                }
                else
                {
                    dishQuantity.Add(item.Dish, 1);
                }
            }
            foreach (KeyValuePair<Dish, int> item in dishQuantity)
            {
                Positions.Add(new Position(item.Key, item.Value));
            }
        }
    }
}
