using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class OutputListViewComponent : ViewComponent
    {
        OutputManager _outputManager = new OutputManager(new EfCoreOutputRepository());

        public IViewComponentResult Invoke(OutputModel model)
        {
            var output = _outputManager.GetAll();
            ViewBag.outputs = new SelectList(output, "Id", "Name");
            return View(model);
        }
    }
}
