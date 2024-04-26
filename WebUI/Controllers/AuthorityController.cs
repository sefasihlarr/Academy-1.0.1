using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AuthorityController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthorityController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var values = new AuthortyListModel()
            {
                Users = _userManager.Users.Where(x => x.Authority == true).ToList(),
            };
            return View(values);
        }
        [HttpGet]
        public IActionResult Register()
        {

            var values = new AppUserListModel()
            {
                Users = _userManager.Users.ToList(),
            };

            return View(values);
        }


        [HttpPost]
        public async Task<IActionResult> Register(AuthorityModel model)
        {


            AppUser user = new AppUser()
            {
                UserName = Convert.ToString(model.TcNumber),
                Name = model.Name,
                SurName = model.SurName,
                Tc = model.TcNumber,
                Authority = model.Authority,
                PhoneNumber = model.Phone,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    Token = code,
                });

                //Burası email gönderme kısmı(send Email)

                TempData.Put("message", new ResultMessage()
                {
                    Title = "Başarılı",
                    Message = "Gorevli basariyla olsuturldu :)",
                    Css = "success"
                });
                return RedirectToAction("Index", "Authority");
            }
            else
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hata",
                    Message = "Görevli olusturulamadı lütfen tekrar deneyiniz",
                    Css = "error"
                });

                return RedirectToAction("Index", "Authority");
            }


        }


		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					TempData.Put("message", new ResultMessage()
					{
						Title = "Hata",
						Message = "Kullanıcı Silinemedi",
						Css = "error"
					});
				}
			}
			else
			{
				TempData.Put("message", new ResultMessage()
				{
					Title = "Hata",
					Message = "Kullanıcı bulunamadı",
					Css = "error"
				});
			}

            return RedirectToAction("Index", user);
		}


		public async Task<IActionResult> Detail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var values = new AppUserModel()
            {
                Id = user.Id,
                Tc = user.Tc,
                Name = user.Name,
                SurName = user.SurName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Authority = user.Authority,
            };

            return View(values);
        }


        public async Task<IActionResult> Update(AppUserModel model)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString( model.Id));

            if (user!=null)
            {
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Tc = model.Tc;
                user.Email = model.Email;
                user.Condition = model.Condition;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
				TempData.Put("message", new ResultMessage()
				{
					Title = "Başarılı",
					Message = "Kullanıcı güncellme işlemi başarılı",
					Css = "success"
				});

                return RedirectToAction("Index");
			}

            else
            {
				TempData.Put("message", new ResultMessage()
				{
					Title = "Hata",
					Message = "Kullanıcı güncellenemedi.Bilgilerinizi gözden geçiriniz",
					Css = "error"
				});
			}

			return RedirectToAction("Index");
		}

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.First(x => x.Id == id);

            var roles = _roleManager.Roles.ToList();

            TempData["UserId"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new List<RoleAssingViewModel>();

            foreach (var item in roles)
            {
                var x = new RoleAssingViewModel();
                x.RoleId = item.Id;
                x.Name = item.Name;
                x.Exists = userRoles.Contains(item.Name);
                model.Add(x);
            }



            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssingViewModel> model)
        {
            var userId = (int)TempData["UserId"];

            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Başarılı",
                Message = "Role atama islemleriniz basariyla gerceklesti",
                Css = "success"
            });

            return RedirectToAction("Index", "Authority");

        }


        //[HttpGet]
        //public async  Task<IActionResult> AssignRole(AppRoleListModel model,string id)
        //{
        //	id = "19";
        //          var user = await _userManager.FindByIdAsync(id);

        //	ViewBag.userId=user.Id;

        //          var values= model.Roles = _roleManager.Roles.ToList();
        //	return View(values);

        //}

        //[HttpPost]
        //public async Task<IActionResult> AssignRole(AppUserModel model, string[] roleNames)
        //{
        //	var user = _userManager.Users.FirstOrDefault(x => x.Id == model.Id);

        //	if (model!=null & roleNames != null)
        //	{
        //		foreach (var item in roleNames)
        //		{
        //                 var result =  await _userManager.AddToRoleAsync(user,item);

        //			if (result.Succeeded)
        //			{
        //                      return RedirectToAction("Index", "Authority");
        //                  }

        //			else
        //			{
        //                      foreach (var error in result.Errors)
        //                      {
        //                          ModelState.AddModelError(string.Empty, error.Description);
        //                      }

        //				return NotFound();
        //                  }
        //              }

        //	}

        //	return NotFound();
        //}
    }
}
