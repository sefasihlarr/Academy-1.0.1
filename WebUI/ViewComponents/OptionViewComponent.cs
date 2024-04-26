using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class OptionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new QuestionModel()
            {
            });
        }
    }
}
