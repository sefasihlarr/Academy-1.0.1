using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]

    public class OptionController : Controller
    {
        OptionManager _optionManager = new OptionManager(new EfCoreOptionRepository());
        public IActionResult Index()
        {
            var values = new OptionListModel()
            {
                Options = _optionManager.GetAll()
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(OptionModel model)
        {
            if (ModelState.IsValid)
            {

                var values = new Option()
                {
                    Name = model.Name,
                    Text = model.Text,
                    Condition = model.Condition,
                };

                if (values != null)
                {
                    _optionManager.Create(values);
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Başarılı",
                        Message = "Şık ekleme işlemi başarılı",
                        Css = "success"
                    });
                    return RedirectToAction("Index", "Option");
                }

            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Şık eklemek işlemi başarısız.Lütfen bilgilerinizi gözden geçiriniz",
                Css = "error"
            });
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(OptionModel model)
        {
            var values = _optionManager.GetById(model.Id);
            if (values != null)
            {
                _optionManager.Delete(values);
                return RedirectToAction("Index", "Option");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _optionManager.GetById(id);

            if (values == null)
            {
                return NotFound();
            }


            return View(new OptionModel()
            {
                Id = values.Id,
                Name = values.Name,
                Text = values.Text,
                Condition = values.Condition,
            });
        }
    }
}
