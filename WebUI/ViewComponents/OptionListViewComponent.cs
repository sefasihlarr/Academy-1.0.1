using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class OptionListViewComponent : ViewComponent
    {
        OptionManager _optionManager = new OptionManager(new EfCoreOptionRepository());

        public IViewComponentResult Invoke(OptionModel model)
        {
            var option = _optionManager.GetAll();
            ViewBag.options = new SelectList(option, "Id", "Name");
            return View(model);
        }
    }
}
