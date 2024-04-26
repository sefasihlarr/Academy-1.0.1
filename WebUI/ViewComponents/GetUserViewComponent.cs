using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class GetUserViewComponent : ViewComponent
    {
        AppUserManager _appUserManager = new AppUserManager(new EfCoreAppUserRepostory());
        private UserManager<AppUser> _userManager;

        public GetUserViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);

            var values = _appUserManager.GetById(Convert.ToInt32(userId));

            return View(new AppUserModel()
            {
                Id = values.Id,
                Name = values.Name,
                SurName = values.SurName,

            });
        }
    }
}
