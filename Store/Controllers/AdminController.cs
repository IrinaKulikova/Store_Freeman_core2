using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        #region private fields

        private IToyRepository _toyRepository;

        #endregion

        #region ctor

        public AdminController(IToyRepository toyRepository)
        {
            _toyRepository = toyRepository;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            return View(await _toyRepository.Toys().ToListAsync());
        }

        public async Task<ViewResult> Edit(int toyId)
        {
            return View(await _toyRepository.FindByIdAsync(toyId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Toy toy)
        {
            if (!ModelState.IsValid)
            {
                return View(toy);
            }

            await _toyRepository.AddOrUpdate(toy);
            TempData["message"] = $"{toy.Name} has been saved";

            return RedirectToAction("Index");
        }

        public ViewResult Create() => View("Edit", new Toy());

        [HttpPost]
        public async Task<IActionResult> Delete(int toyId)
        {
            var deletedToy = await _toyRepository.DeleteToy(toyId);

            if (deletedToy != null)
            {
                TempData["message"] = $"{deletedToy.Name} was deleted";
            }

            return RedirectToAction("Index");
        }
    }
}