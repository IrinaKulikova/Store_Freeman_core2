using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using System.Linq;

namespace Store.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IToyRepository _repository;

        public NavigationMenuViewComponent(IToyRepository repo)
        {
            _repository = repo;
        }

        public IViewComponentResult Invoke() =>
            View(_repository.Toys
            .Select(t => t.Category)
            .Distinct()
            .OrderBy(c => c));
    }
}
