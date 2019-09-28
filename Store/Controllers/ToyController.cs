using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using Store.ViewModels;
using System.Linq;

namespace Store.Controllers
{
    public class ToyController : Controller
    {
        #region private fields

        private readonly IToyRepository _toyRepository;

        #endregion

        #region props

        public int PageSize { get; set; } = 4;

        #endregion

        #region ctor

        public ToyController(IToyRepository toyRepository)
        {
            _toyRepository = toyRepository;
        }

        #endregion

        public ViewResult List(string category, int toyPage = 1)
        {
            var toys = _toyRepository.Toys()
                       .Where(t => category == null || t.Category == category)
                       .OrderBy(p => p.ToyID)
                       .Skip((toyPage - 1) * PageSize)
                       .Take(PageSize);

            var model = new ToysListViewModel
            {
                Toys = toys,
                CurrentCategory = category,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = toyPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    toys.Count() :
                    toys.Where(t => t.Category == category).Count()
                }
            };

            return View(model);
        }
    }
}