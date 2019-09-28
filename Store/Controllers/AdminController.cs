using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Store.Controllers
{
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
    }
}