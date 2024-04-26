using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class LastExamResultViewComponent : ViewComponent
    {
        ScorsManager _scorsManager = new ScorsManager(new EfCoreScorsRepository());
        private readonly AppUserManager _appUserManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public LastExamResultViewComponent(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var latestScore = _scorsManager.GetTogetherList().OrderByDescending(s => s.ExamId).FirstOrDefault();
            var values = new ScorListModel();

            if (await _userManager.IsInRoleAsync(user, "Öğrenci"))
            {
                values.scors = _scorsManager.GetTogetherList().Where(s => s.User.ClassId == user.ClassId
                && s.ExamId == latestScore.ExamId
                && s.Condition == true).ToList();
            }
            else if (await _userManager.IsInRoleAsync(user, "Öğretmen") ||
                     await _userManager.IsInRoleAsync(user, "Müdür") ||
                     await _userManager.IsInRoleAsync(user, "MüdürYardımcısı"))
            {
                values.scors = _scorsManager.GetTogetherList().Where(s => s.ExamId == latestScore.ExamId).ToList();
            }

            return View(values);
        }
    }
}
