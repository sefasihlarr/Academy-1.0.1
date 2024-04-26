using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalTeacherViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public TotalTeacherViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = new TotalCountsModel()
            {
                TotalTeacher = _userManager.Users.Where(x => x.Authority == true).Count()
            };
            return View(values);
        }
    }
}
