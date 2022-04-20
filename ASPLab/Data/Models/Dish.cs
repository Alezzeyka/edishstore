using System;

namespace ASPLab.Data.Models
{
    public class Dish
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public ushort Volume { get; set; }
        public string Img   { get; set; }
        public Guid CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
