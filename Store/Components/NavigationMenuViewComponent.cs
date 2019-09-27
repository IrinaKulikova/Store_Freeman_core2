using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using System.Linq;

namespace Store.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IToyRepository _repository;

        public NavigationMenuViewComponent(IToyRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            
            return View(_repository.Catrgories);
        }
    }
}
