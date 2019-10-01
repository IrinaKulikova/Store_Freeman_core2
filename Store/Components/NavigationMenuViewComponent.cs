using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using System.Linq;

namespace Store.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        #region private fields

        private ICategoryRepository _categoryRepository;

        #endregion

        #region ctor

        public NavigationMenuViewComponent(IToyRepository repository,
                                            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_categoryRepository.Categories());
        }
    }
}
