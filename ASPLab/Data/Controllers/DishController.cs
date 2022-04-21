using Microsoft.AspNetCore.Mvc;
using ASPLab.Data.Interfaces;
using System.Collections.Generic;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels;

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
            ViewBag.Title = "Страница с посудой";
            DishListViewModels model = new DishListViewModels();
            model.AllDishes = _allDish.Dishes;
            model.currCategory = "Тарелки";          
            return View(model);
        }
    }
}
