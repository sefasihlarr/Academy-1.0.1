using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ExamController : Controller
    {
        ClassManager _classManager = new ClassManager(new EfCoreClassRepository());
        AppUserManager _appUserManager = new AppUserManager(new EfCoreAppUserRepostory());
        LessonManager _lessonManager = new LessonManager(new EfCoreLessonRepository());
        SubjectManager _subjectManager = new SubjectManager(new EfCoreSubjectRepository());
        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());
        ScorsManager _scorsManager = new ScorsManager(new EfCoreScorsRepository());
        QuestionManager _questionManager = new QuestionManager(new EfCoreQuestionRepository());

        private readonly UserManager<AppUser> _userManager;

        public ExamController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var values = new ExamListModel()
            {
                Exams = _examManager.GetWithList().ToList(),
            };

            return View(values);
        }

        public IActionResult AllExamList()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = _appUserManager.GetById(Convert.ToInt32(userId));

            var values = new ExamListModel()
            {
                Exams = _examManager.GetWithList().ToList(),
                Scors = _scorsManager.GetAll().ToList(),
            };


            List<int> examIds = new List<int>();


            foreach (var item in values.Exams)
            {
                foreach (var value in values.Scors)
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ExamModel model)
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);

            var user = _appUserManager.GetById(Convert.ToInt32(userId));

            var values = new Exam()
            {
                Id = model.Id,
                UserId = user.Id,
                Timer = model.Timer,
                Title = model.Title,
                Description = model.Description,
                ExamDate = model.ExamDate,
                ClassId = model.ClassId,
                LessonId = model.LessonId,
                SubjectId = model.SubjectId,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                Condition = model.Condition,
            };


            if (values != null)
            {
                _examManager.Create(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Basariyla Eklendi",
                    Message = "Sinav basariyla eklendi Sinavlarim kismindan eklemis oldugunuz sinavlari goruntuleyebilirsiniz",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");

            }



            var lesson = _lessonManager.GetAll();
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");

            var level = _classManager.GetAll();
            ViewBag.levels = new SelectList(level, "Id", "Name");

            var subject = _subjectManager.GetAll();
            ViewBag.subjects = new SelectList(subject, "Id", "Name");

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Sinav ekleme işlemi başarısız.Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });

            return RedirectToAction("Index", "Exam", model);
        }



        [HttpPost]
        public IActionResult Delete(int examId, int classId, int lessonId, int subjectId)
        {
            if (examId != null || classId != null || lessonId != null || subjectId != null)
            {
                _examManager.DeleteFromExam(examId, classId, lessonId, subjectId);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Silme basarili",
                    Message = "Sinaviniz basariyla silindi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Silme işlemli başarısız",
                Message = "Sınav silmek işlemi başarısız.Lütfen daha sonra tekrar deneyiniz.",
                Css = "erorr"
            });
            return RedirectToAction("Index", "Exam");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _examManager.GetById(id);
            if (values == null)
            {
                return NotFound();
            }


            return View(new ExamModel()
            {
                Id = values.Id,
                Title = values.Title,
                Description = values.Description,
                ClassId = values.ClassId,
                LessonId = values.LessonId,
                SubjectId = values.SubjectId,
                CreatedDate = values.CreatedDate,
                UpdatedDate = values.UpdatedDate,
                Condition = values.Condition,
            });
        }

        [HttpPost]
        public IActionResult Update(ExamModel model)
        {
            var values = _examManager.GetById(model.Id);
            if (values != null)
            {
                values.Id = model.Id;
                values.Title = model.Title;
                values.Description = model.Description;
                values.ClassId = model.ClassId;
                values.LessonId = model.LessonId;
                values.SubjectId = model.SubjectId;
                values.UpdatedDate = model.UpdatedDate;
                values.Condition = model.Condition;

                _examManager.Update(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Guncelleme basarili",
                    Message = "Sinav guncelleme islemi basariyla gerceklesti",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Guncelleme basarisiz",
                Message = "Sinav guncelleme islemi basarisiz lütfen bilgileri gozden geciriniz",
                Css = "error"
            });
            return View(model);
        }



        [HttpGet]
        public IActionResult Exam(int id)
        {
            var exam = _examManager.GetById(id);

            var values = new QuestionListModel()
            {

                Questions = _questionManager.GetQuestionsByExam(id),

                SureDegeri = exam.Timer

            };

            ViewBag.ExamId = id;

            return View(values);
        }
    }
}
