using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class OrderController : Controller
    {
        #region private fields

        private IOrderRepository _repository;
        private Cart _cart;

        #endregion

        #region ctor

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repository = repoService;
            _cart = cartService;
        }

        #endregion

        [Authorize]
        public async Task<ViewResult> List()
        {
            return View(await _repository.Orders(false));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkShipped(int orderId)
        {
            var order = await _repository.FindByIdAsync(orderId);

            if (order != null)
            {
                order.Shipped = true;
                await _repository.SaveOrUpdate(order);
            }

            return RedirectToAction(nameof(List));
        }


        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty! ");
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.Lines = _cart.Lines.ToArray();
            await _repository.SaveOrUpdate(order);

            return RedirectToAction(nameof(Completed));
        }


        public ViewResult Completed()
        {
            _cart.Clear();

            return View();
        }
    }
}