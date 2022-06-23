using ASPLab.Data.Models;

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
            Sum = CalculateSum();
        }
        private double CalculateSum() => Quantity * Dish.Price;

    }
}
