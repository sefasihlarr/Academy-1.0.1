using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = new AppRoleListModel()
            {
                Roles = _roleManager.Roles.ToList(),
            };


            return View(values);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(RoleModel model)
        {


            var values = new AppRole()
            {
                Name = model.Name,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                Condition = model.Condition,
            };

            var result = await _roleManager.CreateAsync(values);

            if (result.Succeeded)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Role başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Role");

            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Detail(int id)
        {

            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            if (values == null)
            {
                return NotFound();
            }

            var model = new RoleModel()
            {
                Id = values.Id,
                Name = values.Name,
            };

            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> Update(RoleModel model)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);

            if (values != null)
            {
                values.Name = model.Name;
                var result = await _roleManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }

                else
                {
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hata",
                        Message = "Role güncelleme işlemi başarısız.Lütfen bigileri gözden geçiriniz",
                        Css = "error"
                    });
                }
            }

            return View(model);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            if (values != null)
            {
                var result = await _roleManager.DeleteAsync(values);
                if (result.Succeeded)
                {
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Başarılı",
                        Message = "Role silme işlemi başarılı",
                        Css = "success"
                    });
                    return RedirectToAction("Index", "Role");
                }
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hata",
                Message = "Role silme işlemi başarısız.Lütefen daha sonra tekarar deneyiniz",
                Css = "error"
            });
            return View();

        }

        public IActionResult Officer()
        {

            return View();
        }


    }
}
