using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ExamQuestionController : Controller
    {

        QuestionManager _questionManager = new QuestionManager(new EfCoreQuestionRepository());
        ExamManager _examManager = new ExamManager(new EfCoreExamRepository());
        ExamQuestionManager _examQuestionManager = new ExamQuestionManager(new EfCoreExamQuestionRepository());


        public IActionResult Index(ExamModel model)
        {
            List<int> SelectedQuestionIds = new List<int>();
            var questions = _examQuestionManager.GetQuestionsList().Where(x => x.ExamId == model.Id).ToList();

            var values = new QuestionListModel()
            {
                Questions = _questionManager.GetWithList()
                .Where(x => x.LessonId == model.LessonId)
                .Where(x => x.SubjectId == model.SubjectId)
                .ToList(),
                SelectedQuestions = SelectedQuestionIds
            };

            foreach (var item in questions)
            {
                if (values.Questions.Any(x => x.Id == item.QuestionId))
                {
                    SelectedQuestionIds.Add(item.QuestionId);
                }
            }

            if (SelectedQuestionIds.Count == 0)
            {
                ViewBag.btnCondition = false;
            }

            else
            {
                ViewBag.btnCondition = true;
            }

            ViewBag.ExamId = model.Id;

            return View(values);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExamQuestions model, int[] questionIds)
        {
            if (model != null & questionIds != null)
            {


                foreach (var item in questionIds)
                {
                    //buraya userId eklenecek
                    _examQuestionManager.Create(model, item);

                };
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Sinav soruları basariyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");

            }

            return View(model);
        }



        //burası silmeyecek alan  arasında yer alabilir. ilişkileri silmemiz gerekiyor 
        [HttpPost]
        public IActionResult Delete(int examId, int[] questionsId)
        {
            var values = _examQuestionManager.GetById(examId);

            if (examId != null & questionsId != null)
            {
                foreach (var item in questionsId)
                {
                    _examQuestionManager.DeleteFormExamQuestion(values, item);
                }

                return RedirectToAction("Index", "Exam");

            }

            return NotFound();
        }


        //şu an için kullanılmayacak
        //sınavın sorularını farklı bir sayfada sıralayıp listeleyebiliz
        //güncelleme işlemlerinide kolaylarştır
        [HttpGet]
        public IActionResult Detail(int id)
        {
            return View();
        }


        [HttpPost]
        public IActionResult Update(ExamQuestions model, int[] questionIds)
        {
            var questions = _examQuestionManager.GetAll().Where(x => x.ExamId == model.ExamId).ToList();

            if (model != null & questionIds != null)
            {

                foreach (var item in questions)
                {
                    _examQuestionManager.Delete(item);
                }

                foreach (var item in questionIds)
                {
                    _examQuestionManager.Create(model, item);
                }

                TempData.Put("message", new ResultMessage()
                {
                    Title = "Basarılı",
                    Message = "Sinav soruları basariyla Güncellendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");
            }

            return View(model);
        }

    }
}
