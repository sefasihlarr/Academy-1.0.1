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
    public class BranchController : Controller
    {
        BranchManager _branchManager = new BranchManager(new EfCoreBranchRepository());
        public IActionResult Index()
        {
            return View(new BranchListModel()
            {
                Branches = _branchManager.GetAll().ToList()
            });

        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public IActionResult Create(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                var values = new Branch
                {

                    Name = model.Name,
                    Condition = model.Condition,
                    CreatedDate = model.CreatedDate,
                };
                if (values == null)
                {
                    return NotFound(model);
                }
                _branchManager.Create(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Basarili",
                    Message = "Sube basariyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Branch");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Opps ! Biseyler ters gitti",
                Message = "Lutfen Şube bilgilerini tekrar gozden gecirinizi.",
                Css = "error"
            });
            return RedirectToAction("Index", "Branch", model);



        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var values = _branchManager.GetById(id);
            if (values != null)
            {

                _branchManager.Delete(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Basarili",
                    Message = "Sube basariyla silindi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Branch");
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Opps!Biseyler ters gitti daha sonra tekrar deneyiniz.",
                Css = "error"
            });
            return View("Index", "Branch");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _branchManager.GetById(id);

            if (values == null)
            {

                return NotFound();
            }

            return View(new BranchModel()
            {
                Id = values.Id,
                Name = values.Name,
                Condition = values.Condition,

            });
        }
        [HttpPost]
        public IActionResult Update(BranchModel model)
        {

            var values = _branchManager.GetById(model.Id);
            if (values == null)
            {
                return NotFound();
            }
            values.Name = model.Name;
            values.Condition = model.Condition;
            values.UpdatedDate = model.UpdatedDate;
            _branchManager.Update(values);
            TempData.Put("message", new ResultMessage()
            {
                Title = "Guncelleme basarili",
                Message = "Sube guncelleme islemi bariyla gerceklesti",
                Css = "success"
            });
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Sube guncellenemedi bilgileri tekrar gozden geciriniz.",
                Css = "error"
            });
            return RedirectToAction("Index", "Branch");
        }


    }
}
