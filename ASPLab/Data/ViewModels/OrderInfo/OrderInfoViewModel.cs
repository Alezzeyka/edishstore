using ASPLab.Data.Models;
using ASPLab.Data.ViewModels.OrderInfo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.ViewModels
{
    public sealed class OrderInfoViewModel
    {
        public Guid OrderId { get; }
        public DateTime OrderDate { get; }
        public double TotalSum { get; }
        public List<Position> Positions { get; }
        public OrderInfoViewModel(Order order)
        {
            OrderId = order.ID;
            OrderDate = order.orderTime;
            SetPositions();
            TotalSum = CalculateTotalSum();

            void SetPositions()
            {
                Dictionary<Dish, int> dishQuantity = new Dictionary<Dish, int>();
                foreach (OrderDetail orderDetail in order.OrgerDetails)
                {
                    if (dishQuantity.ContainsKey(orderDetail.Dish))
                    {
                        dishQuantity[orderDetail.Dish]++;
                    }
                    else
                    {
                        dishQuantity.Add(orderDetail.Dish, 1);
                    }
                }
                foreach (KeyValuePair<Dish,int> item in dishQuantity)
                {
                    Positions.Add(new Position(item.Key, item.Value));
                }
            }
        }
        private double CalculateTotalSum()
        {
            double totalSum = 0;
            foreach (Position item in Positions)
            {
                totalSum += item.Sum;
            }
            return totalSum;
        }
    }
}
