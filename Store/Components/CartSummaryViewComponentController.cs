using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        #region private fields

        private readonly Cart _cartService;

        #endregion

        #region ctor

        public CartSummaryViewComponent(Cart cartService)
        {
            _cartService = cartService;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            return View(_cartService);
        }
    }
}