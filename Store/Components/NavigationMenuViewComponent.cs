using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using System.Linq;

namespace Store.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        #region private fields

        private IToyRepository _repository;

        #endregion

        #region ctor

        public NavigationMenuViewComponent(IToyRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            
            return View(_repository.Catrgories);
        }
    }
}
