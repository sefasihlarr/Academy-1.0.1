using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class BranchFormViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var values = new BranchModel()
            {

            };


            return View(values);
        }
    }
}
