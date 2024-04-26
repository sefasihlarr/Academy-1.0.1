using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ExamAnswerController : Controller
    {

        ExamAnswerManager _examAnswerManager = new ExamAnswerManager(new EfCoreExamAswerRepository());
        ExamQuestionManager _examQuestionManager = new ExamQuestionManager(new EfCoreExamQuestionRepository());
        CartManager _cartManager = new CartManager(new EfCoreCartRepository());
        AppUserManager _appUserManager = new AppUserManager(new EfCoreAppUserRepostory());
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;


        public ExamAnswerController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create(ExamAnswers model)
        {
            if (model != null)
            {
                ViewBag.ExamModel = model.ExamId;
                return View(model);
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult Create(ExamAnswers model, int[] questionIds, Dictionary<int, int> optionIds)
        {
            if (model != null && questionIds != null && optionIds != null)
            {
                var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
                var getId = _appUserManager.GetById(Convert.ToInt32(userId));
                model.UserId = getId.Id;

                foreach (var item in questionIds)
                {
                    //TryGetValue değeri kontrol edip varsa atama yapar yoksa değeri null yaparak geçer
                    if (optionIds.TryGetValue(item, out int option))
                    {
                        _examAnswerManager.Create(model, item, option);
                    }
                    else
                    {
                        _examAnswerManager.Create(model, item, null);
                    }
                }

                _cartManager.AddToCart(Convert.ToString(getId.Id), model.ExamId);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Sinav Bitti",
                    Message = "Sinaviniz bitti.Sinav sonucunuza Sinav sonuclarım kismindan ulasabilirsiniz :)",
                    Css = "warning"
                });
                return RedirectToAction("Index", "Home");
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Sinavinizda hata ile karsilastik lütfen yetkili birimler iletisime geciniz",
                Css = "error"
            });
            return View(model);
        }


    }
}
