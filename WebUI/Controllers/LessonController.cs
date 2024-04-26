using BusinessLayer.Concrete;
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
    public class LessonController : Controller
    {
        LessonManager _lessonManager = new LessonManager(new EfCoreLessonRepository());
        ClassManager _classManager = new ClassManager(new EfCoreClassRepository());
        public IActionResult Index()
        {
            var values = new LessonListModel()
            {
                Lessons = _lessonManager.GetWithClassList()
            };

            if (values == null)
            {
                return NotFound();
            }

            return View(values);

        }

        [HttpGet]
        public IActionResult Create()
        {
            var classes = _classManager.GetAll();
            ViewBag.classes = new SelectList(classes, "Id", "Name");

            if (classes == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hata",
                    Message = "Bişeyler ters gitti.",
                    Css = "error"
                });
            }

            var values = new LessonListModel()
            {
                Lessons = _lessonManager.GetWithClassList()
            };

            if (values == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hata",
                    Message = "Ders Bulunamadı",
                    Css = "error"
                });
            }

            return View(values);
        }



        [HttpPost]
        public IActionResult Create(LessonModel model)
        {

            var values = new Lesson()
            {
                Name = model.Name,
                ClassId = model.ClassId,
                Condition = model.Condition,
                CreatedDate = model.CreatedDate
            };

            if (values != null)
            {
                _lessonManager.Create(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Ders başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Lesson");
            }

            var classes = _classManager.GetAll();
            ViewBag.classes = new SelectList(classes, "Id", "Name");
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Ders ekleme işlemi başarısız.Bilgilerinizi gözden geçiriniz",
                Css = "error"
            });
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int lessonId, int classId)
        {
            if (lessonId == null || classId == null)
            {

                return NotFound();
            }

            _lessonManager.DeleteFromLesson(lessonId, classId);
            TempData.Put("message", new ResultMessage()
            {
                Title = "Başarılı",
                Message = "Ders silme işlemi başarılı",
                Css = "success"
            });
            return RedirectToAction("Index", "Lesson");

        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _lessonManager.GetById(id);

            if (values == null)
            {
                return NotFound();
            }

            return View(new LessonModel()
            {
                Id = values.Id,
                Name = values.Name,
                ClassId = values.ClassId,
                UpdatedDate = values.UpdatedDate,
                CreatedDate = values.CreatedDate,
                Condition = values.Condition,
            });
        }

        [HttpPost]
        public IActionResult Update(LessonModel model)
        {
            var values = _lessonManager.GetById(model.Id);

            if (values == null)
            {
                return NotFound();
            }

            values.Name = model.Name;
            values.ClassId = model.ClassId;
            values.Condition = model.Condition;
            values.UpdatedDate = model.UpdatedDate;
            values.Condition = model.Condition;

            _lessonManager.Update(values);
            TempData.Put("message", new ResultMessage()
            {
                Title = "Başarılı",
                Message = "Ders güncelleme işlemi başarılı",
                Css = "error"
            });
            return RedirectToAction("Index", "Lesson");
        }
    }
}
