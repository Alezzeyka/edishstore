using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ASPLab.Data.Models;
using System.Text.Json;

namespace ASPLab.Services
{
    public static class JsonParser
    {
        public static string JsonPath;
        public static List<Dish> ParseDishes(string jsonByte)
        {
            var dishes = JsonSerializer.Deserialize<List<Dish>>(jsonByte,new JsonSerializerOptions{ })!;

            var numb = 0;
            
            /*if (jObject["Dishes"] != null)
            {
                foreach (var item in jObject["Dishes"])
                {
                    Dish dish = JsonSerializer.Deserialize<Dish>(item);
                    dishes.Add(
                        new Dish
                        {
                            Name = EncodeString(item["Name"]),
                            Price = float.Parse(item["Price"].ToString().Replace(".",",")),
                            Color = item["Color"].ToString(),
                            Material = item["Material"].ToString(),
                            Volume = ushort.Parse(item["Volume"].ToString()),
                            Img = item["Img"].ToString(),
                            Category = new Category{Name = EncodeString(item["Category"].ToString()) }
                        });
                }
                return dishes;
            }*/
            return dishes;
        }

        private static string EncodeString(string initString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(initString);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
