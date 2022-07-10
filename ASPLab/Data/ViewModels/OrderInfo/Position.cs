using ASPLab.Data.Models;
using System;

namespace ASPLab.Data.ViewModels.OrderInfo
{
    public class Position
    {
        public Dish Dish { get; }
        public int Quantity { get; }
        public double Sum { get; }

        public Position()
        {
                
        }
        public Position(Dish dish, int quantity)
        {
            Dish = dish;
            Quantity = quantity;
            Sum = Math.Round(CalculateSum(),2);
        }
        private double CalculateSum() => Quantity * Dish.Price;

    }
}
