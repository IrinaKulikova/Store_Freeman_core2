using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories.Interfaces;
using Store.ViewModels;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        #region private fields

        private readonly IToyRepository _repository;
        private readonly Cart _cartService;

        #endregion

        #region ctor

        public CartController(IToyRepository repository, Cart cartService)
        {
            _repository = repository;
            _cartService = cartService;
        }

        #endregion

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = _cartService,
                ReturnUrl = returnUrl
            });
        }

        public async Task<RedirectToActionResult> AddToCart(int ToyId,
                                                            string returnUrl)
        {
            var toy = await _repository.FindByIdForSerializeAsync(ToyId);

            if (toy != null)
            {
                _cartService.AddItem(toy, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int toyId,
                                                     string returnUrl)
        {
            if (toyId != 0)
            {
                _cartService.RemoveLine(toyId);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}