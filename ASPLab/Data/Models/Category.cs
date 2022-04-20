using System;
using System.Collections.Generic;

namespace ASPLab.Data.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Dish> Dishes { get; set; }
    }
}
