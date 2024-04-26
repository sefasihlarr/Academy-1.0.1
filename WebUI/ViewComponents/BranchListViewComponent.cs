using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class BranchListViewComponent : ViewComponent
    {
        BranchManager _branchManager = new BranchManager(new EfCoreBranchRepository());

        public IViewComponentResult Invoke(BranchModel model)
        {
            var branch = _branchManager.GetAll();
            ViewBag.brances = new SelectList(branch, "Id", "Name");
            return View(model);
        }
    }
}
