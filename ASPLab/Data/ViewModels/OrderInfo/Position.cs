﻿using ASPLab.Data.Models;
using System;

namespace ASPLab.Data.ViewModels.OrderInfo
{
    public sealed class Position
    {
        public Dish Dish { get; }
        public int Quantity { get; }
        public double Sum { get; }
        public Position(Dish dish, int quantity)
        {
            Dish = dish;
            Quantity = quantity;
            Sum = Math.Round(CalculateSum());
        }
        private double CalculateSum() => Quantity * Dish.Price;

    }
}
