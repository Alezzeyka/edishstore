using ASPLab.Data.Interfaces;
using ASPLab.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPLab.Data.Controllers
{
    public class HomeController:Controller
    {
        private readonly IAllDish _allDish;
        public HomeController(IAllDish allDish)
        {
           _allDish = allDish;
        }
        public ViewResult Index()
        {
        return View(new HomeViewModel { dishes = _allDish.Dishes});
        }

    }
}
