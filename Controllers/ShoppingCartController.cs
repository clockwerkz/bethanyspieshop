using bethanyspieshop.Models;
using bethanyspieshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bethanyspieshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            Console.WriteLine(pieId);
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == pieId);
            Console.WriteLine(selectedPie?.Name);
            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var pieToRemove = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == pieId);

            if (pieToRemove != null)
            {
                _shoppingCart.RemoveFromCart(pieToRemove);
            }
            return RedirectToAction("Index");
        }
    }
}
