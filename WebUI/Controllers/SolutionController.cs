using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]

    public class SolutionController : Controller
    {
        SolutionManager _solutionManager = new SolutionManager(new EfCoreSolutionRepository());
        OptionManager _optionManager = new OptionManager(new EfCoreOptionRepository());
        QuestionManager _quesitonManager = new QuestionManager(new EfCoreQuestionRepository());
        public IActionResult Index(int id)
        {
            var values = _quesitonManager.GetById(id);

            if (values == null)
            {
                return NotFound();
            }

            var options = _optionManager.GetAll().Where(x => x.QuestionId == values.Id);
            ViewBag.options = new SelectList(options, "Id", "Text");

            return View(new QuestionModel()
            {
                Id = values.Id,
                Text = values.Text,
                ImageUrl = values.ImageUrl,
                QuestionText = values.QuestionText,
                LessonId = values.LessonId,
                LevelId = values.LevelId,
                SubjectId = values.SubjectId,
                OutputId = values.OutputId,
                Condition = values.Condition,
                SolutionCondition = values.SolutionCondition,
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(SolutionModel model, IFormFile file, QuestionModel questionModel)
        {

            var values = new Solution()
            {
                Id = model.Id,
                Text = model.Text,
                VideoUrl = file.FileName,
                QuestionId = model.QuestionId,
                OptionId = model.OptionId,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                Condition = model.Condition,
            };

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\video", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //Question tablosundaki solution condition durumunu güncelle

            var questionId = _quesitonManager.GetById(model.QuestionId);

            if (questionId != null)
            {
                questionId.SolutionCondition = questionModel.SolutionCondition;
            }



            if (values != null)
            {
                _solutionManager.Create(values);
                _quesitonManager.Update(questionId);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Soru çözümü başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Questions", "Solution");

            }

            var options = _optionManager.GetAll();
            ViewBag.options = new SelectList(options, "Id", "Name");
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Soru çözümü eklenemedi.Lütfen daha sonra tekrar deneyiniz.",
                Css = "error"
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _solutionManager.GetById(id);
            if (values == null)
            {
                return NotFound();
            }

            return View(new SolutionModel()
            {
                Id = values.Id,
                Text = values.Text,
                VideoUrl = values.VideoUrl,
                QuestionId = values.QuestionId,
                Option = values.Option,
                Condition = values.Condition,
            });
        }


        [HttpPost]
        public IActionResult Update(SolutionModel model)
        {
            var values = _solutionManager.GetById(model.Id);

            if (values == null)
            {
                return NotFound();
            }
            if (values != null)
            {
                model.Text = values.Text;
                model.VideoUrl = values.VideoUrl;
                model.QuestionId = values.QuestionId;
                model.OptionId = values.OptionId;
                model.Condition = values.Condition;

                _solutionManager.Update(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Soru çözümü başarıyla güncellendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Solution");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Soru çözümü güncellenemedi.Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });
            return RedirectToAction("Index", "Solution");
        }

        public IActionResult Questions()
        {
            var values = new QuestionListModel()
            {
                Questions = _quesitonManager.GetWithList().OrderByDescending(q => q.Id).ToList()

            };
            return View(values);
        }

    }
}
