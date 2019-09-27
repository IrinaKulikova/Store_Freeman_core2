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

        public ViewResult List(string category,int toyPage = 1)
        {
            return View(new ToysListViewModel
            {
                Toys = _toyRepository.Toys
                 .Where(t => category == null || t.Category == category)
                 .OrderBy(p => p.ToyID)
                 .Skip((toyPage - 1) * PageSize)
                 .Take(PageSize),
                CurrentCategory = category,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = toyPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null?
                    _toyRepository.Toys.Count():
                    _toyRepository.Toys.Where(t=>
                    t.Category == category).Count()
                }                
            });
        }
    }
}