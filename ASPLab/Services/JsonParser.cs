using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ASPLab.Data.Models;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings;
using System.IO;

namespace ASPLab.Services
{
    public static class JsonParser
    {
        public static List<Dish> ParseDishes(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            List<Dish> dishes = JsonSerializer.Deserialize<List<Dish>>(jsonString)!;
            return dishes;
        }
    }
}
