using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class InfoGuardianViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        AppUserManager _appUserManager = new AppUserManager(new EfCoreAppUserRepostory());
        GuardianManager _guardianManager = new GuardianManager(new EfCoreGuardianRepository());

        public InfoGuardianViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = _appUserManager.GetById(Convert.ToInt32(userId));

            var info= _guardianManager.GetAll().FirstOrDefault(x => x.UserId == getId.Id);


            var values = new GuardianModel()
            {

                GuardianName = info.GuardianName,
                GuardianSurName = info.GuardianSurName,



            };
            return View();
        }
    }
}
