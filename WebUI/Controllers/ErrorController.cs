using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ErrorController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
