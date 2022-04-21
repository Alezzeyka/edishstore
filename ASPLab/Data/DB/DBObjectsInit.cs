using ASPLab.Data.DB.Context;
using ASPLab.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.DB
{
    public class DBObjectsInit
    {
        public static void DefaultDBObjectsInitialization(AppDBContent db)
        {
            Random random = new Random();
            if(!db.Category.Any() && !db.Dishes.Any())
            {
                List<Category> categories = new List<Category>
                {
                    new Category { ID = new Guid(), Name = "Тарелки" },
                    new Category { ID = new Guid(), Name = "Стаканы" },
                    new Category { ID = new Guid(), Name = "Чашки" }                                 
                };
                List<Dish> dishes = new List<Dish>();
                List<Dish> plateDishes = new List<Dish>();
                List<Dish> cupDishes = new List<Dish>();
                List<Dish> glassDishes = new List<Dish>();
                for(int idx = 0; idx < 20; ++idx)
                {
                    if (idx < 10)
                    {
                        Dish dish = new Dish
                        {
                            ID = Guid.NewGuid(),
                            Name = $"Тарелка {idx}",
                            Price = (float)random.NextDouble() * 10,
                            Color = "Белый",
                            Img = @"/img/Plate-500x500.jpg",
                            Material = "Керамика",
                            Volume = 150,
                            Category = categories[0]
                        };
                        dishes.Add(dish);
                        plateDishes.Add(dish);
                    } else if(idx > 10 && idx < 15)
                    {
                        Dish dish = new Dish
                        {
                            ID = Guid.NewGuid(),
                            Name = $"Стакан {idx}",
                            Price = (float)random.NextDouble() * 10,
                            Color = "Прозрачный",
                            Img = @"/img/Glass-500x500.jpg",
                            Material = "Стекло",
                            Volume = 200,
                            Category = categories[1]
                        };
                        dishes.Add(dish);
                        glassDishes.Add(dish);
                    }
                    else
                    {
                        Dish dish = new Dish
                        {
                            ID = Guid.NewGuid(),
                            Name = $"Чашка для чая {idx}",
                            Price = (float)random.NextDouble() * 10,
                            Color = "Красный",
                            Img = @"/img/TeaCup-500x500.jpg",
                            Material = "Керамика",
                            Volume = 500,
                            Category = categories[2]
                        };
                        dishes.Add(dish);
                        cupDishes.Add(dish);
                    }
                }
                categories[0].Dishes = plateDishes;
                categories[1].Dishes = glassDishes;
                categories[2].Dishes = cupDishes;
                db.Dishes.AddRange(dishes);
                db.Category.AddRange(categories);
                db.SaveChanges();
            }
        }
    }
}
