﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Repositories.Interfaces;
using Store.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        #region private fields

        private readonly IToyRepository _toyRepository;
        private readonly ICategoryRepository _categoryRepository;

        #endregion

        #region ctor

        public AdminController(IToyRepository toyRepository,
                               ICategoryRepository categoryRepository)
        {
            _toyRepository = toyRepository;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public IActionResult Index()
        {
            return View( _toyRepository.GetAll());
        }

        public async Task<ViewResult> Edit(int toyId)
        {
            var toy = await _toyRepository.FindByIdAsync(toyId);
            var categories = await _categoryRepository.Categories()
                    .Select(cat => new SelectListItem()
                    {
                        Value = cat.CategoryId.ToString(),
                        Text = cat.Name,
                        Selected = cat.CategoryId==toy.Category.CategoryId
                    }).ToListAsync();

            var model = new ToyCreateViewModel()
            {
                Categories = categories,
                Description = toy.Description,
                Name = toy.Name,
                Price = toy.Price,
                ToyId = toy.ToyId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ToyCreateViewModel toyModel)
        {
            if (!ModelState.IsValid)
            {
                return View(toyModel);
            }

            var toy = new Toy()
            {
                ToyId = toyModel.ToyId,
                Name = toyModel.Name,
                Description = toyModel.Description,
                Price = toyModel.Price
            };


            if (Int32.TryParse(toyModel.Category, out int id))
            {
                toy.Category = await _categoryRepository.FindById(id);
            }

            await _toyRepository.AddOrUpdate(toy);
            TempData["message"] = $"{toy.Name} has been saved";

            return RedirectToAction("Index");
        }

        public async Task<ViewResult> Create()
        {
            var categories = await _categoryRepository.Categories()
                    .Select(cat => new SelectListItem()
                    {
                        Value = cat.CategoryId.ToString(),
                        Text = cat.Name
                    }).ToListAsync();

            var model = new ToyCreateViewModel()
            {
                Categories = categories,
            };

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int toyId)
        {
            var deletedToy = await _toyRepository.FindByIdAsync(toyId);

            if (deletedToy.CartLines.Count() == 0)
            {
                TempData["message"] = $"{deletedToy.Name} was deleted";
                await _toyRepository.DeleteById(toyId);
            }
            else
            {
                TempData["message"] = $"{deletedToy.Name} can not be deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}