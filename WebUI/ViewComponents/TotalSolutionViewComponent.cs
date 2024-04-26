using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalSolutionViewComponent : ViewComponent
    {
        SolutionManager _solutionManager = new SolutionManager(new EfCoreSolutionRepository());
        public IViewComponentResult Invoke()
        {
            var values = new TotalCountsModel()
            {
                TotalSolution = _solutionManager.GetAll().Count()
            };


            return View(values);
        }
    }
}
