using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using System;
using System.Collections.Generic;

namespace ASPLab.Data.Mocks
{
    public class MockCategory : IDishCategory
    {
        public IEnumerable<Category> Categories 
        { get
            {
                return new List<Category>
                {
                    new Category{ID = new Guid(), Name = "Тарелки"},
                    new Category{ID = new Guid(), Name = "Стаканы"},
                    new Category{ID= new Guid(), Name = "Чашки"}
                };
            } 
        }
    }
}
