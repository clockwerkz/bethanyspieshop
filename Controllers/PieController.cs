using bethanyspieshop.Models;
using bethanyspieshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bethanyspieshop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category="")
        {
            PiesListViewModel piesListViewModel = new PiesListViewModel()
            {
                Pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName.Contains(category)),
                CurrentCategory = category == "" ? "All Pies" : category
            };
            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
            {
                return NotFound();
            }
            return View(pie);

        }
    }
}
