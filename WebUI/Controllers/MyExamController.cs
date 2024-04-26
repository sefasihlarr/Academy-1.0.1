using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MyExamController : Controller
    {
        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());
        ExamAnswerManager _examAnswerManager = new ExamAnswerManager(new EfCoreExamAswerRepository());
        ScorsManager _scorsManager = new ScorsManager(new EfCoreScorsRepository());
        AppUserManager _appUserManager = new AppUserManager(new EfCoreAppUserRepostory());
        QuestionManager _questionManager = new QuestionManager(new EfCoreQuestionRepository());
        private readonly UserManager<AppUser> _userManager;

        public MyExamController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = _appUserManager.GetById(Convert.ToInt32(userId));

            var answeredExamIds = _examAnswerManager.GetAll().Select(a => a.ExamId).ToList();

            var values = new ExamListModel()
            {
                Exams = _examManager.GetWithList()
                .Where(x => x.ClassId == getId.ClassId && !answeredExamIds.Contains(x.Id))
                .ToList()
            };
            return View(values);
        }

        public IActionResult TeacherExams()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = _appUserManager.GetById(Convert.ToInt32(userId));

            var values = new ExamListModel()
            {
                Exams = _examManager.GetWithList().Where(x => x.UserId == getId.Id).ToList(),
                Scors = _scorsManager.GetAll().Where(x => x.UserId == getId.Id).ToList(),
            };


            List<int> examIds = new List<int>();


            foreach (var item in values.Exams)
            {
                foreach (var value in  values.Scors)
                {
                    if (item.Id == value.ExamId)
                    {
                        examIds.Add(value.Id);
                    }
                }
            }


            var scors = _scorsManager.GetAll().ToList();

            ViewBag.Scors = scors;

            return View(values);
        }

        public IActionResult Exam(int id)
        {
            var süre = 60;

            var values = new QuestionListModel()
            {
                Questions = _questionManager.GetQuestionsByExam(id),

                SureDegeri = süre
            };
            ViewBag.ExamId = id;

            return View(values);
        }



    }
}
