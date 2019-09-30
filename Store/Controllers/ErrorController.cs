using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers {

    public class ErrorController : Controller {

        public ViewResult Error() => View();
    }
}
