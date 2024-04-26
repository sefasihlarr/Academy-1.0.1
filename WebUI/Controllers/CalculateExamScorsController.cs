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
    public class CalculateExamScorsController : Controller
    {

        ExamAnswerManager _examAnswerManager = new ExamAnswerManager(new EfCoreExamAswerRepository());
        SolutionManager _solutionManager = new SolutionManager(new EfCoreSolutionRepository());
        ScorsManager _scoresManager = new ScorsManager(new EfCoreScorsRepository());
        CartManager _cartManager = new CartManager(new EfCoreCartRepository());
        AppUserManager _appUserManager = new AppUserManager(new EfCoreAppUserRepostory());
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public CalculateExamScorsController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult AllUsersResultQuestions(int id)
        {
            // Sınava katılan tüm öğrencilerin listesini al
            var users = _userManager.Users.Where(x => x.Authority == false).ToList();
            // Her kullanıcının sınav sonucunu hesapla ve veritabanına kaydet
            foreach (var user in users)
            {
                // Kullanıcının sınav cevaplarını al
                var userExamAnswers = _examAnswerManager.GetListTogether().Where(x => x.ExamId == id && x.UserId == user.Id).ToList();

                var nullQuestion = 0;
                var score = 0;
                var questionFalse = 0;
                var questionTrue = 0;
                var trueOption = "";
                var correctAnswers = new List<string>();

                if (userExamAnswers == null)
                {
                    continue;
                }

                foreach (var examAnswer in userExamAnswers)
                {
                    if (examAnswer.OptionId != null)
                    {
                        var solutions = _solutionManager.GetWithQuestionList();
                        var solution = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.Question.Id && s.OptionId == examAnswer.Option.Id);

                        var TrueOption = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.QuestionId).Option.Name;
                        correctAnswers.Add(TrueOption);

                        if (solution != null)
                        {
                            score += 1;
                            questionTrue += 1;
                        }

                        else if (solution == null)
                        {
                            questionFalse += 1;
                        }

                        else
                        {
                            nullQuestion += 1;
                        }

                    }
                    else
                    {
                        var solutions = _solutionManager.GetWithQuestionList();
                        var TrueOption = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.QuestionId).Option.Name;
                        correctAnswers.Add(TrueOption);
                        nullQuestion += 1;
                    }
                }

                decimal totalNegative = questionFalse * 0.25m; // 'm' ekleyerek decimal tipinde bir sabit belirtiyoruz
                decimal net = questionTrue - totalNegative;
                decimal totalScore = net * 100m; // 'm' ekleyerek decimal tipinde bir sabit belirtiyoruz
                int totalQuestion = userExamAnswers.Count;
                if (totalScore != 0)
                {
                    decimal ExamScor = Math.Round(totalScore / totalQuestion, 2);
                    var model = new ExamAnswerListModel()
                    {
                        ExamAnswers = userExamAnswers,
                        QuestionFalse = questionFalse,
                        QuestionTrue = questionTrue,
                        QuestionNull = nullQuestion,
                        Score = score,
                        CorrectAnswers = correctAnswers,
                        UserId = user.Id, // Kullanıcının Id'sini de modelde kaydet
                        ExamId = id // Sınavın Id'sini de modelde kaydet
                    };


                    var values = new Scors()
                    {
                        UserId = model.UserId,
                        ExamId = model.ExamId,
                        True = model.QuestionTrue,
                        False = model.QuestionFalse,
                        Null = model.QuestionNull,
                        Average = net,
                        Scor = ExamScor,
                        Condition = false,
                    };

                    // Modeli veritabanına kaydet
                    _scoresManager.Create(values);
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Başarılı",
                        Message = "Hesaplama işlemi başarılı",
                        Css = "success"
                    });

                }
                // sonucu iki ondalık basamakla yuvarlıyoruz


            }

            return RedirectToAction("TeacherExams", "MyExam");
        }


        public IActionResult UpdateScors(int id, bool condition)
        {
            var scors = _scoresManager.GetAll().Where(x => x.ExamId == id).ToList();
            if (scors != null)
            {
                if (condition == true)
                {
                    foreach (var item in scors)
                    {
                        item.Condition = condition;
                        _scoresManager.Update(item);
                    }

                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Başarılı",
                        Message = "Sınav sonuçları başarıyla yayınlandı.",
                        Css = "success"
                    });

                    return RedirectToAction("TeacherExams", "MyExam");
                }

                if (condition == false)
                {
                    foreach (var item in scors)
                    {
                        item.Condition = condition;
                        _scoresManager.Update(item);
                    }

                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Uyarı!",
                        Message = "Sınav sonuçları yayından kaldırıldı.",
                        Css = "warning"
                    });

                    return RedirectToAction("TeacherExams", "MyExam");
                }



                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hata!",
                    Message = "Sınav yayınlama işleminde bir aksaklık yaşandı.",
                    Css = "error"
                });


                return RedirectToAction("TeacherExams", "MyExam");

            }



            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Sınav sonuçları yayınlanamadı.",
                Css = "error"
            });

            return View();
        }
    }
}
