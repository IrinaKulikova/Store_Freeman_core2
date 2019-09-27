using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories.Interfaces;
using Store.ViewModels;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        private readonly IToyRepository _repository;
        private readonly Cart _cartService;

        public CartController(IToyRepository repository, Cart cartService)
        {
            _repository = repository;
            _cartService = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cartService,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int ToyID, string returnUrl)
        {
            Toy toy = _repository.Toys
            .FirstOrDefault(р => р.ToyID == ToyID);

            if (toy != null)
            {
                _cartService.AddItem(toy, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int ToyID,
                                                     string returnUrl)
        {
            Toy toy = _repository.Toys
            .FirstOrDefault(p => p.ToyID == ToyID);

            if (toy != null)
            {
                _cartService.RemoveLine(toy);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}