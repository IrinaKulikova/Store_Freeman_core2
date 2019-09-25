using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using Store.ViewModels;
using System.Linq;

namespace Store.Controllers
{
    public class ToyController : Controller
    {
        private readonly IToyRepository _toyRepository;
        public int PageSize { get; set; } = 4;

        public ToyController(IToyRepository toyRepository)
        {
            _toyRepository = toyRepository;
        }

        public ViewResult List(int toyPage = 1)
        {
            return View(new ToysListViewModel
            {
                Toys = _toyRepository.Toys
                 .OrderBy(p => p.ToyID)
                 .Skip((toyPage - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = toyPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _toyRepository.Toys.Count()
                }
            });
        }
    }
}