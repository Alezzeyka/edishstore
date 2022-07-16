using ASPLab.Data.DB.Context;
using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.DB.Repository
{
    public class DishRepository : IAllDish
    {
        private readonly AppDBContent _db;
        public DishRepository(AppDBContent appDBContent)
        {
            _db = appDBContent;
        }
        public IEnumerable<Dish> Dishes => _db.Dishes.Include(dish => dish.Category);

        public Dish GetDish(Guid dishID) => _db.Dishes
            .Include(dish => dish.Category)
            .FirstOrDefault(dish => dish.ID == dishID);

        public int AddRange(List<Dish> dishes)
        {
            _db.AddRange(dishes);
            return _db.SaveChanges();
        }

        public Category GetCategoryByName(string name)
        {
            return _db.Category.FirstOrDefault(c => c.Name == name);
        }
    }
}
