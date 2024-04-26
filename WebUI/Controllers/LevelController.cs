using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class LevelController : Controller
    {
        LevelManager _levelManager = new LevelManager(new EfCoreLevelRepository());
        public IActionResult Index()
        {
            return View(new LevelListModel()
            {
                Levels = _levelManager.GetAll()
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LevelModel model)
        {
            if (ModelState.IsValid)
            {

                var values = new Level()
                {
                    Name = model.Name,
                    Condition = model.Condition,
                    CreatedDate = model.CreatedDate,
                };

                _levelManager.Create(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Derece ekleme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Level");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Derece ekleme işlemi başarısız",
                Css = "error"
            });
            return View(model);
        }

        public IActionResult Delete(LevelModel model)
        {
            var values = _levelManager.GetById(model.Id);

            if (values != null)
            {
                _levelManager.Delete(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Derece silme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Level");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Derece silmek işlemi başarısız",
                Css = "error"
            });
            return View();
        }

        [HttpGet]
        public IActionResult Detail(LevelModel model)
        {
            var values = _levelManager.GetById(model.Id);

            if (values == null)
            {
                return NotFound();
            }

            return View(new LevelModel()
            {
                Id = values.Id,
                Name = values.Name,
                Condition = values.Condition,
            });
        }

        [HttpPost]
        public IActionResult Update(LevelModel model)
        {
            if (ModelState.IsValid)
            {

                var values = _levelManager.GetById(model.Id);

                if (values == null)
                {
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hata",
                        Message = "Derece bulunamadı.Bilgilerinizi gözden geçiriniz",
                        Css = "error"
                    });
                }

                values.Name = model.Name;
                values.Condition = model.Condition;
                values.UpdatedDate = model.UpdatedDate;
                _levelManager.Update(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Derece güncelleme işlemi başarılı",
                    Css = "success"
                });
            }
            return RedirectToAction("Index", "Level");
        }

    }
}
