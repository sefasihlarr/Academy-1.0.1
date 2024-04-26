using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class RegisterFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RegisterModel model)
        {
            var values = new RegisterModel()
            {

            };

            return View(values);
        }
    }
}
