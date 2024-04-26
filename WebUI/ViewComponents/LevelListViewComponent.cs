using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class LevelListViewComponent : ViewComponent
    {
        LevelManager _levelManager = new LevelManager(new EfCoreLevelRepository());

        public IViewComponentResult Invoke(LevelModel model)
        {
            var level = _levelManager.GetAll();
            ViewBag.levels = new SelectList(level, "Id", "Name");
            return View(model);
        }
    }
}
