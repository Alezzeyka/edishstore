using System;
using System.Collections.Generic;
using ASPLab.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using ASPLab.Data.Models;
using ASPLab.Services;
using Microsoft.AspNetCore.Http;

namespace ASPLab.Data.Controllers
{
    public class JsonUploadController:Controller
    {
        private IAllDish _dish;
        public JsonUploadController(IAllDish dish)
        {
            _dish=dish;
        }

        public RedirectToActionResult GetJson(IFormFile file)
        {
            try
            {
                int recordsCount;
                string fileName = System.IO.Path.GetFileName(file.FileName);
                var filePath = Directory.GetCurrentDirectory() + "\\jsonfile.json";
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                }

                List<Dish> dishList = JsonParser.ParseDishes(filePath);
                System.IO.File.Delete(filePath);
                foreach (Dish dish in dishList)
                {
                    Category category = _dish.GetCategoryByName(dish.Category.Name);
                    if (category == null)
                    {
                        TempData["error"] = $"Категория {dish.Category.Name} не существует";
                        return RedirectToAction("PersonalInfo", "User");
                    }

                    dish.Category = category;
                }

                recordsCount = _dish.AddRange(dishList);
                TempData["message"] = $"Успешно добавлено {recordsCount} записей";
                return RedirectToAction("PersonalInfo", "User");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Ошибка загрузки файла";
                return RedirectToAction("PersonalInfo", "User");
            }
        }

    }
}




