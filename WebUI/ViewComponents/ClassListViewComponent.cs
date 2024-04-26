using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class ClassListViewComponent : ViewComponent
    {
        ClassManager _classManager = new ClassManager(new EfCoreClassRepository());

        public IViewComponentResult Invoke(ClassModel model)
        {
            var classes = _classManager.GetAll().ToList();
            //ikinci virgülden sonraki kısım Listede görünecek kısım
            ViewBag.classes = new SelectList(classes, "Id", "Name");
            return View(model);
        }
    }
}
