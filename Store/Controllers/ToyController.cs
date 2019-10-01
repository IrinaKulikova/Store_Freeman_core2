using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Repositories.Interfaces;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class ToyController : Controller
    {
        #region private fields

        private readonly IToyRepository _toyRepository;
        private readonly ICategoryRepository _categoryRepository;

        #endregion

        #region props

        public int PageSize { get; set; } = 6;

        #endregion

        #region ctor

        public ToyController(IToyRepository toyRepository,
                             ICategoryRepository categoryRepository)
        {
            _toyRepository = toyRepository;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public async Task<ViewResult> List(string category, int toyPage = 1)
        {
            ICollection<Toy> toys = null;

            var currentCategory = (!String.IsNullOrEmpty(category)) ?
                        await _categoryRepository.FindByName(category) : null;

            if (String.IsNullOrEmpty(category))
            {

                toys = _toyRepository.Toys()
                           .OrderBy(p => p.ToyId)
                           .Skip((toyPage - 1) * PageSize)
                           .Take(PageSize).ToList();
            }
            else
            {
                toys = currentCategory.Toys;
            }

            
            var total = (String.IsNullOrEmpty(category)) ?
                                _toyRepository.Toys().Count() :
                                currentCategory.Toys.Count();

            var model = new ToysListViewModel
            {
                Toys = toys,
                CurrentCategory = currentCategory,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = toyPage,
                    ItemsPerPage = PageSize,
                    TotalItems = total
                }
            };

            return View(model);
        }
    }
}