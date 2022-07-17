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
            string fileName = System.IO.Path.GetFileName(file.FileName);
            var filePath = Directory.GetCurrentDirectory() + "\\jsonfile.json";
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyToAsync(stream);
            }
            List<Dish> dishList = JsonParser.ParseDishes(filePath);
            System.IO.File.Delete(filePath);
            TempData["message"] = $"Успешно добавлено {dishList.Count} записей";
            return RedirectToAction("PersonalInfo", "User");
        }
    }
}
