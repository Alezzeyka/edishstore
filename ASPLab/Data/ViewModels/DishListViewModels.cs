using ASPLab.Data.Models;
using System.Collections.Generic;

namespace ASPLab.Data.ViewModels
{
    public class DishListViewModels
    {
        public IEnumerable<Dish> AllDishes { get; set; }
        public string currCategory { get; set; }
    }
}
