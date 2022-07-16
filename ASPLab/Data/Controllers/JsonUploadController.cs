using System;
using System.Collections.Generic;
using ASPLab.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            int recordsCount;
            try
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(stream);
                    List<Dish> dishList = JsonParser.ParseDishes(JObject.Parse(file.FileName));
                    recordsCount = _dish.AddRange(dishList);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("PersonalInfo", "User");
            }
            TempData["message"] = $"Успешно добавлено {recordsCount} записей";
            return RedirectToAction("PersonalInfo", "User");

        }

        
    }
}
