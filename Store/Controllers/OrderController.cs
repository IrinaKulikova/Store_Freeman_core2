using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        public async Task<ViewResult> List()
        {
            return View(await repository.Orders(false));
        }

        [HttpPost]
        public async Task<IActionResult> MarkShipped(int orderID)
        {
            var order = await repository.FindByIdAsync(orderID);

            if (order != null)
            {
                order.Shipped = true;
                await repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }


        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty! ");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                await repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }


        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}