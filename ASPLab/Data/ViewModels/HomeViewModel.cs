using ASPLab.Data.Models;
using System.Collections.Generic;

namespace ASPLab.Data.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Dish> dishes { get; set; }
    }
}
