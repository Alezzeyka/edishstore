using System.Collections.Generic;
using ASPLab.Data.Models;
using Newtonsoft.Json.Linq;

namespace ASPLab.Services
{
    public static class JsonParser
    {
        public static string JsonPath;
        public static List<Dish> ParseDishes(JObject jObject)
        {
            List<Dish> dishes = new List<Dish>();
            if (jObject["Dishes"] != null)
            {
                foreach (var item in jObject["Dishes"])
                {
                    dishes.Add(
                        new Dish
                        {
                            Name = item["Name"].ToString(),
                            Price = float.Parse(item["Price"].ToString()),
                            Color = item["Color"].ToString(),
                            Material = item["Material"].ToString(),
                            Volume = ushort.Parse(item["Volume"].ToString()),
                            Img = item["Img"].ToString(),
                            Category = new Category{Name = item["Category"].ToString()}
                        });
                }
            }

            return dishes;
        }

    }
}
