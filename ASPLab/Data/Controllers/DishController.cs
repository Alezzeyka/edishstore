using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPLab.Data.Interfaces;
using System.Collections.Generic;
using ASPLab.Data.Models;
using ASPLab.Data.ViewModels;
using System.Linq;
using System;

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
        [Route("Dish/List/{category}")]
        public ViewResult List(string category)
        {
            DishListViewModels model = new DishListViewModels();
            model.currCategory = _AllCategories.Categories
                .Where(cat => cat.Name == category)
                .FirstOrDefault();                
            if(model.currCategory == null)
            {
                return View("NotFoundCategory") ;
            }            
            ViewBag.Title = model.currCategory.Name;
            model.AllDishes = _allDish.Dishes.Where(dish => dish.Category == model.currCategory);
            return View(model);
        }
        public IActionResult ListName(string name)
        {
            DishListViewModels model = new DishListViewModels();
            model.AllDishes = _allDish.Dishes
                .Where(dish => dish.Name == name);
            model.currCategory = new Category()
            {
                Name = "Результат поиска",
            };
            return View("List",model);
        }
        
        public ViewResult DishInfo(Guid dishID)
        {
            Dish model = new Dish();
            model = _allDish.GetDish(dishID);
            return View(model);
        }
    }
}
