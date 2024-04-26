using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalClassViewComponent:ViewComponent
    {
        ClassManager _classManager = new ClassManager(new EfCoreClassRepository());
        public IViewComponentResult Invoke()
        {

            var values = new TotalCountsModel()
            {
                TotalClass = _classManager.GetClassBranchList().Count(),
            };

            return View(values);
        }
    }
}
