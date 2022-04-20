using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPLab.Data.Mocks
{
    public class MockDish : IAllDish
    {
        private readonly IDishCategory _dishCategory = new MockCategory();
        public IEnumerable<Dish> Dishes 
        { get
            {
                return new List<Dish>
                {
                    new Dish
                    {
                        ID = Guid.NewGuid(),
                        Name = "Тарелка большая",
                        Price = 9.99F,
                        Color = "Белый",
                        Img = @"/img/Plate-500x500.jpg",
                        Material = "Керамика",
                        Volume = 150,
                        Category = _dishCategory.Categories.Where(c => c.Name == "Тарелки").ToList().First()
                    },
                    new Dish
                    {
                        ID = Guid.NewGuid(),
                        Name = "Стакан обычный",
                        Price = 5.5F,
                        Color = "Прозрачный",
                        Img = @"/img/Glass-500x500.jpg",
                        Material = "Стекло",
                        Volume = 200,
                        Category = _dishCategory.Categories.Where(c => c.Name == "Стаканы").ToList().First()
                    },
                    new Dish
                    {
                        ID = Guid.NewGuid(),
                        Name = "Чашка для чая",
                        Price = 10.99F,
                        Color = "Красный",
                        Img = @"/img/TeaCup-500x500.jpg",
                        Material = "Керамика",
                        Volume = 500,
                        Category = _dishCategory.Categories.Where(c => c.Name == "Чашки").ToList().First()
                    }
                };
            } 
        }
        public Dish GetDish(Guid dishID)
        {
            throw new NotImplementedException();
        }
    }
}
