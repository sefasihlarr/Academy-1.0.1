using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class ClassFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = new ClassModel()
            {

            };


            return View(values);
        }
    }
}
