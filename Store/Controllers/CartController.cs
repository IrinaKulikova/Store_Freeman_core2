using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Store.Infrastructure;
using Store.Models;
using Store.Repositories.Interfaces;
using Store.ViewModels;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        private IToyRepository _repository;

        public CartController(IToyRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int ToyID, string returnUrl)
        {
            Toy toy = _repository.Toys
            .FirstOrDefault(р => р.ToyID == ToyID);
            if (toy != null)
            {
                Cart cart = GetCart();
                cart.AddItem(toy, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int toyld,
                                                     string returnUrl)
        {
            Toy toy = _repository.Toys
            .FirstOrDefault(p => p.ToyID == toyld);
            if (toy != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(toy);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}