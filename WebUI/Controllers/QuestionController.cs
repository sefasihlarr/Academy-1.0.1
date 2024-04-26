using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]

    public class QuestionController : Controller
    {
        QuestionManager _questionManager = new QuestionManager(new EfCoreQuestionRepository());
        LessonManager _lessonManager = new LessonManager(new EfCoreLessonRepository());
        LevelManager _levelManager = new LevelManager(new EfCoreLevelRepository());
        SubjectManager _subjectManager = new SubjectManager(new EfCoreSubjectRepository());
        OutputManager _outputManager = new OutputManager(new EfCoreOutputRepository());
        OptionManager _optionManager = new OptionManager(new EfCoreOptionRepository());
        SolutionManager _solutionManager = new SolutionManager(new EfCoreSolutionRepository());
        ClassManager _classManager = new ClassManager(new EfCoreClassRepository());
        public IActionResult Index()
        {


            var values = new QuestionListModel()
            {
                Questions = _questionManager.GetWithList().ToList(),

            };

            var clases = _classManager.GetAll().ToList();
            ViewBag.clases = new SelectList(clases, "Id", "Name");

            var lessons = _lessonManager.GetAll();
            ViewBag.lessons = new SelectList(lessons, "Id", "Name");

            var level = _levelManager.GetAll();
            ViewBag.levels = new SelectList(level, "Id", "Name");

            var subject = _subjectManager.GetAll();
            ViewBag.subjects = new SelectList(subject, "Id", "Name");

            var output = _outputManager.GetAll();
            ViewBag.outputs = new SelectList(output, "Id", "Name");

            var option = _optionManager.GetAll();
            ViewBag.options = new SelectList(option, "Id", "Name");

            return View(values);
        }

        public JsonResult Class()
        {
            var values = _classManager.GetAll().ToList();
            return Json(values);
        }

        public JsonResult Lesson(int id)
        {
            var values = _lessonManager.GetWithClassList().Where(x => x.ClassId == id).ToList();
            return Json(values);
        }
        public JsonResult Subject(int id)
        {
            var values = _subjectManager.GetWithLessonList().Where(x => x.LessonId == id).ToList();
            return Json(values);
        }


        public JsonResult Output(int id)
        {
            var values = _outputManager.GetAll().Where(x => x.SubjectId == id).ToList();
            return Json(values);
        }

        public IActionResult CashCading()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View(new QuestionModel()
            {

            });
        }


        [HttpPost]
        public async Task<IActionResult> Create(QuestionModel model, IFormFile? file)
        {

            var questionOptions = new List<Option>(); // yeni bir liste oluştur

            foreach (var item in model.Options)
            {
                var optionValue = new Option()
                {
                    Name = item.Name,
                    Text = item.Text,
                    Condition = item.Condition,
                };
                questionOptions.Add(optionValue); // Option nesnelerini yeni liste olarak ekle
            }

            // Question nesnesini oluştur
            var values = new Question()
            {
                Text = model.Text,
                QuestionText = model.QuestionText,
                ImageUrl = file != null ? file.FileName : null,
                LessonId = model.LessonId,
                LevelId = model.LevelId,
                SubjectId = model.SubjectId,
                Options = questionOptions, // yeni liste olarak ekle
                OutputId = model.OutputId,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };

            // Question nesnesini veritabanına ekle
            //soru resmini wwwroot altındaki question dosyasına kaydet
            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\questions", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }


            if (values != null)
            {
                _questionManager.Create(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Soru başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Questions", "Solution");
            }

            // Question nesnesinin Id özelliğini al
            var questionId = values.Id;

            // Option nesnelerinin QuestionId özelliğini güncelle
            foreach (var item in questionOptions)
            {
                item.QuestionId = questionId;
                _optionManager.Create(item);
            }


            //eger bir validation ile karsilasirsa dropdownlarin tekara dolmasi icin tekrar ediyoruz

            var lesson = _lessonManager.GetAll();
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");

            var level = _levelManager.GetAll();
            ViewBag.levels = new SelectList(level, "Id", "Name");

            var subject = _subjectManager.GetAll();
            ViewBag.subjects = new SelectList(subject, "Id", "Name");

            var output = _outputManager.GetAll();
            ViewBag.outputs = new SelectList(output, "Id", "Name");

            var option = _optionManager.GetAll();
            ViewBag.options = new SelectList(option, "Id", "Name");

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Soru ekleme işlemi başarısız.Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });

            return RedirectToAction("Index", "Question", model);
        }

        [HttpPost]
        public IActionResult Delete(int questionId, int outputId, int optionId, int subjectId, int lessonId)
        {
            _questionManager.DeleteFromQuestion(questionId, outputId, optionId, subjectId, lessonId);
            return RedirectToAction("Index", "Question");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _questionManager.GetById(id);
            if (values == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hata",
                    Message = "Soru bulunamadı.",
                    Css = "error"
                });
            }

            return View(new QuestionModel()
            {
                Id = values.Id,
                Text = values.Text,
                QuestionText = values.QuestionText,
                ImageUrl = values.ImageUrl,
                LessonId = values.LessonId,
                LevelId = values.LevelId,
                SubjectId = values.SubjectId,
                Options = values.Options,
                OutputId = values.OutputId,
                CreatedDate = values.CreatedDate,
                UpdatedDate = values.UpdatedDate,
                Condition = values.Condition,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(QuestionModel model, IFormFile file)
        {

            var values = _questionManager.GetById(model.Id);
            if (values != null)
            {
                values.Text = model.Text;
                values.QuestionText = model.QuestionText;
                if (file != null)
                {
                    values.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\questions", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    };

                }
                values.LessonId = model.LessonId;
                values.LevelId = model.LevelId;
                values.SubjectId = model.SubjectId;
                values.Options = model.Options;
                values.OutputId = model.OutputId;
                values.UpdatedDate = model.UpdatedDate;
                values.Condition = model.Condition;


                _questionManager.Update(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Soru güncelleme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Question");
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Soru güncelleme işlemi başarısız",
                Css = "error"
            });
            return View(model);

        }
    }
}
