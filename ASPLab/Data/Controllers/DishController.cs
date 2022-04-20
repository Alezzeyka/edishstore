using Microsoft.AspNetCore.Mvc;
using ASPLab.Data.Interfaces;
using System.Collections.Generic;
using ASPLab.Data.Models;

namespace ASPLab.Data.Controllers
{
    public class DishController:Controller
    {
        private readonly IAllDish _allDish;
        private readonly IDishCategory _AllCategories;
        public DishController(IAllDish allDish, IDishCategory dishCategory)
        {
            _allDish = allDish;
            _AllCategories = dishCategory;
        }
        public ViewResult List()
        {
            IEnumerable<Dish> Dishes = _allDish.Dishes;
            return View(Dishes);
        }
    }
}
