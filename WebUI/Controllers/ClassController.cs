using BusinessLayer.Concrete;
using DataAccessLayer.EfCoreRepository;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ClassController : Controller
    {
        ClassManager _classManager = new ClassManager(new EfCoreClassRepository());
        BranchManager _branchManager = new BranchManager(new EfCoreBranchRepository());

        public IActionResult Index()
        {
            return View(new ClassListModel()
            {
                //where(x=> x.solution).ToList() Expression oldugu icin filtreleme yapabiliriz
                Classes = _classManager.GetAll().ToList()
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassModel model)
        {

            var values = new Class
            {
                Name = model.Name,
                Condition = model.Condition,
            };

            if (values != null)
            {
                _classManager.Create(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Basarili",
                    Message = "Ekleme islemi basariyla gerceklesti",
                    Css = "success"
                });
            }


            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Bişeyler ters gitti.Lütfen sınıf bilgilerini gözden geçirinizi",
                Css = "error"
            });


            return RedirectToAction("Index", "Class");

        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var values = _classManager.GetById(id);
            if (values != null)
            {
                _classManager.Delete(values);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarli",
                    Message = "Silme işlemi başarıyla gerçekleşti",
                    Css = "success"
                });
                return RedirectToAction("Index", "Class");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata!!!",
                Message = "Silme islemi gerceklestirlemedi",
                Css = "error"
            });

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var values = _classManager.GetByIdWithBrances(id);

            if (values == null)
            {
                return NotFound();
            }

            ViewBag.Brances = _branchManager.GetAll().ToList();

            var model = new ClassModel()
            {
                Id = values.Id,
                Name = values.Name,
                Condition = values.Condition,
                SelectedBranch = values.ClassBranches.Select(x => x.Branch).ToList(),
            };

            return View(model);

        }



        [HttpPost]
        public IActionResult Update(ClassModel model, int[] branchIds)
        {
            if (ModelState.IsValid)
            {

                var values = _classManager.GetById(model.Id);

                if (values == null)
                {
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hata",
                        Message = "Guncelleme islemi basarisiz sinifi bulamadik lütefen sonra tekrar deneyiniz",
                        Css = "error"
                    });
                }

                values.Name = model.Name;
                values.Condition = model.Condition;
                values.UpdatedDate = model.UpdatedDate;
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Basarili",
                    Message = "Sinif basariyla guncellendi",
                    Css = "success"
                });
                _classManager.Update(values, branchIds);
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Sinif guncelleme islemi başarıyla gerçeklerşti",
                    Css = "success"
                });
                return RedirectToAction("Index", "Class");
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Sinif guncelleme islemi basarisiz lütfen bilgileri gozden geciriniz var eksik olmadıgına dikkat ediniz",
                Css = "error"
            });
            return RedirectToAction("Index", "Class");
        }

    }
}
