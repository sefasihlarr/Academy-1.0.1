using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class TotalStudentViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public TotalStudentViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var users = new TotalCountsModel()
            {
                TotalStudent = _userManager.Users.Where(x => x.Authority == false).Count()
            };



            return View(users);
        }
    }
}
